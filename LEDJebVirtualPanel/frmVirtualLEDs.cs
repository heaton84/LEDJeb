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

        public frmVirtualLEDs()
        {
            InitializeComponent();

            // Automagically wire up controller
            // "root" display is display1. Data is clocked in here, then shifted out towards display 16.
            MAX7219Display lbl = display1;
            int display_count = 1;

            while (lbl.NextInChain != null)
            {
                display_count++;
                lbl = lbl.NextInChain;
            }

            byte[] initPacketBytes = new byte[] { 0x02, 16, (byte)display_count, 0x03 };

            ConsumePacket(m_Encoding.GetString(initPacketBytes));

            // INITIALIZE DISPLAYS

            MacroWriteAllDisplays(0x0B, 8);     // Set scan limit = 8
            MacroWriteAllDisplays(0x0C, 1);     // Disable shutdown mode
            MacroWriteAllDisplays(0x0A, 0x0F);  // Full intensity

            // BLANK ALL READOUTS

            for (int column = 1; column <= 8; column++)
                MacroWriteAllDisplays((byte)column, 0x0F);

            // Spin up socket listener

            m_ListenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 5155);
            m_ListenerSocket.Bind(ipep);
            m_ListenerSocket.Listen(100);

            m_ListenerSocket.BeginAccept(new AsyncCallback(ConnectionAccept), m_ListenerSocket);
        }

        public void ConnectionAccept(IAsyncResult state)
        {
            Socket listener = (Socket)state.AsyncState;
            m_HandlerSocket = listener.EndAccept(state);

            IPEndPoint src = (IPEndPoint)m_HandlerSocket.RemoteEndPoint;

            tssStatus.Text = "Connected to game at " + src.Address.ToString() + ":" + src.Port.ToString();
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
                    tssStatus.Text = "Receive error: Packet too short";
                else
                {
                    byte[] data = m_Encoding.GetBytes(p_Packet);

                    if (data[0] != 0x02)
                        tssStatus.Text = "Receive error: No STX at byte 0!";
                    else
                    {
                        if (data[data.Length - 1] != (byte)0x03)
                            tssStatus.Text = "Receive error: No ETX at end of packet!";
                        else
                        {
                            if (data[1] <= 15)
                            {
                                for (int i = 1; i <= m_DisplayCount; i++)
                                {
                                    display1.ClockData(data[1]); // address
                                    display1.ClockData(data[i + 1]); // data
                                }

                                display1.Load(); // loads all displays
                            }
                            else
                            {
                                switch (data[1])
                                {
                                    case 16:
                                        m_DisplayCount = data[2];
                                        break;
                                    default:
                                        tssStatus.Text = "Unknown command: " + Convert.ToString((int)data[2]);
                                        Debug("Bad command byte {0}", data[2]);
                                        break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug("PACKET ERROR {0}", ex.Message);
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

                        Debug("Read {0} chars: {1}", chars, packet);

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
                    m_HandlerSocket.Close();
                    m_HandlerSocket = null;

                    m_ListenerSocket.BeginAccept(new AsyncCallback(ConnectionAccept), m_ListenerSocket);
                }
                catch (Exception ex)
                {

                }
            }
            catch (Exception ex)
            {
                Debug("TIMER ERROR {0}", ex.Message);
            }
        }

        private void Debug(string p_msg, params object[] p_args)
        {
            try
            {
                //log.WriteLine(String.Format(p_msg, p_args));
                //log.Flush();
            }
            catch (Exception)
            {

            }
        }

        private void frmVirtualLEDs_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*try
            {
                log.Close();
                log.Dispose();
            }
            catch (Exception)
            {

            }*/
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
    }
}
