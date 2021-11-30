using HumJ.Iot.Gu256x128c.Enums;

namespace HumJ.Iot.Gu256x128c
{
    public static class WindowDisplaySettingCommands
    {
        /// <summary>
        /// Select current window
        /// </summary>
        /// <param name="device">GU256x128c device</param>
        /// <param name="define">Window define</param>
        public static void CurrentWndowSelect(this Gu256x128c device, WindowDefine define)
        {
            var a = (byte)define;
            device.WriteBytes(new byte[] { 0x1F, 0x28, 0x77, 0x01, a });
        }

        /// <summary>
        /// Define or Cancel User Window
        /// </summary>
        /// <param name="device">GU256x128c device</param>
        /// <param name="a">Definable window number</param>
        /// <param name="b">Define or Cancel</param>
        /// <param name="xP">Left position of window (by 1dot)</param>
        /// <param name="yP">Top position of window (by 8dot) </param>
        /// <param name="xS">X size of window (by 1dot)</param>
        /// <param name="yS">Y size of window (by 8dot) </param>
        public static void UserWindowDefinitionCancel(this Gu256x128c device, byte a, byte b, ushort xP, ushort yP, ushort xS, ushort yS)
        {
            var xPL = (byte)xP;
            var xPH = (byte)(xP >> 8);
            var yPL = (byte)yP;
            var yPH = (byte)(yP >> 8);
            var xSL = (byte)xS;
            var xSH = (byte)(xS >> 8);
            var ySL = (byte)yS;
            var ySH = (byte)(yS >> 8);

            device.WriteBytes(new byte[] { 0x1F, 0x28, 0x77, 0x02, a, b, xPL, xPH, yPL, yPH, xSL, xSH, ySL, ySH });
        }
    }
}