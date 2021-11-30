using System.IO.Ports;
using System.Linq;

namespace HumJ.Iot.Gu256x128c
{
    public class Gu256x128c
    {
        public const int DefaultBaudRate = 38400;
        public const int DefaultDataBits = 8;
        public const Parity DefaultParity = Parity.None;
        public const StopBits DefaultStopBits = StopBits.One;
        public const bool DefaultDtrEnable = true;

        internal readonly SerialPort serialPort;

        public Gu256x128c(string portName)
        {
            serialPort = new SerialPort(portName, DefaultBaudRate, DefaultParity, DefaultDataBits, DefaultStopBits)
            {
                DtrEnable = true
            };
        }

        internal void WriteString(string s)
        {
            serialPort.Write(s);
        }

        internal void WriteBytes(byte[] bytes)
        {
            serialPort.Write(bytes, 0, bytes.Length);
        }

        internal void WriteBytes(byte[] bytes, byte[] more)
        {
            var buffer = bytes.Concat(more).ToArray();
            serialPort.Write(buffer, 0, buffer.Length);
        }
    }
}