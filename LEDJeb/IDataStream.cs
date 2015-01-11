using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LEDJeb
{
    /// <summary>
    /// Wraps a socket for serial port
    /// </summary>
    public interface IDataStream
    {
        void Send(byte[] data);
    }
}
