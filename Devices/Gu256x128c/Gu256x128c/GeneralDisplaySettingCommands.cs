using HumJ.Iot.Gu256x128c.Enums;

namespace HumJ.Iot.Gu256x128c
{
    public static class GeneralDisplaySettingCommands
    {
        /// <summary>
        /// Select the visual quality of horizontal scroll
        /// <para>Default = <see cref="ScrollPriority.ScrollSpeed"/></para>
        /// </summary>
        /// <param name="device">GU256x128c device</param>
        public static void HorizontalScrollQualitySelect(this Gu256x128c device, ScrollPriority priority)
        {
            var n = (byte)priority;
            device.WriteBytes(new byte[] { 0x1F, 0x60, n });
        }

        /// <summary>
        /// Specifies or cancels reverse display
        /// <para>Default = <see cref="false"/></para>
        /// </summary>
        /// <param name="device">GU256x128c device</param>
        public static void SpecifiesOrCancelsReverseDisplay(this Gu256x128c device, bool specify)
        {
            var n = (byte)(specify ? 1 : 0);
            device.WriteBytes(new byte[] { 0x1F, 0x72, n });
        }

        /// <summary>
        /// Specifies write mixture mode. The new character or raphic image display mixed with current display image stored in display memory is overwritten to the display memory.
        /// <para>Default = <see cref="MixtureMode.Normal"/></para>
        /// </summary>
        /// <param name="device">GU256x128c device</param>
        public static void SpecifiesWriteMixtureDisplayMode(this Gu256x128c device, MixtureMode mode)
        {
            var n = (byte)mode;
            device.WriteBytes(new byte[] { 0x1F, 0x77, n });
        }
    }
}