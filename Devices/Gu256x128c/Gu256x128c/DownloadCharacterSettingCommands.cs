using HumJ.Iot.Gu256x128c.Enums;

namespace HumJ.Iot.Gu256x128c
{
    public static class DownloadCharacterSettingCommands
    {
        /// <summary>
        /// Specify enable or disable for download character
        /// <para>Default = <see cref="false"/></para>
        /// </summary>
        /// <param name="device">GU256x128c device</param>
        public static void SpecifyDownloadCharacter(this Gu256x128c device, bool enable)
        {
            var n = (byte)(enable ? 1 : 0);
            device.WriteBytes(new byte[] { 0x1B, 0x25, n });
        }

        /// <summary>
        /// Define 6 x 8 or 8 x 16 dot download characters into RAM. 
        /// </summary>
        /// <param name="device">GU256x128c device</param>
        /// <param name="a">Select character type</param>
        /// <param name="c1">Start character code</param>
        /// <param name="c2">End character code</param>
        /// <param name="d">Defined data [ (x1, d1 ... dx1), (xk, dk ... dxk) ]</param>
        public static void DownloadCharacterDefinition(this Gu256x128c device, byte a, byte c1, byte c2, params KeyValuePair<byte, byte[]>[] d)
        {
            var buffer = d.SelectMany(kv => new byte[] { kv.Key }.Concat(kv.Value).ToArray()).ToArray();
            device.WriteBytes(new byte[] { 0x1B, 0x26, a, c1, c2 }, buffer);
        }

        /// <summary>
        /// Delete defined 6x8 or 8x16 dot download character.
        /// </summary>
        /// <param name="device">GU256x128c device</param>
        /// <param name="a">Select character type</param>
        /// <param name="c">Character code for delete</param>
        public static void DeleteDownloadedCharacter(this Gu256x128c device, byte a, byte c)
        {
            device.WriteBytes(new byte[] { 0x1B, 0x3F, a, c });
        }

        /// <summary>
        /// Defines the16 x 16 downloaded character in specified code.
        /// </summary>
        /// <param name="device">GU256x128c device</param>
        /// <param name="c">Character code</param>
        /// <param name="d">Definition data</param>
        public static void DownloadCharacterDefinition16x16(this Gu256x128c device, char c, params byte[] d)
        {
            var c1 = (byte)(c >> 8);
            var c2 = (byte)c;
            device.WriteBytes(new byte[] { 0x1F, 0x28, 0x67, 0x10, c1, c2 }, d);
        }

        /// <summary>
        /// Delete the16 x 16 downloaded character defined in the specified code.
        /// </summary>
        /// <param name="device">GU256x128c device</param>
        /// <param name="c">Delete character code</param>
        public static void DownloadedCharacterDelete16x16(this Gu256x128c device, char c)
        {
            var c1 = (byte)(c >> 8);
            var c2 = (byte)c;
            device.WriteBytes(new byte[] { 0x1F, 0x28, 0x67, 0x11, c1, c2 });
        }

        /// <summary>
        /// Save the download character already defined on RAM to the FROM.
        /// <para>* Valid at the user setup mode.</para>
        /// </summary>
        /// <param name="device">GU256x128c device</param>
        /// <param name="a">Select font type</param>
        public static void SaveDownloadedCharacter(this Gu256x128c device, FontType type)
        {
            var a = (byte)type;
            device.WriteBytes(new byte[] { 0x1F, 0x28, 0x65, 0x11, a });
        }

        /// <summary>
        /// Transfer the download character defined in FROM to RAM.
        /// </summary>
        /// <param name="device">GU256x128c device</param>
        /// <param name="type">Select font type</param>
        public static void DownloadCharacterTransfer(this Gu256x128c device, FontType type)
        {
            var a = (byte)type;
            device.WriteBytes(new byte[] { 0x1F, 0x28, 0x65, 0x21, a });
        }

        /// <summary>
        /// Define the user font of 1byte code to the user table.
        /// </summary>
        /// <param name="device"></param>
        /// <param name="m">User table</param>
        /// <param name="p">Definition data [ P(80h-1), P(80h-2), ... , P(FFh-n) ]</param>
        public static void FromUserFontDefinition(this Gu256x128c device, FontSize table, params byte[] p)
        {
            var m = (byte)table;
            device.WriteBytes(new byte[] { 0x1F, 0x28, 0x65, 0x13, m }, p);
        }
    }
}