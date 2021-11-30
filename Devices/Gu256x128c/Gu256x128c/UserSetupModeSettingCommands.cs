using HumJ.Iot.Gu256x128c.Enums;

namespace HumJ.Iot.Gu256x128c
{
    public static class UserSetupModeSettingCommands
    {
        /// <summary>
        /// Start User set up mode start.
        /// </summary>
        /// <param name="device">GU256x128c device</param>
        public static void UserSetupModeStart(this Gu256x128c device)
        {
            device.WriteBytes(new byte[] { 0x1F, 0x28, 0x65, 0x01, 0x49, 0x4E });
        }

        /// <summary>
        /// End User set up mode end.
        /// <para>* Valid at the user setup mode.</para>
        /// </summary>
        /// <param name="device">GU256x128c device</param>
        public static void IoPortInputOutputSetting(this Gu256x128c device, byte n, PortMode mode)
        {
            device.WriteBytes(new byte[] { 0x1F, 0x28, 0x65, 0x02, 0x4F, 0x55, 0x54 });
        }
    }
}