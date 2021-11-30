using HumJ.Iot.Gu256x128c.Enums;

namespace HumJ.Iot.Gu256x128c
{
    public static class CharacterDisplaySettingCommands
    {
        /// <summary>
        /// Select the write screen mode for base window.
        /// <para>Default = <see cref="ScreenMode.DisplayScreenMode"/></para>
        /// </summary>
        /// <param name="device">GU256x128c device</param>
        public static void WriteScreenModeSelect(this Gu256x128c device, ScreenMode mode)
        {
            var a = (byte)mode;
            device.WriteBytes(new byte[] { 0x1F, 0x28, 0x77, 0x10, a });
        }

        /// <summary>
        /// Some characters located on 20h-7Fh are chosen/replaced from 14 types font set.
        /// <para>Default = <see cref="FontSet.America"/></para>
        /// </summary>
        /// <param name="device">GU256x128c device</param>
        public static void SpecifiesInternationalFontSet(this Gu256x128c device, FontSet set)
        {
            var n = (byte)set;
            device.WriteBytes(new byte[] { 0x1B, 0x52, n });
        }

        /// <summary>
        /// The fonts located on 80h-FFh of font table are chosen/Replaced from 10 types font set.
        /// <para>Default = <see cref="CodeType.PC437"/></para>
        /// </summary>
        /// <param name="type"></param>
        /// <param name="device">GU256x128c device</param>
        public static void SpecifiesCharacterCodeType(this Gu256x128c device, CodeType type)
        {
            var n = (byte)type;
            device.WriteBytes(new byte[] { 0x1B, 0x74, n });
        }

        /// <summary>
        /// Set to Over-write mode.
        /// </summary>
        /// <param name="device">GU256x128c device</param>
        public static void OverwriteMode(this Gu256x128c device)
        {
            device.WriteBytes(new byte[] { 0x1F, 0x01 });
        }

        /// <summary>
        /// Set to Vertical scroll mode. 
        /// </summary>
        /// <param name="device">GU256x128c device</param>
        public static void VerticalScrollMode(this Gu256x128c device)
        {
            device.WriteBytes(new byte[] { 0x1F, 0x02 });
        }

        /// <summary>
        /// Set to horizontal scroll mode. 
        /// </summary>
        /// <param name="device">GU256x128c device</param>
        public static void HorizontalScrollMode(this Gu256x128c device)
        {
            device.WriteBytes(new byte[] { 0x1F, 0x03 });
        }

        /// <summary>
        /// Set to horizontal scroll speed. 
        /// <para>Default = <see cref="0x00"/></para>
        /// <para><see cref="0x00"/> = By Character</para>
        /// <para><see cref="0x01"/> = T millisecond / dots</para>
        /// <para><see cref="02H - 1FH"/> = (n - 1) x T millisecond / dot</para>
        /// <para>Scroll base speed “T” is depending on write screen mode, character size selected. </para>
        /// </summary>
        /// <param name="device">GU256x128c device</param>
        public static void HorizontalScrollSpeed(this Gu256x128c device, byte n)
        {
            device.WriteBytes(new byte[] { 0x1F, 0x73, n });
        }

        /// <summary>
        /// Select font size of a character.
        /// <para>Default = <see cref="FontSize.Font6x8"/></para>
        /// </summary>
        /// <param name="device">GU256x128c device</param>
        public static void FontSizeSelect(this Gu256x128c device, FontSize size)
        {
            var m = (byte)size;
            device.WriteBytes(new byte[] { 0x1F, 0x28, 0x67, 0x01, m });
        }

        /// <summary>
        /// Specify or cancel 2byte character mode. 
        /// <para>Default = <see cref="false"/></para>
        /// </summary>
        /// <param name="device">GU256x128c device</param>
        public static void SpecifyCancel2ByteCharacterMode(this Gu256x128c device, bool specify)
        {
            var m = (byte)(specify ? 1 : 0);
            device.WriteBytes(new byte[] { 0x1F, 0x28, 0x67, 0x02, m });
        }

        /// <summary>
        /// Select 2 byte character type 
        /// <para>Default = <see cref="CharacterType.Japanese"/></para>
        /// </summary>
        /// <param name="device">GU256x128c device</param>
        public static void Select2ByteCharacterType(this Gu256x128c device, CharacterType type)
        {
            var m = (byte)type;
            device.WriteBytes(new byte[] { 0x1F, 0x28, 0x67, 0x03, m });
        }

        /// <summary>
        /// Magnify the character by x times on the right, y times downward.
        /// <para>Default x = <see cref="0x01"/></para>
        /// <para>Default y = <see cref="0x01"/></para>
        /// </summary>
        /// <param name="x">Specify the size of magnification X</param>
        /// <param name="y">Specify the size of magnification Y</param>
        /// <param name="device">GU256x128c device</param>
        public static void FontMagnifiedDisplay(this Gu256x128c device, byte x, byte y)
        {
            device.WriteBytes(new byte[] { 0x1F, 0x28, 0x67, 0x40, x, y });
        }

        /// <summary>
        /// Specify or cancel boldface character.
        /// <para>Default = <see cref="false"/></para>
        /// </summary>
        /// <param name="device">GU256x128c device</param>
        public static void CharacterBoldDisplay(this Gu256x128c device, bool specify)
        {
            var b = (byte)(specify ? 1 : 0);
            device.WriteBytes(new byte[] { 0x1F, 0x28, 0x67, 0x41, b });
        }
    }
}