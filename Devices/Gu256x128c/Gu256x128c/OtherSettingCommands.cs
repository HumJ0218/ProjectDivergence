using HumJ.Iot.Gu256x128c.Enums;
using System.IO.Ports;

namespace HumJ.Iot.Gu256x128c
{
    public static class OtherSettingCommands
    {
        private static readonly Dictionary<int, byte> BaudRateList = new Dictionary<int, byte>
        {
           // { 19200,0x00 },
            { 4800,0x01 },
            { 9600,0x02 },
            { 19200,0x03 },
            { 38400,0x04 },
            { 57600,0x05 },
            { 115200,0x06 },
        };

        private static readonly Dictionary<Parity, byte> ParityList = new Dictionary<Parity, byte>
        {
            { Parity.None,0x00 },
            { Parity.Even,0x01 },
            { Parity.Odd,0x02 },
        };

        /// <summary>
        /// Set the contents of data“b” to memory SW “a”.
        /// <para>* Valid at the user setup mode.</para>
        /// </summary>
        /// <param name="device">GU256x128c device</param>
        /// <param name="a">Memory SW number</param>
        /// <param name="b">Setting data</param>
        public static void MemorySwSetting(this Gu256x128c device, byte a, byte b)
        {
            device.WriteBytes(new byte[] { 0x1F, 0x28, 0x65, 0x03, a, b });
        }

        /// <summary>
        /// Send the contents of memory SW data “a”.
        /// </summary>
        /// <param name="device">GU256x128c device</param>
        /// <param name="a">Memory SW number</param>
        public static void MemorySwDataSend(this Gu256x128c device, byte a)
        {
            device.WriteBytes(new byte[] { 0x1F, 0x28, 0x65, 0x04, a });
        }

        // General-purpose memory store
        // General-purpose memory transfer
        // General-purpose memory send


        /// <summary>
        /// Send each display status information.
        /// </summary>
        /// <param name="device">GU256x128c device</param>
        /// <param name="name">Informarion name</param>
        /// <param name="address">Start address</param>
        /// <param name="length">Data length</param>
        public static void DisplayStatusSend(this Gu256x128c device, InformationName name, byte address, byte length)
        {
            var a = (byte)name;
            var b = address;
            var c = length;

            device.WriteBytes(new byte[] { 0x1F, 0x28, 0x65, 0x40, a, b, c });
        }

        /// <summary>
        /// Change the RS-232 serial interface communication parameters.
        /// </summary>
        public static void Rs232SerialSettings(this Gu256x128c device, int baudRate = Gu256x128c.DefaultBaudRate, Parity parity = Gu256x128c.DefaultParity)
        {
            var a = BaudRateList[baudRate];
            var b = ParityList[parity];

            device.WriteBytes(new byte[] { 0x1F, 0x28, 0x69, 0x10, a, b });

            device.serialPort.Close();
            device.serialPort.BaudRate = baudRate;
            device.serialPort.Parity = parity;
            device.serialPort.Open();
        }

        /// <summary>
        /// Shift to “Memory re-write mode” from “Normal mode”.
        /// </summary>
        /// <param name="device">GU256x128c device</param>
        public static void MemoryRewriteMode(this Gu256x128c device)
        {
            device.WriteBytes(new byte[] { 0x1C, 0x7C, 0x4D, 0xD0, 0x4D, 0x4F, 0x44, 0x45, 0x49, 0x4E });
        }
    }
}