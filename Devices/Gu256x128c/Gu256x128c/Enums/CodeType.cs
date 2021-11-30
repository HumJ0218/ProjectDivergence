namespace HumJ.Iot.Gu256x128c.Enums
{
    public enum CodeType :byte{
        /// <summary>
        /// USA - Euro std
        /// </summary>
        PC437 = 0x00,

        /// <summary>
        /// Katakana - Japanese
        /// </summary>
        Katakana = 0x01,

        /// <summary>
        /// Multilingual
        /// </summary>
        PC850 = 0x02,

        /// <summary>
        /// Portuguese
        /// </summary>
        PC860 = 0x03,

        /// <summary>
        /// Canadian-French
        /// </summary>
        PC863 = 0x04,

        /// <summary>
        /// Nordic
        /// </summary>
        PC865 = 0x05,

        /// <summary>
        /// WPC1252
        /// </summary>
        WPC1252 = 0x10,

        /// <summary>
        /// Cyrillic #2
        /// </summary>
        PC866 = 0x11,

        /// <summary>
        /// Latin 2
        /// </summary>
        PC852 = 0x12,

        /// <summary>
        /// PC858
        /// </summary>
        PC858 = 0x13,

        /// <summary>
        /// User table
        /// </summary>
        UserTable = 0xFF,
    }
}