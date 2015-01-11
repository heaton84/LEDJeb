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
 * 3. Support decimal points
 * 4. ACK/NAK feedback from controller (power lost in mid-stream = no idea how many displays are there)
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
        private string[] m_LEDs;
        private List<IDataStream> m_DataStreams;

        public void Initialize(int p_LEDCount)
        {
            m_LEDs = new string[p_LEDCount];
            m_DataStreams = new List<IDataStream>();
        }

        /// <summary>
        /// Adds a logical device to the update list
        /// This will either be a TCP socket or a COM port, as wrapped in an IDataStream interface
        /// </summary>
        /// <param name="p_DataStream"></param>
        public void RegisterDevice(IDataStream p_DataStream)
        {
            m_DataStreams.Add(p_DataStream);
        }

        public void LoadFlightData(LEDJebFlightData p_FlightData)
        {
            // Map numbers to readouts
            // Orbital Data
            m_LEDs[0] = FormatDistance(p_FlightData.orbitalApoapsis);
            m_LEDs[1] = FormatTime(p_FlightData.orbitalApoapsisETA);

            m_LEDs[2] = FormatDistance(p_FlightData.orbitalPeriapsis);
            m_LEDs[3] = FormatTime(p_FlightData.orbitalPeriapsisETA);

            m_LEDs[4] = FormatDistance(p_FlightData.flightAltitude);
            m_LEDs[5] = FormatTime(p_FlightData.orbitalPeriod);
            m_LEDs[6] = FormatDistance(p_FlightData.orbitalInclanation * 10D);

            // Node/Intercept Data

            if (p_FlightData.HasManeuverNode)
            {
                // We have a node
                // TODO: When this goes negative, show as T+
                // thought:
                //  1. invert the result. 2....
                //  T- show as "- 000000", show "- 999999" on overflow
                //  T+ show as "00000000", show "99999999" on overflow

                // For burn time, refer to this code:
                // https://github.com/MuMech/MechJeb2/blob/master/MechJeb2/MechJebModuleNodeExecutor.cs

                m_LEDs[7] = FormatDistance(p_FlightData.maneuverDeltaVRemaining);
                m_LEDs[8] = FormatTime(p_FlightData.maneuverNodeETA);
                m_LEDs[9] = "        "; // todo: FormatTime(p_FlightData.maneuverBurnTimeRemaining);
            }
            else
            {
                // No node, blank out displays
                m_LEDs[7] = "        "; // delta v remain
                m_LEDs[8] = "        "; // time to node
                m_LEDs[9] = "        "; // burn time remaining
            }

            // Target Data

            if (p_FlightData.HasTargetData)
            {
                m_LEDs[10] = FormatDistance(p_FlightData.targetRelativeVelocity * 10D); // relative velocity
                m_LEDs[11] = "        "; // time to intercept
                m_LEDs[12] = FormatDistance(p_FlightData.targetDistance); // distance to target
                m_LEDs[13] = FormatDistance(p_FlightData.targetAscendingNode * 10D); // relative inclination
            }
            else
            {
                m_LEDs[10] = "        "; // relative velocity
                m_LEDs[11] = "        "; // time to intercept
                m_LEDs[12] = "        "; // distance to target
                m_LEDs[13] = "        "; // relative inclination
            }

            m_LEDs[14] = FormatDistance(p_FlightData.orbitalVelocity * 10D);    // vessel velocity
            m_LEDs[15] = FormatTime(p_FlightData.flightMissionTime);          // mission time

            //p_Mono.Debug("Loadp_FlightData: alt={0} raw={1} Idsc={2}", altitude, m_LEDs[4], m_DataStreams.Count);

            foreach (IDataStream Ids in m_DataStreams)
                UpdateDevice(Ids);
        }

        public void UpdateDevice(IDataStream p_DataStream)
        {
            byte[] packet = new byte[m_LEDs.Length + 3];
            int i = 0;

            byte[][] readoutdata = new byte[m_LEDs.Length][];

            for (i = 0; i < m_LEDs.Length; i++)
                readoutdata[i] = System.Text.Encoding.ASCII.GetBytes(m_LEDs[i]);

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

                for (int disp = m_LEDs.Length - 1; disp >= 0; disp--)
                {
                    byte db;
                    
                    // Right-pad any short readouts with spaces
                    if (m_LEDs[disp].Length >= column)
                        db = (byte)m_LEDs[disp][column - 1];
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

                    if (db == ' ')
                        packet[i++] = 15;
                    else if (db == '-')
                        packet[i++] = 10;
                    else if (db == 'E')
                        packet[i++] = 11;
                    else if (db == 'H')
                        packet[i++] = 12;
                    else if (db == 'L')
                        packet[i++] = 13;
                    else if (db == 'P')
                        packet[i++] = 14;
                    else
                        packet[i++] = (byte)(db - (byte)'0');
                }

                //p_Mono.Debug("LEDJeb is sending {0} bytes of data: {1}", packet.Length, System.Text.Encoding.ASCII.GetString(packet));

                try
                {
                    p_DataStream.Send(packet);
                }
                catch (Exception ex)
                {
                    // Loop is redundant as Send() should handle any exception

                    FlightGlobals.print(String.Format("LEDJeb: Unhandled exception in IDataStream.Send(): {0}", ex.Message));
                }
            }
        }

        private string FormatDistance(double p_Distance)
        {
            if (p_Distance < -9999)
                return "        ";
            else if (p_Distance <= 99999999)
                return String.Format("{0,8:0}", p_Distance);
            else if (p_Distance <= 99999999D * 1000D)
                return String.Format("{0,8:0}", p_Distance / 1000);
            else if (p_Distance <= 99999999D * 1000D * 1000D)
                return String.Format("{0,8:0}", p_Distance / (1000 * 1000));
            else
                return "99999999";
        }

        /// <summary>
        /// Takes a typical KSP time (seconds) and formats it into
        /// a raw string in the format DDHHMMSS
        /// </summary>
        /// <param name="p_Time"></param>
        /// <returns></returns>
        private string FormatTime(double p_Time)
        {
            if (p_Time < 0)
                return "        ";

            int seconds = (int)p_Time % 60;
            int minutes = ((int)p_Time / 60) % 60;
            int hours = ((int)p_Time / 3600) % 60;
            int days = ((int)p_Time / 3600 * 24);

            if (days > 99)
                return String.Format("--{1:00}{2:00}{3:00}", days, hours, minutes, seconds);
            else
                return String.Format("{0:00}{1:00}{2:00}{3:00}", days, hours, minutes, seconds);
        }
    }
}
