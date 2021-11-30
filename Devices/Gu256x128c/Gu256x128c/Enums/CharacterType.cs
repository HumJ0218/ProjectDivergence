namespace HumJ.Iot.Gu256x128c.Enums
{
    public enum CharacterType : byte
    {
        /// <summary>
        /// JIS X0208  (Shift-JIS)
        /// </summary>
        Japanese = 0x00,

        /// <summary>
        /// KSC5601-87
        /// </summary>
        Korean = 0x01,

        /// <summary>
        /// GB2312-1980
        /// </summary>
        SimplifiedChinese = 0x02,

        /// <summary>
        /// Big-5
        /// </summary>
        TraditionalChinese = 0x03,
    }
}