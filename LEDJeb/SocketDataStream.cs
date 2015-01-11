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

        public void Initialize(int p_Port)
        {
            m_Port = p_Port;

            AllocateSocket();
        }

        private void AllocateSocket()
        {
            m_Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint ipep = new IPEndPoint(IPAddress.Loopback, m_Port);
            m_Socket.Connect(ipep);
        }

        public void Send(byte[] p_Data)
        {
            try
            {
                if (m_Socket == null)
                    AllocateSocket();

                if (m_Socket.Send(p_Data) < p_Data.Length || !m_Socket.Connected)
                {
                    m_Socket.Disconnect(false);
                    m_Socket = null;
                }
            }
            catch (Exception)
            {
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
