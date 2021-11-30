using HumJ.Iot.Gu256x128c.Enums;

namespace HumJ.Iot.Gu256x128c
{
    public static class GeneralSettingCommands
    {
        /// <summary>
        /// Brightness level setting
        /// <para>Default = <see cref="BrightnessLevel.Level4Of4"/></para>
        /// </summary>
        /// <param name="device">GU256x128c device</param>
        public static void BrightnessLevelSetting(this Gu256x128c device, BrightnessLevel level)
        {
            var n = (byte)level;
            device.WriteBytes(new byte[] { 0x1F, 0x58, n });
        }

        /// <summary>
        /// Clear the all display screen, and initialize all setting.
        /// </summary>
        /// <param name="device">GU256x128c device</param>
        public static void InitializeDisplay(this Gu256x128c device)
        {
            device.WriteBytes(new byte[] { 0x1B, 0x40 });
        }

        /// <summary>
        /// The cursor moves to specified X, Y position on display memory.
        /// </summary>
        /// <param name="x">Cursor position x</param>
        /// <param name="y">Cursor position y</param>
        /// <param name="device">GU256x128c device</param>
        public static void CursorSet(this Gu256x128c device, ushort x, ushort y)
        {
            var xL = (byte)x;
            var xH = (byte)(x >> 8);
            var yL = (byte)y;
            var yH = (byte)(y >> 8);

            device.WriteBytes(new byte[] { 0x1F, 0x24, xL, xH, yL, yH });
        }

        /// <summary>
        /// Display cursor ON/OFF select
        /// <para>Default = <see cref="false"/></para>
        /// </summary>
        /// <param name="display">Cursor ON</param>
        /// <param name="device">GU256x128c device</param>
        public static void CursorDisplayOnOffSelect(this Gu256x128c device, bool display)
        {
            var n = (byte)(display ? 1 : 0);
            device.WriteBytes(new byte[] { 0x1F, 0x43, n });
        }
    }
}