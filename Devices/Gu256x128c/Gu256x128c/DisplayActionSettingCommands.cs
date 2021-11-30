namespace HumJ.Iot.Gu256x128c
{
    public static class DisplayActionSettingCommands
    {
        /// <summary>
        /// Data processing are stopped while waiting by this command.
        /// </summary>
        /// <param name="device">GU256x128c device</param>
        /// <param name="t">Wait time (x approx. 0.5sec)</param>
        public static void Wait(this Gu256x128c device, byte t)
        {
            device.WriteBytes(new byte[] { 0x1F, 0x28, 0x61, 0x01, t });
        }

        /// <summary>
        /// Data processing are stopped while waiting by this command.
        /// </summary>
        /// <param name="device">GU256x128c device</param>
        /// <param name="t">Wait time (x approx. 16msec)</param>
        public static void ShortWait(this Gu256x128c device, byte t)
        {
            device.WriteBytes(new byte[] { 0x1F, 0x28, 0x61, 0x02, t });
        }

        /// <summary>
        /// Shift the display screen. Horizontal display screen scrolling can be possible by this command.
        /// </summary>
        /// <param name="device">GU256x128c device</param>
        /// <param name="w">Number of Display screen shift</param>
        /// <param name="c">Number of repetition</param>
        /// <param name="s">Scroll speed</param>
        public static void ScrollDisplayAction(this Gu256x128c device, ushort w, ushort c, byte s)
        {
            var wL = (byte)w;
            var wH = (byte)(w >> 8);
            var cL = (byte)c;
            var cH = (byte)(c >> 8);

            device.WriteBytes(new byte[] { 0x1F, 0x28, 0x61, 0x10, wL, wH, cL, cH, s });
        }

        /// <summary>
        /// Blink display action on display screen.
        /// </summary>
        /// <param name="device">GU256x128c device</param>
        /// <param name="p">Blink pattern</param>
        /// <param name="t1">Normal display time</param>
        /// <param name="t2">Blank or Reverse display time</param>
        /// <param name="c">Number of repetition</param>
        public static void DisplayBlink(this Gu256x128c device, byte p, byte t1, byte t2, byte c)
        {
            device.WriteBytes(new byte[] { 0x1F, 0x28, 0x61, 0x11, p, t1, t2, c });
        }

        /// <summary>
        /// Curtain display action on display screen.
        /// </summary>
        /// <param name="device">GU256x128c device</param>
        /// <param name="v">Direction of Curtain action</param>
        /// <param name="s">Curtain action speed</param>
        /// <param name="p">Curtain action pattern</param>
        public static void CurtainDisplayAction(this Gu256x128c device, byte v, byte s, byte p)
        {
            device.WriteBytes(new byte[] { 0x1F, 0x28, 0x61, 0x12, v, s, p });
        }

        /// <summary>
        /// Spring display action on display screen.
        /// </summary>
        /// <param name="device">GU256x128c device</param>
        /// <param name="v">Direction of spring action</param>
        /// <param name="s">Spring action speed</param>
        /// <param name="p"> Display memory pattern address</param>
        public static void SpringDisplayAction(this Gu256x128c device, byte v, byte s, ushort p)
        {
            var pL = (byte)p;
            var pH = (byte)(p >> 8);

            device.WriteBytes(new byte[] { 0x1F, 0x28, 0x61, 0x13, v, s, pL, pH });
        }

        /// <summary>
        /// Random display action on display screen.
        /// </summary>
        /// <param name="device">GU256x128c device</param>
        /// <param name="s">Random display action speed</param>
        /// <param name="p">Display memory pattern address</param>
        public static void RandomDisplayAction(this Gu256x128c device, byte s, ushort p)
        {
            var pL = (byte)p;
            var pH = (byte)(p >> 8);

            device.WriteBytes(new byte[] { 0x1F, 0x28, 0x61, 0x14, s, pL, pH });
        }

        /// <summary>
        /// Set the display power ON or OFF
        /// <para>Default = <see cref="true"/></para>
        /// </summary>
        /// <param name="device">GU256x128c device</param>
        public static void DisplayPowerControl(this Gu256x128c device, bool powerOn)
        {
            var p = (byte)(powerOn ? 1 : 0);
            device.WriteBytes(new byte[] { 0x1F, 0x28, 0x61, 0x40, p });
        }
    }
}