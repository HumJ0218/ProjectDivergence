namespace HumJ.Iot.Gu256x128c
{
    public static class BitImageDisplaySettingCommands
    {
        /// <summary>
        /// Display the dot pattern on a drawing position or delete the dot pattern already displayed.
        /// </summary>
        /// <param name="device">GU256x128c device</param>
        /// <param name="dotDisplay">Dot display ON or OFF</param>
        /// <param name="x">Dot pattern drawing position x</param>
        /// <param name="y">Dot pattern drawing position y</param>
        public static void DotPatternDrawing(this Gu256x128c device, bool dotDisplay, ushort x, ushort y)
        {
            var pen = (byte)(dotDisplay ? 1 : 0);
            var xL = (byte)x;
            var xH = (byte)(x >> 8);
            var yL = (byte)y;
            var yH = (byte)(y >> 8);

            device.WriteBytes(new byte[] { 0x1F, 0x28, 0x64, 0x10, pen, xL, xH, yL, yH });
        }

        /// <summary>
        /// Display the Line, Box, Box FILL on the drawing area specified by x1,y1,x2,y2 or delete the dot pattern already displayed.
        /// </summary>
        /// <param name="device">GU256x128c device</param>
        /// <param name="mode">Drawing mode select</param>
        /// <param name="dotOn">Dot ON or OFF</param>
        /// <param name="x1"> Line/Box pattern drawing start position x1</param>
        /// <param name="y1">Line/Box pattern drawing start position y1</param>
        /// <param name="x2">Line/Box pattern drawing end position x2</param>
        /// <param name="y2">Line/Box pattern drawing end position y2</param>
        public static void LineBoxPatternDrawing(this Gu256x128c device, byte mode, bool dotOn, ushort x1, ushort y1, ushort x2, ushort y2)
        {
            var pen = (byte)(dotOn ? 1 : 0);
            var x1L = (byte)x1;
            var x1H = (byte)(x1 >> 8);
            var y1L = (byte)y1;
            var y1H = (byte)(y1 >> 8);
            var x2L = (byte)x2;
            var x2H = (byte)(x2 >> 8);
            var y2L = (byte)y2;
            var y2H = (byte)(y2 >> 8);

            device.WriteBytes(new byte[] { 0x1F, 0x28, 0x64, 0x11, mode, pen, x1L, x1H, y1L, y1H, x2L, x2H, y2L, y2H });
        }

        /// <summary>
        /// Display the bit image data on the cursor position real-time.
        /// </summary>
        /// <param name="device">GU256x128c device</param>
        /// <param name="x">Bit image X size</param>
        /// <param name="y">Bit image Y size</param>
        /// <param name="d">Image data</param>
        public static void RealtimeBitIimageDisplay(this Gu256x128c device, ushort x, ushort y, params byte[] d)
        {
            var xL = (byte)x;
            var xH = (byte)(x >> 8);
            var yL = (byte)y;
            var yH = (byte)(y >> 8);

            device.WriteBytes(new byte[] { 0x1F, 0x28, 0x66, 0x11, xL, xH, yL, yH, 0x01 }, d);
        }

        /// <summary>
        /// Define user bit image to the RAM.
        /// </summary>
        /// <param name="device">GU256x128c device</param>
        /// <param name="a">Bit image data definition address</param>
        /// <param name="s">Bit image data length</param>
        /// <param name="d">Image data</param>
        public static void RamBitImageDefinition(this Gu256x128c device, int a, int s, params byte[] d)
        {
            var aL = (byte)a;
            var aH = (byte)(a >> 8);
            var aE = (byte)(a >> 16);
            var sL = (byte)s;
            var sH = (byte)(s >> 8);
            var sE = (byte)(s >> 16);

            device.WriteBytes(new byte[] { 0x1F, 0x28, 0x66, 0x01, aL, aH, aE, sL, sH, sE }, d);
        }

        /// <summary>
        /// Define user bit image to the FROM. 
        /// <para>* Valid at the user setup mode.</para>
        /// </summary>
        /// <param name="device">GU256x128c device</param>
        /// <param name="a">Bit image data definition address</param>
        /// <param name="s">Bit image data length</param>
        /// <param name="d">Image data</param>
        public static void FromBitImageDefinition(this Gu256x128c device, int a, int s, params byte[] d)
        {
            var aL = (byte)a;
            var aH = (byte)(a >> 8);
            var aE = (byte)(a >> 16);
            var sL = (byte)s;
            var sH = (byte)(s >> 8);
            var sE = (byte)(s >> 16);

            device.WriteBytes(new byte[] { 0x1F, 0x28, 0x65, 0x10, aL, aH, aE, sL, sH, sE }, d);
        }

        /// <summary>
        /// Display the RAM or FROM bit image defined on cursor position.
        /// </summary>
        /// <param name="device"></param>
        /// <param name="m">Select bit image data display memory</param>
        /// <param name="a">Bit image data definition address</param>
        /// <param name="yS">Bit image defined, Y size (by 8dots)</param>
        /// <param name="x">Bit image display X size (by 1dot)</param>
        /// <param name="y">Bit image display Y size (by 8dots)</param>
        public static void DownloadedBitImageDisplay(this Gu256x128c device, byte m, int a, ushort yS, ushort x, ushort y)
        {
            var aL = (byte)a;
            var aH = (byte)(a >> 8);
            var aE = (byte)(a >> 16);
            var ySL = (byte)yS;
            var ySH = (byte)(yS >> 8);
            var xL = (byte)x;
            var xH = (byte)(x >> 8);
            var yL = (byte)y;
            var yH = (byte)(y >> 8);

            device.WriteBytes(new byte[] { 0x1F, 0x28, 0x66, 0x10, m, aL, aH, aE, ySL, ySH, xL, xH, yL, yH, 0x01 });
        }

        /// <summary>
        /// Scroll display the RAM, FROM or Display memory bit image defined from the right end of current window.
        /// </summary>
        /// <param name="device"></param>
        /// <param name="m">Select bit image data display memory</param>
        /// <param name="a">Bit image data definition address</param>
        /// <param name="yS">Bit image defined, Y size (by 8dots)</param>
        /// <param name="x">Bit image scroll display shift X size (by 1dot)</param>
        /// <param name="y">Bit image scroll display Y size (by 8dots)</param>
        /// <param name="s">Scroll speed</param>
        public static void DownloadedBitImageScrollDisplay(this Gu256x128c device, byte m, int a, ushort yS, ushort x, ushort y, byte s)
        {
            var aL = (byte)a;
            var aH = (byte)(a >> 8);
            var aE = (byte)(a >> 16);
            var ySL = (byte)yS;
            var ySH = (byte)(yS >> 8);
            var xL = (byte)x;
            var xH = (byte)(x >> 8);
            var yL = (byte)y;
            var yH = (byte)(y >> 8);

            device.WriteBytes(new byte[] { 0x1F, 0x28, 0x66, 0x90, m, aL, aH, aE, ySL, ySH, xL, xH, yL, yH, 0x01, s });
        }
    }
}