using HumJ.Iot.Gu256x128c.Enums;

namespace HumJ.Iot.Gu256x128c
{
    public static class GpioControlCommands
    {
        /// <summary>
        /// Set input or output for general purpose I/O port.
        /// </summary>
        /// <param name="device">GU256x128c device</param>
        /// <param name="n">I/O port number</param>
        /// <param name="a">Set Input or Output</param>
        public static void IoPortInputOutputSetting(this Gu256x128c device, byte n, PortMode mode)
        {
            var a = (byte)mode;
            device.WriteBytes(new byte[] { 0x1F, 0x28, 0x70, 0x01, n, a });
        }

        /// <summary>
        /// Output the data to general purpose I/O port.
        /// </summary>
        /// <param name="device">GU256x128c device</param>
        public static void IoPortOutput(this Gu256x128c device, byte n, byte a)
        {
            device.WriteBytes(new byte[] { 0x1F, 0x28, 0x70, 0x10, n, a });
        }

        /// <summary>
        /// The state of a general purpose I/O port is transmitted via RS232C I/F.
        /// </summary>
        /// <param name="device">GU256x128c device</param>
        public static void IoPortInput(this Gu256x128c device, byte n)
        {
            device.WriteBytes(new byte[] { 0x1F, 0x28, 0x70, 0x20, n });
        }
    }
}