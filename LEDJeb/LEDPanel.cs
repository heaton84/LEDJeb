/*
 * LEDJeb LEDPanel
 * 
 * Author: heaton84
 * Date:   1/10/2015
 * Brief:  Converts flight data into a serial byte stream, which is sent out
 *         to the virtual LED app and/or physical LED devices via IDataStream
 *         interfaces.
 * 
 * TODO List:
 * 
 * 1. Send init packet upon successful connection (write reg 16 with display #)
 * 2. Provide configuration interface for # of displays and what goes to which display
 * 3. ACK/NAK feedback from controller (power lost in mid-stream = no idea how many displays are there)
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace LEDJeb
{
    /// <summary>
    /// Organizes multiple LEDs into logical sequence, manages
    /// converting raw flight data into byte streams
    /// </summary>
    public class LEDPanel
    {
        private List<IDataStream> m_DataStreams = new List<IDataStream>();

        private string[] m_LEDReadout;      // Holds actual readout text (ie "  123456")
        private DisplayVar[] m_LEDVars;     // Tells logic which flight var goes where
        private int[] m_LEDDecimalPlaces;   // Tells logic how many decimal places should go on the display

        private bool m_UserIdInProgress = false;

        public enum DisplayVar
        {
            Altitude,
            Apoapsis,
            ApoapsisETA,
            BLANK,
            FlightMissionTime,
            FlightVelocity,
            ManeuverBurnTime,
            ManeuverDeltaV,
            ManeuverETA,
            OrbitalInclination,
            OrbitalPeriod,
            Periapsis,
            PeriapsisETA,
            TargetDistance,
            TargetETA,
            TargetRelativeInclination,
            TargetRelativeV
        }

        public void InitializeFromConfig()
        {
            int ledCount = LEDJebBehavior.GetConfigDisplayCount();
            IDataStream[] configStreams = LEDJebBehavior.GetConfigDataStreams();

            m_LEDReadout = new string[ledCount];
            m_LEDVars = LEDJebBehavior.GetConfigDisplayVars();
            m_LEDDecimalPlaces = LEDJebBehavior.GetConfigDisplayDecimalPlaces();

            m_DataStreams.AddRange(configStreams);
        }

        public void LoadFlightData(LEDJebFlightData p_FlightData)
        {
            for (int i = 1; i <= m_LEDReadout.Length; i++)
                m_LEDReadout[i - 1] = FormatFlightVar(i, p_FlightData);

            //p_Mono.Debug("Loadp_FlightData: alt={0} raw={1} Idsc={2}", altitude, m_LEDs[4], m_DataStreams.Count);

            // Push changes out to any configured clients
            foreach (IDataStream Ids in m_DataStreams)
                UpdateDevice(Ids);
        }

        /// <summary>
        /// Tells display controller how many display modules are attached to it
        /// </summary>
        public void InitializeAllDevices()
        {
            byte[] initPacketBytes = new byte[4];

            initPacketBytes[0] = 0x02; // STX
            initPacketBytes[1] = 16;   // Controller "display count" register
            initPacketBytes[2] = (byte)m_LEDReadout.Length;
            initPacketBytes[3] = 0x03; // ETX

            foreach (IDataStream ids in m_DataStreams)
                ids.Send(initPacketBytes);
        }

        /// <summary>
        /// Writes a decimal number to all displays identifying which display is which
        /// </summary>
        public void BeginIdentifyDisplaysToUser()
        {
            m_UserIdInProgress = true;

            // TODO: Send down display IDs
        }

        public void EndIdentifyDisplaysToUser()
        {
            m_UserIdInProgress = false;
        }

        public void UpdateDevice(IDataStream p_DataStream)
        {
            byte[] packet = new byte[m_LEDReadout.Length + 3];
            int i = 0;

            byte[][] readoutdata = new byte[m_LEDReadout.Length][];

            for (i = 0; i < m_LEDReadout.Length; i++)
                readoutdata[i] = System.Text.Encoding.ASCII.GetBytes(m_LEDReadout[i]);

            // Packet format for decimal readout:
            // [STX][REG][D(n)][D(n-1)]...[D(1)][ETX]
            //
            // REG 0 = No-op
            // REG 1 = left-most digit
            // ...
            // REG 8 = right-most digit
            // REG 9-15 are as defined in MAX7219 documentation
            // REG 16 is an extenstion that tells the controller how many displays are attached
            //     NOTE: Reg 16 MUST BE written to before sending any display data!

            packet[0] = 0x02; // STX
            packet[packet.Length - 1] = 0x03; // ETX

            // Since data is shifted out to daisychained MAX7219's, we have to transmit
            // one packet for each column of the display. Each column packet will contain
            // data for every display in the daisychain.

            for (int column = 1; column <= 8; column++)
            {
                packet[1] = (byte)column; // Address registers 1-8 for column number
                i = 2;

                for (int disp = m_LEDReadout.Length - 1; disp >= 0; disp--)
                {
                    byte db, dbOut;
                    
                    // Right-pad any short readouts with spaces
                    if (m_LEDReadout[disp].Length >= column)
                        db = (byte)m_LEDReadout[disp][column - 1];
                    else
                        db = (byte)' ';
                    
                    // Readout register format is identical to MAX7219 with BCODE enabled
                    // 0x00 = '0'
                    // 0x01 = '1'
                    // ...
                    // 0x09 = '9'
                    // 0x0A = '-'
                    // 0x0B = 'E'
                    // 0x0C = 'H'
                    // 0x0D = 'L'
                    // 0x0E = 'P'
                    // 0x0F = ' '
                    //
                    // Set MSB (0x80) if decimal point should be turned on for digit

                    dbOut = 0x00;

                    if (db == ' ')
                        dbOut = 15;
                    else if (db == '-')
                        dbOut = 10;
                    else if (db == 'E')
                        dbOut = 11;
                    else if (db == 'H')
                        dbOut = 12;
                    else if (db == 'L')
                        dbOut = 13;
                    else if (db == 'P')
                        dbOut = 14;
                    else
                        dbOut = (byte)(db - (byte)'0');

                    // Apply decimal point from precision indicator

                    if (IsTimeReadout(m_LEDVars[disp]))
                    {
                        if ((column == 2 || column == 4 || column == 6) && m_LEDReadout[disp].Trim() != "")
                            dbOut |= 0x80;
                    }
                    else
                    {
                        if ((m_LEDDecimalPlaces[disp] > 0 && column == (8 - m_LEDDecimalPlaces[disp])) && m_LEDReadout[disp].Trim() != "")
                            dbOut |= 0x80;
                    }

                    packet[i++] = dbOut;
                }

                //p_Mono.Debug("LEDJeb is sending {0} bytes of data: {1}", packet.Length, System.Text.Encoding.ASCII.GetString(packet));

                try
                {
                    p_DataStream.Send(packet);
                }
                catch (Exception ex)
                {
                    // Catch is redundant as Send() should handle any exception
                    LEDJebBehavior.LogError("Unhandled exception in IDataStream.Send(): {0}", ex.Message);
                }
            }
        }

        private bool IsTimeReadout(DisplayVar p_Var)
        {
            return (p_Var == DisplayVar.ApoapsisETA || p_Var == DisplayVar.FlightMissionTime ||
                p_Var == DisplayVar.ManeuverBurnTime || p_Var == DisplayVar.ManeuverETA ||
                p_Var == DisplayVar.OrbitalPeriod || p_Var == DisplayVar.PeriapsisETA ||
                p_Var == DisplayVar.TargetETA);
        }

        private string FormatFlightVar(int p_DisplayIndex, LEDJebFlightData p_FlightData)
        {
            DisplayVar sourceVar = m_LEDVars[p_DisplayIndex - 1];
            double valueOfVar = p_FlightData.FromEnum(sourceVar);

            if (IsTimeReadout(sourceVar))
                return FormatTime(valueOfVar);
            else
                return FormatDistance(valueOfVar * (Math.Pow(10, m_LEDDecimalPlaces[p_DisplayIndex - 1])));
        }

        private string FormatDistance(double p_Distance)
        {
            if (double.IsNaN(p_Distance))
                return "        ";
            else if (p_Distance < -99999999)
                return "-     0L";
            else if (p_Distance < 0)
                return String.Format("-{0,7:0}", -p_Distance);
            else if (p_Distance <= 99999999)
                return String.Format("{0,8:0}", p_Distance);
            else if (p_Distance <= 99999999D * 1000D)
                return String.Format("{0,8:0}", p_Distance / 1000);
            else if (p_Distance <= 99999999D * 1000D * 1000D)
                return String.Format("{0,8:0}", p_Distance / (1000 * 1000));
            else
                return "        ";
        }

        /// <summary>
        /// Takes a typical KSP time (seconds) and formats it into
        /// a raw string in the format DDHHMMSS
        /// </summary>
        /// <param name="p_Time"></param>
        /// <returns></returns>
        private string FormatTime(double p_Time)
        {
            if (double.IsNaN(p_Time))
                return "        ";

            bool tMinus = p_Time < 0;

            if (tMinus)
                p_Time = -p_Time;

            // Sort of "calms" time down a bit
            p_Time = Math.Floor(p_Time);

            int seconds = (int)p_Time % 60;
            int minutes = ((int)p_Time / 60) % 60;
            int hours = ((int)p_Time / 3600) % 60;
            int days = ((int)p_Time / 3600 * 24);

            if (tMinus)
            {
                if (days > 0)
                    return String.Format("--{0:00}{1:00}{2:00}", days, hours, minutes);
                else
                    return String.Format("- {0:00}{1:00}{2:00}", hours, minutes, seconds);

            }
            else
            {
                if (days > 99)
                    return String.Format("--{0:00}{1:00}{2:00}", hours, minutes, seconds);
                else
                    return String.Format("{0:00}{1:00}{2:00}{3:00}", days, hours, minutes, seconds);
            }
        }
    }
}
