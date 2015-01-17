/*
 * LEDJeb frmVirtualLEDs
 * 
 * Author: heaton84
 * Date:   1/10/2015
 * Brief:  Emulates a bunch of MAX7219's daisychained together
 *         Provides a testbed for driver development, and also is
 *         a freebee for those who don't wish to invest in hardware
 *         
 * TODO List:
 * 
 * 1. Remember last position
 * 2. Support resizing so it's not fixed to 1024x768
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Net;
using System.Net.Sockets;

namespace LEDJebVirtualPanel
{
    public partial class frmVirtualLEDs : Form
    {
        protected Socket m_ListenerSocket = null;
        protected Socket m_HandlerSocket = null;

        protected System.Text.Encoding m_Encoding = System.Text.Encoding.GetEncoding("ISO-8859-1");

        private int m_DisplayCount = 0;

        private bool m_DebugFlag = false;
        protected System.IO.StreamWriter m_LogStream = null;

        public frmVirtualLEDs()
        {
            InitializeComponent();

            try
            {
                m_DebugFlag = bool.Parse(System.Configuration.ConfigurationManager.AppSettings["debug"]);
            }
            catch (Exception)
            {
                MessageBox.Show("Warning: I did not understand the \"debug\" setting in the configuration file. Valid values are \"true\" and \"false\".", "LEDJeb Virtual Panel");
            }

            m_LogStream = new System.IO.StreamWriter("LEDJebVirtualPanelLog.txt", false);

            int display_count = WireUpAndCountDisplays();

            FakeInitPacket(display_count);

            InitSockets();
        }

        public int WireUpAndCountDisplays()
        {
            int display_count = 1;

            try
            {
                // Automagically wire up controller
                // "root" display is display1. Data is clocked in here, then shifted out towards display 16.
                MAX7219Display lbl = display1;

                lbl.DoubleClick += new EventHandler(displayX_DoubleClick);

                while (lbl.NextInChain != null)
                {
                    display_count++;
                    lbl = lbl.NextInChain;
                    lbl.DoubleClick += new EventHandler(displayX_DoubleClick);
                }

                LogDebug("WireUpAndCountDisplays()  display_count = {0}", display_count);
            }
            catch (Exception ex)
            {
                LogError("WireUpAndCountDisplays()  {0}{1}", ex.Message, ex.StackTrace);
            }

            return display_count;
        }

        public void InitSockets()
        {
            string IPAddy = System.Configuration.ConfigurationManager.AppSettings["IP Address"];
            string Port = System.Configuration.ConfigurationManager.AppSettings["Port"];

            IPAddress listenerAddress;
            int listenerPort;

            LogDebug("InitSockets()  config ip=\"{0}\" port=\"{1}\"", IPAddy, Port);

            try
            {
                if (IPAddy == "0.0.0.0")
                    listenerAddress = IPAddress.Any;
                else
                    listenerAddress = IPAddress.Parse(IPAddy);
            }
            catch (Exception ex)
            {
                LogError("InitSocket()  bad address, using any. {0}{1}", ex.Message, ex.StackTrace);

                listenerAddress = IPAddress.Any;
            }

            if (!Int32.TryParse(Port, out listenerPort))
                listenerPort = 5155;

            if (listenerAddress == IPAddress.Any)
                LogDebug("InitSockets()  using ip=Any port={0}", listenerPort);
            else
                LogDebug("InitSockets()  using ip={0} port={1}", listenerAddress.ToString(), listenerPort);


            // Spin up socket listener

            m_ListenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint ipep = new IPEndPoint(listenerAddress, listenerPort);
            m_ListenerSocket.Bind(ipep);
            m_ListenerSocket.Listen(100);

            LogDebug("InitSockets()  BeginAccept");
            m_ListenerSocket.BeginAccept(new AsyncCallback(ConnectionAccept), m_ListenerSocket);
        }

        /// <summary>
        /// Tells controller logic that there are 16 display modules
        /// Initializes all display modules (virtual MAX7219's) per manufacturer's spec
        /// Blanks all readouts
        /// </summary>
        public void FakeInitPacket(int display_count)
        {
            byte[] initPacketBytes = new byte[] { 0x02, 16, (byte)display_count, 0x03 };

            ConsumePacket(m_Encoding.GetString(initPacketBytes));

            // INITIALIZE DISPLAYS

            MacroWriteAllDisplays(0x0B, 8);     // Set scan limit = 8
            MacroWriteAllDisplays(0x0C, 1);     // Disable shutdown mode
            MacroWriteAllDisplays(0x0A, 0x0F);  // Full intensity

            // BLANK ALL READOUTS

            for (int column = 1; column <= 8; column++)
                MacroWriteAllDisplays((byte)column, 0x0F);
        }

        public void ConnectionAccept(IAsyncResult state)
        {
            try
            {
                LogDebug("ConnectionAccept()  Incoming connection request");

                Socket listener = (Socket)state.AsyncState;
                m_HandlerSocket = listener.EndAccept(state);

                IPEndPoint src = (IPEndPoint)m_HandlerSocket.RemoteEndPoint;

                LogDebug("ConnectionAccept()  Connection established from {0}:{1}", src.Address.ToString(), src.Port);
                tssStatus.Text = "Connected to game at " + src.Address.ToString() + ":" + src.Port.ToString();
            }
            catch (Exception ex)
            {
                LogError("ConnectionAccept()  {0}{1}", ex.Message, ex.StackTrace);

                m_ListenerSocket.BeginAccept(new AsyncCallback(ConnectionAccept), m_ListenerSocket);
            }
        }

        public void ConsumePacket(string p_Packet)
        {
            // Packet layout:

            // STX
            // CMD_char
            //    [0-15] = Write MAX7219 register for every display, 1 byte for each display to follow
            //    [16] = Display count
            // ETX

            try
            {
                if (p_Packet.Length < 3)
                {
                    LogDebug("ConsumePacket()  Too short ({0})", p_Packet.Length);
                    tssStatus.Text = "Receive error: Packet too short";
                }
                else
                {
                    byte[] data = m_Encoding.GetBytes(p_Packet);

                    if (data[0] != 0x02)
                    {
                        LogDebug("ConsumePacket()  Not an STX ({0})", data[0]);
                        tssStatus.Text = "Receive error: No STX at byte 0!";
                    }
                    else
                    {
                        if (data[data.Length - 1] != (byte)0x03)
                        {
                            LogDebug("ConsumePacket()  Not an ETX ({0})", data[data.Length - 1]);
                            tssStatus.Text = "Receive error: No ETX at end of packet!";
                        }
                        else
                        {
                            if (data[1] <= 15)
                            {
                                for (int i = 1; i <= m_DisplayCount; i++)
                                {
                                    display1.ClockData(data[1]); // address
                                    display1.ClockData(data[i + 1]); // data
                                }

                                display1.LoadData(); // loads all displays
                            }
                            else
                            {
                                switch (data[1])
                                {
                                    case 16:
                                        m_DisplayCount = data[2];
                                        break;
                                    default:
                                        tssStatus.Text = "Unknown command: " + Convert.ToString((int)data[1]);
                                        LogDebug("ConsumePacket()  not a command: {0}", data[1]);
                                        break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogError("ConsumePacket()  {0}{1}", ex.Message, ex.StackTrace);
            }
        }

        private void MacroWriteAllDisplays(byte register, byte value)
        {
            byte[] packet = new byte[m_DisplayCount + 3];

            packet[0] = 0x02; // STX
            packet[1] = register;
            packet[packet.Length - 1] = 0x03; // ETX

            // Fill in value
            for (int i = 2; i < packet.Length - 1; i++)
                packet[i] = value;

            ConsumePacket(m_Encoding.GetString(packet));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // INITIALIZE DISPLAYS

            MacroWriteAllDisplays(0x0B, 8);  // Set scan limit = 8
            MacroWriteAllDisplays(0x0C, 1);  // Disable shutdown mode
            MacroWriteAllDisplays(0x0A, 0x0F);  // Full intensity

            // BLANK ALL READOUTS

            for (int column = 1; column <= 8; column++)
                MacroWriteAllDisplays((byte)column, 0x0F);

            // IDENTIFY DISPLAYS AS 01 TO 16
            byte[] identifier = new byte[m_DisplayCount + 3];

            identifier[0] = 0x02;
            identifier[identifier.Length - 1] = 0x03;

            identifier[1] = 8; // Address column 8
            
            for (int i = 1; i <= m_DisplayCount; i++)
            {
                identifier[i + 1] = (byte)((m_DisplayCount - i + 1) % 10);
            }

            ConsumePacket(m_Encoding.GetString(identifier));

            identifier[1] = 7; // Address column 7

            for (int i = 1; i <= m_DisplayCount; i++)
            {
                identifier[i + 1] = (byte)((m_DisplayCount - i + 1) / 10);
            }

            ConsumePacket(m_Encoding.GetString(identifier));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int column = 1; column <= 8; column++)
                MacroWriteAllDisplays((byte)column, 0x0F);
        }

        private void tmrCheckData_Tick(object sender, EventArgs e)
        {
            try
            {
                if (m_HandlerSocket != null)
                {
                    if (!m_HandlerSocket.Connected)
                    {
                        // Bump timer, reconnect after 10 passes
                    }

                    while (m_HandlerSocket.Available >= m_DisplayCount + 3)
                    {
                        byte[] buffer = new byte[m_DisplayCount + 3];

                        int chars = m_HandlerSocket.Receive(buffer);

                        string packet = m_Encoding.GetString(buffer, 0, chars);

                        LogDebug("tmrCheckData_Tick() Got {0} bytes", chars);

                        ConsumePacket(packet);

                        tssStatus.Text = String.Format("{0:HH:mm:ss} read {1} chars from game / {2} waiting", DateTime.Now, chars, m_HandlerSocket.Available);
                    }
                }
            }
            catch (SocketException sckEx)
            {
                // Shut down socket, let's event handler resume game connections

                try
                {
                    LogError("tmrCheckData_Tick()  Socket error {0} at {1}", sckEx.ErrorCode, sckEx.StackTrace);

                    m_HandlerSocket.Close();
                    m_HandlerSocket = null;

                    LogDebug("tmrCheckData_Tick()  BeginAccept()");
                    m_ListenerSocket.BeginAccept(new AsyncCallback(ConnectionAccept), m_ListenerSocket);
                }
                catch (Exception ex)
                {
                    LogError("tmrCheckData_Tick()  Fatal socket tear-down error {0}{1}", ex.Message, ex.StackTrace);
                }
            }
            catch (Exception ex)
            {
                LogError("tmrCheckData_Tick()  {0}{1}", ex.Message, ex.StackTrace);
            }
        }

        private void frmVirtualLEDs_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (m_LogStream != null)
                {
                    m_LogStream.Flush();
                    m_LogStream.Close();
                    m_LogStream.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fatal error closing program log: " + ex.Message, "LEDJeb Virtual Panel");
            }
        }

        private void lblDistanceToTarget_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int column = 1; column <= 8; column++)
            {
                if (column % 2 == 0)
                    MacroWriteAllDisplays((byte)column, 0x88); // Sets decimal point as well
                else
                    MacroWriteAllDisplays((byte)column, 8);
            }
        }

        private void configToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void PickColor(MAX7219Display p_DisplayTarget)
        {
            DialogResult dr = colorDialog.ShowDialog(this);

            if (dr == DialogResult.OK)
            {
                p_DisplayTarget.ColorLight = colorDialog.Color;
                p_DisplayTarget.ColorDark = p_DisplayTarget.ColorBackground = Color.FromArgb(p_DisplayTarget.ColorLight.R / 8, p_DisplayTarget.ColorLight.G / 8, p_DisplayTarget.ColorLight.B / 8);
            }

        }

        private void displayX_DoubleClick(object sender, EventArgs e)
        {
            MAX7219Display display = null;

            if (sender is MAX7219Display)
                display = sender as MAX7219Display;
            else
                display = ((Control)sender).Parent as MAX7219Display;

            PickColor(display);
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            testToolStripMenuItem.Checked = !testToolStripMenuItem.Checked;

            if (testToolStripMenuItem.Checked)
            {
                for (int i = 1; i <= 8; i++)
                    MacroWriteAllDisplays((byte)i, (byte)i);
            }
            else
            {
                for (int i = 1; i <= 8; i++)
                    MacroWriteAllDisplays((byte)i, (byte)15);
            }
        }

        private void LogDebug(string p_Msg, params object[] p_Args)
        {
            if (m_DebugFlag)
                m_LogStream.WriteLine(String.Format(p_Msg, p_Args));
        }

        private void LogError(string p_Msg, params object[] p_Args)
        {
            m_LogStream.WriteLine("### " + String.Format(p_Msg, p_Args));
        }
    }
}
