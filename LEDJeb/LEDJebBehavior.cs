/*
 * LEDJeb Plugin Core
 * 
 * Author: heaton84
 * Date:   1/10/2015
 * Brief:  Sends flight data to external interface via socket or COM port
 * 
 * TODO List:
 * 
 * 1. (SEVERE) Mod crashes KSP when KSP is shut down. Not sure yet why this is.
 * 2. Provide configuration for:
 *      Update write (currently fixed to 100ms)
 *      TCP port number (currently fixed to 5155)
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Timers;

namespace LEDJeb
{
    [KSPAddon(KSPAddon.Startup.Flight, true)]
    public class LEDJebBehavior : UnityEngine.MonoBehaviour
    {
        protected Timer m_UpdateTimer = null;
        protected LEDPanel m_Panel = null;

        protected SocketDataStream m_SDS = null;

        protected bool m_RunFlag = false;

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

            // Set up inteface to virtual panel,
            // currently hard-coded to 16 readouts
            m_Panel = new LEDPanel();
            m_Panel.Initialize(16);
        }

        /// <summary>
        /// Invoked when we enter Flight
        /// </summary>
        public void Start()
        {
            print("LEDJebBehavior.Start() invoked");

            try
            {
                // Use port 5155 for data transfer
                // TODO: Allow this to be configurable

                m_SDS = new SocketDataStream();
                m_Panel.RegisterDevice(m_SDS);
                m_SDS.Initialize(5155);
            }
            catch (Exception ex)
            {
                print("LEDJebBehavior.Start() **** ERROR: Could not connect to client: " + ex.Message);
            }

            m_RunFlag = true;
            m_UpdateTimer.Interval = 100;
            m_UpdateTimer.Enabled = true;

            
        }

        /// <summary>
        /// Called when exiting game
        /// </summary>
        public void OnDestroy()
        {
            print("LEDJebBehavior.OnDestroy() invoked");

            m_RunFlag = false;
            m_UpdateTimer.Enabled = false;
            m_UpdateTimer = null;
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
    }
}
