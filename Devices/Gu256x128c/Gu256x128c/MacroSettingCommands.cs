using HumJ.Iot.Gu256x128c.Enums;

namespace HumJ.Iot.Gu256x128c
{
    public static class MacroSettingCommands
    {
        /// <summary>
        /// Define or delete of RAM Macro processing definition.
        /// </summary>
        /// <param name="device">GU256x128c device</param>
        /// <param name="p">RAM Macro data length</param>
        /// <param name="d">RAM Macro data</param>
        public static void RamMacroProcessingDefinition(this Gu256x128c device, ushort p, params byte[] d)
        {
            var pL = (byte)p;
            var pH = (byte)(p >> 8);
            device.WriteBytes(new byte[] { 0x1F, 0x3A, pL, pH }, d);
        }

        /// <summary>
        /// Define or delete FROM Macro to the FROM
        /// <para>* Valid at the user setup mode.</para>
        /// </summary>
        /// <param name="device">GU256x128c device</param>
        /// <param name="a">FROM Macro definition number</param>
        /// <param name="p">FROM Macro data length</param>
        /// <param name="t1">Display time interval</param>
        /// <param name="t2">Idle time of macro repetition</param>
        /// <param name="d">FROM Macro data</param>
        public static void FromMacroProcessingDefinition(this Gu256x128c device, byte a, ushort p, byte t1, byte t2, params byte[] d)
        {
            var pL = (byte)p;
            var pH = (byte)(p >> 8);
            device.WriteBytes(new byte[] { 0x1F, 0x28, 0x65, 0x12, a, pL, pH, t1, t2 }, d);
        }

        /// <summary>
        /// Execute Macro continuously.
        /// </summary>
        /// <param name="device">GU256x128c device</param>
        /// <param name="definition">Macro processing definition number</param>
        /// <param name="t1">Display time interval</param>
        /// <param name="t2">Idle time of macro repetition</param>
        public static void MacroExecution(this Gu256x128c device, MacroProcessingDefinition definition, byte t1, byte t2)
        {
            var a = (byte)definition;
            device.WriteBytes(new byte[] { 0x1F, 0x5E, a, t1, t2 });
        }
    }
}