using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LEDJeb
{
    /// <summary>
    /// Supplies all KSP flight data to the LEDJeb Mod
    /// 
    /// Feel free to re-use this code for easy access to common numerals, as some stumbling about
    /// in Maths is involved :)
    /// 
    /// Usage: LEDJebFlightData flightData = LEDJebFlightData.fetch();
    /// </summary>
    public class LEDJebFlightData
    {
        public bool HasOrbitalData;

        // Orbital Data
        public double orbitalApoapsis;
        public double orbitalApoapsisETA;

        public double orbitalPeriapsis;
        public double orbitalPeriapsisETA;

        public double orbitalVelocity;
        public double orbitalPeriod;
        public double orbitalInclanation;
        

        public bool HasFlightData;

        // Flight Data
        public int flightStage;
        public double flightMissionTime;
        public double flightAltitude;

        public bool HasManeuverNode;

        // Maneuver Node Data

        public double maneuverNodeETA;
        public double maneuverDeltaVRemaining;
        public double maneuverBurnTimeRemaining;

        public bool HasTargetData;

        // Target Data
        public double targetDistance;
        public double targetAscendingNode;
        public double targetRelativeVelocity;

        public static LEDJebFlightData fetch()
        {
            LEDJebFlightData flightData = new LEDJebFlightData();

            flightData.HasFlightData = (FlightGlobals.ActiveVessel is Vessel);

            if (flightData.HasFlightData)
            {
                flightData.HasOrbitalData = (FlightGlobals.ActiveVessel.orbit != null); // Not even sure if this is needed
                flightData.HasManeuverNode = (FlightGlobals.ActiveVessel.patchedConicSolver.maneuverNodes.Count > 0);
                flightData.HasTargetData = (FlightGlobals.ActiveVessel.targetObject is Vessel);

                /***** Flight Data *****/

                flightData.flightStage = FlightGlobals.ActiveVessel.currentStage;
                flightData.flightAltitude = FlightGlobals.ActiveVessel.altitude;
                flightData.flightMissionTime = FlightGlobals.ActiveVessel.missionTime;

                if (flightData.HasOrbitalData)
                {
                    flightData.orbitalApoapsis = FlightGlobals.ActiveVessel.orbit.ApA;
                    flightData.orbitalApoapsisETA = FlightGlobals.ActiveVessel.orbit.timeToAp;

                    flightData.orbitalPeriapsis = FlightGlobals.ActiveVessel.orbit.PeA;
                    flightData.orbitalPeriapsisETA = FlightGlobals.ActiveVessel.orbit.timeToPe;

                    flightData.orbitalVelocity = FlightGlobals.ActiveVessel.orbit.vel.magnitude;
                    flightData.orbitalPeriod = FlightGlobals.ActiveVessel.orbit.period;
                    flightData.orbitalInclanation = FlightGlobals.ActiveVessel.orbit.inclination;
                } // Orbital Data

                if (flightData.HasManeuverNode)
                {
                    flightData.maneuverNodeETA = FlightGlobals.ActiveVessel.patchedConicSolver.maneuverNodes[0].UT - Planetarium.GetUniversalTime();
                    flightData.maneuverDeltaVRemaining = FlightGlobals.ActiveVessel.patchedConicSolver.maneuverNodes[0].DeltaV.magnitude;

                    /*
                     * burn time = (m0 * ve / T) * (1 - e^(-dv/ve))
                     *    m0 = mass before burn in metric tons
                     *    ve = engine's exh. velocity (9.81 * Isp)
                     *    T = total thrust of all firing engines in kN
                     *    dv = change in velocity that will result from burn
                     *    
                     */

                    flightData.maneuverBurnTimeRemaining = 0;


                } // Maneuver Node

                if (flightData.HasTargetData)
                {
                    // Remember we've already checked that the targetObject is a Vessel above
                    Vessel targetVessel = (Vessel)FlightGlobals.ActiveVessel.targetObject;

                    Vector3d activeVesselPos = FlightGlobals.ActiveVessel.orbit.getRelativePositionAtUT(Planetarium.GetUniversalTime()) + FlightGlobals.ActiveVessel.orbit.referenceBody.position;
                    Vector3d targetVesselPos = targetVessel.orbit.getRelativePositionAtUT(Planetarium.GetUniversalTime()) + targetVessel.orbit.referenceBody.position;

                    flightData.targetDistance = (activeVesselPos - targetVesselPos).magnitude;
                    flightData.targetAscendingNode = FlightGlobals.ActiveVessel.orbit.inclination + targetVessel.orbit.inclination;

                    Vector3d activeVesselV = FlightGlobals.ActiveVessel.orbit.vel;
                    Vector3d targetVesselV = targetVessel.orbit.vel;

                    flightData.targetRelativeVelocity = (targetVesselV - activeVesselV).magnitude;
                    
                } // Target Data
            }

            return flightData;
        }
    }
}
