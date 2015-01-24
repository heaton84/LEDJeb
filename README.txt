*************************************************
*               INSTALLING PLUGIN               *
*************************************************

1. Copy LEDJeb\bin\Debug\LEDJeb.dll to your KSP Plugins folder.

   For Steam users, this is at C:\Program Files (x86)\Steam\SteamApps\common\Kerbal Space Program\Plugins


*************************************************
*                 USING THE MOD                 *
*************************************************

Make sure to launch the virtual LED panel BEFORE starting KSP.
It is located at LEDJebVirtualPanel\bin\Debug\LEDJebVirtualPanel.exe

It is recommended to use this on a dual-monitor setup. Move the
window to your non-KSP monitor and start KSP.

The displays will come to life once you enter flight mode
(either go to the launch pad or switch to an existing flight
via the tracking station)

Do NOT launch the virtual panel from inside a zip folder. The panel needs
LEDJebVirtualPanel.exe.config present in the same folder to read the
debug settings.



*************************************************
*               TROUBLESHOOTING                 *
*************************************************

Client: Edit LEDJebVirtualPanel.exe.config and set debug flag to true:

<add key="debug" value="false" />
change to
<add key="debug" value="true" />


Plugin: Edit Plugins\PluginData\LEDJeb\config.xml to change
    <int name="DebugMode">0</int>
change to
    <int name="DebugMode">1</int>

Note: This is the 3rd line in the file. If the line is not there,
delete config.xml, relaunch the game, quit at the main menu, then
go back to the config.xml file and the settings should be there.

*************************************************
*                     NOTES                     *
*************************************************

Currently, the following readouts are not available:

1. Burn time remaining
2. Time to intercept
3. Relative inclination is wrong

TODO List:

* Display chain init on reconnect
* In-game UI for display variable assignment
* Readout scaling (keep in mind this needs to work with raw hardware, open to ideas)
* Figure out proper ascending node calculation
* Figure out burn time remaining
* Figure out time to intercept
* Periapsis shows way negative on launch
* Velocity reads wrong on launch (does not match navball)
* Draggable/sizable LED readouts on client


*************************************************
*               VERSION HISTORY                 *
*************************************************

Date        Ver #  Author       Changes
----------  -----  -----------  -----------------------------
01/22/2015  0.3    heaton84     Fixed scaling issues in Linux Mono
				Added customization of LED readouts (shear/thickness, color, etc.)
                                Added saving/loading of configuration

01/17/2015  0.21   heaton84     Added debug logging and IP options

01/11/2015  0.2    heaton84     Added decimal points on client app
                                Removed dependency on 7-segment font
                                Added config hooks
                                Fixed KSP crashing on close

01/10/2015  0.1    heaton84     Alpha Release