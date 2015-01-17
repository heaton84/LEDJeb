/*
 * LEDJeb SocketDataStream
 * 
 * Author: heaton84
 * Date:   1/10/2015
 * Brief:  Supports socket interface for sending serial list
 * 
 * TODO List:
 * 
 * 1. Send init packet upon successful connection (write reg 16 with display #)
 * 2. Work out kinks in reconnecting sockets (starting app after game is running = no data...)
 */

using System;
using System.Net;
using System.Net.Sockets;

namespace LEDJeb
{
    public class SocketDataStream : IDataStream, IDisposable
    {
        private Socket m_Socket = null;
        private int m_Port = 0;
        private IPAddress m_IPAddress = IPAddress.Loopback;

        public void Initialize(string p_ConfigAddress)
        {
            // Address should be in format IP.AD.DR.ESS:PORT, but may just be port from version 0.2 beta
            // Account for both!

            string ipAddy = "127.0.0.1";
            string port = "";

            if (p_ConfigAddress.IndexOf(':') > -1)
            {
                // IP address is specified, go git it

                ipAddy = p_ConfigAddress.Substring(0, p_ConfigAddress.IndexOf(':')).Trim();
                port = p_ConfigAddress.Substring(p_ConfigAddress.IndexOf(':') + 1).Trim();
            }
            else
            {
                port = p_ConfigAddress.Trim();
            }

            if (!Int32.TryParse(port, out m_Port))
            {
                LEDJebBehavior.LogError("SocketDataStream.Initialize()  Could not parse int from port value ({0})", port);

                m_Port = 5155; // Not a parsable number, default
            }

            if (!IPAddress.TryParse(ipAddy, out m_IPAddress))
                m_IPAddress = IPAddress.Loopback;

            if (m_IPAddress == IPAddress.Any)
            {
                LEDJebBehavior.LogError("SocketDataStream.Initialize()  IPAddress.Any detected, forcing to loopback!");

                m_IPAddress = IPAddress.Loopback; // Connect won't take kindly to IPAddress.Any
            }

            AllocateSocket();
        }

        private void AllocateSocket()
        {
            try
            {
                m_Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                IPEndPoint ipep = new IPEndPoint(m_IPAddress, m_Port);
                m_Socket.Connect(ipep);

                LEDJebBehavior.LogDebug("SocketDataStream.AllocateSocket()  Connected to {0}:{1}", m_IPAddress.ToString(), m_Port);
            }
            catch (Exception ex)
            {
                LEDJebBehavior.LogError("SocketDataStream.AllocateSocket()  {0}{1}", ex.Message, ex.StackTrace);
            }
        }

        public void Send(byte[] p_Data)
        {
            try
            {
                if (m_Socket == null)
                {
                    LEDJebBehavior.LogDebug("SocketDataStream.Send()  m_Socket is null, will allocate");

                    AllocateSocket();
                }

                if (m_Socket.Send(p_Data) < p_Data.Length || !m_Socket.Connected)
                {
                    m_Socket.Disconnect(false);
                    m_Socket = null;
                }
            }
            catch (Exception ex)
            {
                LEDJebBehavior.LogError("SocketDataStram.Send()  {0}{1}", ex.Message, ex.StackTrace);

                try
                {
                    m_Socket.Disconnect(false);
                }
                finally
                {
                    m_Socket = null;
                }
            }
        }

        public void Dispose()
        {
            if (m_Socket != null)
            {
                try
                {
                    m_Socket.Disconnect(false);
                }
                catch (Exception)
                {

                }
                finally
                {
                    m_Socket = null;
                }
            }
        }
    }
}
