﻿/*
 * LEDJeb Plugin Core
 * 
 * Author: heaton84
 * Date:   1/10/2015
 * Brief:  Sends flight data to external interface via socket or COM port
 * 
 * TODO List:
 * 
 * 1. Provide configuration UI for:
 *      Update write (currently fixed to 100ms)
 *      TCP port number (currently fixed to 5155)
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Timers;
using KSP.IO;

namespace LEDJeb
{
    [KSPAddon(KSPAddon.Startup.Flight, true)]
    public class LEDJebBehavior : UnityEngine.MonoBehaviour
    {
        protected Timer m_UpdateTimer = null;
        protected LEDPanel m_Panel = null;

        protected SocketDataStream m_SDS = null;

        protected bool m_RunFlag = false;

        private static PluginConfiguration Config = PluginConfiguration.CreateForType<LEDJebBehavior>(null);

        /// <summary>
        /// Invoked when plugin is loaded
        /// </summary>
        public void Awake()
        {
            print("LEDJebBehavior.Awake() invoked");

            // Ask game engine not to release this class at... some point
            // Not yet sure why this is relevant, just that it is good practice.
            DontDestroyOnLoad(this);

            m_UpdateTimer = new Timer();
            m_UpdateTimer.Elapsed += new ElapsedEventHandler(UpdateTimer_Elapsed);

            Config.load();

            // Set up inteface to virtual panel,
            // currently hard-coded to 16 readouts
            m_Panel = new LEDPanel();
            m_Panel.InitializeFromConfig();
        }

        /// <summary>
        /// Invoked when we enter Flight
        /// </summary>
        public void Start()
        {
            print("LEDJebBehavior.Start() invoked");

            m_RunFlag = true;
            m_UpdateTimer.Interval = GetConfigUpdateRate();
            m_UpdateTimer.Enabled = true;            
        }

        public void OnApplicationQuit()
        {
            print("LEDJebBehavior.OnApplicationQuit() invoked");

            m_RunFlag = false;
            m_UpdateTimer.Enabled = false;

        }


        protected void UpdateTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                if (!m_RunFlag)
                    return;

                if (FlightGlobals.ready)
                {
                    LEDJebFlightData flightData = LEDJebFlightData.fetch();

                    m_Panel.LoadFlightData(flightData);
                }
                else
                    print("LEDJebBehavior.UpdateTimer  FightGlobals was NULL!");
            }
            catch (Exception ex)
            {
                print("LEDJebBehavior.UpdateTimer  " + ex.Message + ex.StackTrace);
            }
        }

        public void Debug(string p_msg, params object[] p_args)
        {
            print(String.Format(p_msg, p_args));
        }

        #region Configuration

        private static void SetDefaultConfig()
        {
            Config.SetValue("DisplayCount", 16);

            Config.SetValue("UpdateRateMS", 100);

            // Data Streams

            Config.SetValue("DataStreamCount", 1);
            
            // Type = "Socket" or "Serial"
            Config.SetValue("DataStreamType1", "Socket");

            // Address = port # for socket, device ID for serial (ie "COM1" or "/dev/ttyS1")
            Config.SetValue("DataStreamAddress1", "5155");

            // Displays

            // DisplayVarX = what to show
            Config.SetValue("DisplayVar1", "Apoapsis");
            Config.SetValue("DisplayVar2", "ApoapsisETA");
            Config.SetValue("DisplayVar3", "Periapsis");
            Config.SetValue("DisplayVar4", "PeriapsisETA");
            Config.SetValue("DisplayVar5", "Altitude");
            Config.SetValue("DisplayVar6", "OrbitalPeriod");
            Config.SetValue("DisplayVar7", "OrbitalInclination");
            Config.SetValue("DisplayVar8", "ManeuverDeltaV");
            Config.SetValue("DisplayVar9", "ManeuverETA");
            Config.SetValue("DisplayVar10", "ManeuverBurnTime");
            Config.SetValue("DisplayVar11", "TargetRelativeV");
            Config.SetValue("DisplayVar12", "TargetETA");
            Config.SetValue("DisplayVar13", "TargetDistance");
            Config.SetValue("DisplayVar14", "TargetRelativeInclination");
            Config.SetValue("DisplayVar15", "FlightVelocity");
            Config.SetValue("DisplayVar16", "FlightMissionTime");

            // DisplayDecimalX = # of decimal places to show (ignored for time readouts)
            Config.SetValue("DisplayDecimal1", 0);
            Config.SetValue("DisplayDecimal2", 0);
            Config.SetValue("DisplayDecimal3", 0);
            Config.SetValue("DisplayDecimal4", 0);
            Config.SetValue("DisplayDecimal5", 0);
            Config.SetValue("DisplayDecimal6", 0);
            Config.SetValue("DisplayDecimal7", 1);
            Config.SetValue("DisplayDecimal8", 1);
            Config.SetValue("DisplayDecimal9", 0);
            Config.SetValue("DisplayDecimal10", 0);
            Config.SetValue("DisplayDecimal11", 1);
            Config.SetValue("DisplayDecimal12", 0);
            Config.SetValue("DisplayDecimal13", 1);
            Config.SetValue("DisplayDecimal14", 1);
            Config.SetValue("DisplayDecimal15", 1);
            Config.SetValue("DisplayDecimal16", 0);

            // TODO: Scale mode for each display

            Config.save();
        }

        public static int GetConfigDisplayCount()
        {
            int count = 0;

            Config.load();

            try
            {
                count = Config.GetValue<int>("DisplayCount", -1);
            }
            catch (Exception)
            {
                FlightGlobals.print("LEDJeb: Config DisplayCount is not a parsable integer number! Defaulting to 1 display.");
                count = 1;
            }

            if (count == -1)
            {
                FlightGlobals.print("LEDJeb: Looks like config doesn't exist. Setting up default config.");
                SetDefaultConfig();

                count = Config.GetValue<int>("DisplayCount", 1);
            }

            return count;
        }

        public static int GetConfigUpdateRate()
        {
            int rate = 0;

            Config.load();

            try
            {
                rate = Config.GetValue<int>("UpdateRateMS", 1000);
            }
            catch (Exception)
            {
                FlightGlobals.print("LEDJeb: Config UpdateRateMS is not a parsable integer number! Defaulting to 1 second.");
                rate = 1000;
            }

            return rate;
        }

        public static IDataStream[] GetConfigDataStreams()
        {
            Config.load();

            List<IDataStream> streams = new List<IDataStream>(); // Do not cross them
            int streamCount = Config.GetValue<int>("DataStreamCount");
            string streamType;
            string streamAddress;

            for (int i = 1; i <= streamCount; i++)
            {
                streamType = Config.GetValue<string>("DataStreamType" + i.ToString()).ToLower();
                streamAddress = Config.GetValue<string>("DataStreamAddress" + i.ToString());

                if (streamType == "socket")
                {
                    SocketDataStream sds = new SocketDataStream();

                    sds.Initialize(Convert.ToInt32(streamAddress));

                    streams.Add(sds);
                }
                else if (streamType == "serial")
                {
                    // TODO: Create SerialDataStream class
                }
                else
                    FlightGlobals.print("LEDJeb: Unknown data stream type for config DataStreamType" + i.ToString() + "! Ignoring stream.");
            }

            return streams.ToArray();
        }

        public static LEDPanel.DisplayVar[] GetConfigDisplayVars()
        {
            int displayCount = GetConfigDisplayCount();
            LEDPanel.DisplayVar[] vars = new LEDPanel.DisplayVar[displayCount];
            string displayVar;

            for (int i = 1; i <= displayCount; i++)
            {
                displayVar = Config.GetValue<string>("DisplayVar" + i.ToString(), "");

                try
                {
                    vars[i - 1] = (LEDPanel.DisplayVar)Enum.Parse(typeof(LEDPanel.DisplayVar), displayVar);
                }
                catch (Exception ex)
                {
                    FlightGlobals.print("LEDJeb: Unknown flight variable for Display " + i.ToString() + ": " + displayVar + ", defaulting to blank " + ex.Message);
                }
            }

            return vars;
        }

        public static int[] GetConfigDisplayDecimalPlaces()
        {
            int displayCount = GetConfigDisplayCount();
            int[] decimals = new int[displayCount];

            for (int i = 1; i <= displayCount; i++)
            {
                try
                {
                    decimals[i - 1] = Config.GetValue<int>("DisplayDecimal" + i.ToString(), 0);
                }
                catch (Exception)
                {
                    FlightGlobals.print("LEDJeb: Bad or missing decimal count for for Display " + i.ToString() + ", defaulting to 0");
                    decimals[i - 1] = 0;
                }
            }

            return decimals;
        }

        #endregion
    }
}
