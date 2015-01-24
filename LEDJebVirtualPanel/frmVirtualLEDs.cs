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
 * 2. Support dynamic sizing of MAX7219 readouts & labels
 * 3. Support custom layouts
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
        private decimal m_Contrast = 32;

        private bool m_DebugFlag = false;
        protected System.IO.StreamWriter m_LogStream = null;

        protected DateTime m_LastPacketReceived = DateTime.MinValue;

        public frmVirtualLEDs()
        {
            InitializeComponent();

            InitializeDynamicLayout();

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

            MacroWriteAllDisplays(0x09, 0xFF);  // Code B on everything
            MacroWriteAllDisplays(0x0A, 0x0F);  // Full intensity
            MacroWriteAllDisplays(0x0B, 8);     // Set scan limit = 8
            MacroWriteAllDisplays(0x0C, 1);     // Disable shutdown mode
            MacroWriteAllDisplays(0x0F, 0x00);  // Turn off display test

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
                m_LastPacketReceived = DateTime.Now;
            }
            catch (Exception ex)
            {
                LogError("ConnectionAccept()  {0}{1}", ex.Message, ex.StackTrace);

                m_ListenerSocket.BeginAccept(new AsyncCallback(ConnectionAccept), m_ListenerSocket);
            }
        }

        public void ResetConnection()
        {
            try
            {
                if (m_HandlerSocket != null)
                    m_HandlerSocket.Disconnect(false);
            }
            catch (Exception)
            {

            }
            finally
            {
                m_HandlerSocket = null;
            }

            try
            {
                m_ListenerSocket.BeginAccept(new AsyncCallback(ConnectionAccept), m_ListenerSocket);
            }
            catch (Exception ex)
            {
                LogError("ResetConnection()  {0}{1}", ex.Message, ex.StackTrace);
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
                    if (DateTime.Now.Subtract(m_LastPacketReceived).TotalSeconds > 15)
                    {
                        tssStatus.Text = String.Format("{0:HH:mm:ss} Receive timeout", DateTime.Now);

                        ResetConnection();
                        return;
                    }

                    System.Diagnostics.Stopwatch gdiTimer = new System.Diagnostics.Stopwatch();
                    int byteCount = 0;

                    gdiTimer.Start();

                    while (m_HandlerSocket.Available >= m_DisplayCount + 3)
                    {
                        byte[] buffer = new byte[m_DisplayCount + 3];

                        int chars = m_HandlerSocket.Receive(buffer);

                        string packet = m_Encoding.GetString(buffer, 0, chars);

                        LogDebug("tmrCheckData_Tick() Got {0} bytes", chars);

                        ConsumePacket(packet);
                        byteCount += chars;
                        m_LastPacketReceived = DateTime.Now;
                    }

                    gdiTimer.Stop();
                    long gdiLoad = gdiTimer.ElapsedMilliseconds;

                    if (gdiLoad > 100)
                        gdiLoad = 100;

                    tssStatus.Text = String.Format("{0:HH:mm:ss} Connected to game", DateTime.Now);
                    tssGDILoad.Value = (int)gdiLoad;

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
            frmConfigReadouts config = new frmConfigReadouts();

            config.Shear = display1.DigitShear;
            config.DigitSpacing = display1.DigitSpacing;
            config.SegmentSpacing = display1.SegmentPadding;
            config.SegmentThickness = display1.SegmentWidth;

            if (config.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                MAX7219Display d = display1;

                while (d != null)
                {
                    d.DigitShear = config.Shear;
                    d.DigitSpacing = config.DigitSpacing;
                    d.SegmentPadding = config.SegmentSpacing;
                    d.SegmentWidth = config.SegmentThickness;

                    d = d.NextInChain;
                }

                SaveConfig();
            }
        }

        private void PickColor(MAX7219Display p_DisplayTarget)
        {
            DialogResult dr = colorDialog.ShowDialog(this);

            if (dr == DialogResult.OK)
            {
                p_DisplayTarget.ForeColor = colorDialog.Color;
                p_DisplayTarget.BackColor = Color.FromArgb(p_DisplayTarget.ForeColor.R / 8, p_DisplayTarget.ForeColor.G / 8, p_DisplayTarget.ForeColor.B / 8);
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
                {
                    if (i % 2 == 0)
                        MacroWriteAllDisplays((byte)i, (byte)((9 - i) | 0x80));
                    else
                        MacroWriteAllDisplays((byte)i, (byte)(9 - i));
                }
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

        private void testDatarateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Start blasting junk to all displays
        }

        private void SaveConfig()
        {
            // Uses segment config from root display
            // Uses color from each display

            Properties.Settings.Default.Contrast = m_Contrast;
            Properties.Settings.Default.DigitShear = display1.DigitShear;
            Properties.Settings.Default.DigitSpacing = display1.DigitSpacing;
            Properties.Settings.Default.SegmentPadding = display1.SegmentPadding;
            Properties.Settings.Default.SegmentWidth = display1.SegmentWidth;

            // Individual colors
            Properties.Settings.Default.Color1 = display1.ForeColor;
            Properties.Settings.Default.Color2 = display2.ForeColor;
            Properties.Settings.Default.Color3 = display3.ForeColor;
            Properties.Settings.Default.Color4 = display4.ForeColor;
            Properties.Settings.Default.Color5 = display5.ForeColor;
            Properties.Settings.Default.Color6 = display6.ForeColor;
            Properties.Settings.Default.Color7 = display7.ForeColor;
            Properties.Settings.Default.Color8 = display8.ForeColor;
            Properties.Settings.Default.Color9 = display9.ForeColor;
            Properties.Settings.Default.Color10 = display10.ForeColor;
            Properties.Settings.Default.Color11 = display11.ForeColor;
            Properties.Settings.Default.Color12 = display12.ForeColor;
            Properties.Settings.Default.Color13 = display13.ForeColor;
            Properties.Settings.Default.Color14 = display14.ForeColor;
            Properties.Settings.Default.Color15 = display15.ForeColor;
            Properties.Settings.Default.Color16 = display16.ForeColor;

            Properties.Settings.Default.Save();
        }

        private void LoadConfig()
        {
            m_Contrast = Properties.Settings.Default.Contrast;

            // Individual colors
            display1.ForeColor = Properties.Settings.Default.Color1;
            display2.ForeColor = Properties.Settings.Default.Color2;
            display3.ForeColor = Properties.Settings.Default.Color3;
            display4.ForeColor = Properties.Settings.Default.Color4;
            display5.ForeColor = Properties.Settings.Default.Color5;
            display6.ForeColor = Properties.Settings.Default.Color6;
            display7.ForeColor = Properties.Settings.Default.Color7;
            display8.ForeColor = Properties.Settings.Default.Color8;
            display9.ForeColor = Properties.Settings.Default.Color9;
            display10.ForeColor = Properties.Settings.Default.Color10;
            display11.ForeColor = Properties.Settings.Default.Color11;
            display12.ForeColor = Properties.Settings.Default.Color12;
            display13.ForeColor = Properties.Settings.Default.Color13;
            display14.ForeColor = Properties.Settings.Default.Color14;
            display15.ForeColor = Properties.Settings.Default.Color15;
            display16.ForeColor = Properties.Settings.Default.Color16;

            MAX7219Display d = display1;

            while (d != null)
            {
                SetContrast(d);

                d.DigitShear = Properties.Settings.Default.DigitShear;
                d.DigitSpacing = Properties.Settings.Default.DigitSpacing;
                d.SegmentPadding = Properties.Settings.Default.SegmentPadding;
                d.SegmentWidth = Properties.Settings.Default.SegmentWidth;
                
                d = d.NextInChain;
            }
        }

        private void SetContrast(MAX7219Display p_Display)
        {
            decimal c = m_Contrast / 255;

            p_Display.BackColor = Color.FromArgb((int)(p_Display.ForeColor.R * c), (int)(p_Display.ForeColor.G * c), (int)(p_Display.ForeColor.B * c));
        }

        private void frmVirtualLEDs_Load(object sender, EventArgs e)
        {
            LoadConfig();
            ResizeDynamicLayout();
        }

        #region Dynamic Layout

        private List<PanelLayoutItem> m_PanelLayoutItems = new List<PanelLayoutItem>();

        protected void InitializeDynamicLayout()
        {
            m_PanelLayoutItems.Add(new PanelLayoutItem(display1, label1, 1, 8, "APOAPSIS"));
            m_PanelLayoutItems.Add(new PanelLayoutItem(display2, label2, 25, 8, "TIME TO APOAPSIS"));

            m_PanelLayoutItems.Add(new PanelLayoutItem(display3, label3, 1, 22, "PERIAPSIS"));
            m_PanelLayoutItems.Add(new PanelLayoutItem(display4, label4, 25, 22, "TIME TO PERIAPSIS"));

            m_PanelLayoutItems.Add(new PanelLayoutItem(display5, label5, 1, 36, "ALTITUDE"));
            m_PanelLayoutItems.Add(new PanelLayoutItem(display6, label6, 25, 36, "ORBITAL PERIOD"));

            m_PanelLayoutItems.Add(new PanelLayoutItem(display7, label7, 1, 49, "INCLINATION"));

            m_PanelLayoutItems.Add(new PanelLayoutItem(display8, label8, 51, 8, "DELTA V REMAINING"));
            m_PanelLayoutItems.Add(new PanelLayoutItem(display9, label9, 75, 8, "TIME TO NODE"));

            m_PanelLayoutItems.Add(new PanelLayoutItem(display10, label10, 75, 22, "BURN TIME REMAINING"));

            m_PanelLayoutItems.Add(new PanelLayoutItem(display11, label11, 51, 36, "RELATIVE VELOCITY"));
            m_PanelLayoutItems.Add(new PanelLayoutItem(display12, label12, 75, 36, "TIME TO INTERCEPT"));

            m_PanelLayoutItems.Add(new PanelLayoutItem(display13, label13, 51, 49, "DISTANCE TO TARGET"));
            m_PanelLayoutItems.Add(new PanelLayoutItem(display14, label14, 75, 49, "RELATIVE INCLINATION"));

            m_PanelLayoutItems.Add(new PanelLayoutItem(display15, label15, 1, 77, "VELOCITY"));
            m_PanelLayoutItems.Add(new PanelLayoutItem(display16, label16, 25, 77, "MISSION TIME"));
        }

        protected void ResizeDynamicLayout()
        {
            foreach (PanelLayoutItem pli in m_PanelLayoutItems)
                pli.PerformLayout(this.Size);
        }

        #endregion

        private void frmVirtualLEDs_Resize(object sender, EventArgs e)
        {
            ResizeDynamicLayout();
        }
    }
}
