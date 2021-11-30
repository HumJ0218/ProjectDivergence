namespace HumJ.Iot.Gu256x128c
{
    public static class CodeSet
    {
        /// <summary>
        /// Display character to the cursor position.
        /// </summary>
        /// <param name="device">GU256x128c device</param>
        public static void CharacterDisplay(this Gu256x128c device, string s) => device.WriteString(s);

        /// <summary>
        /// The cursor moves to left by one character.
        /// </summary>
        /// <param name="device">GU256x128c device</param>
        public static void BackSpace(this Gu256x128c device) => device.WriteString("\x08");

        /// <summary>
        /// The cursor moves to right by one character.
        /// </summary>
        /// <param name="device">GU256x128c device</param>
        public static void HorizontalTab(this Gu256x128c device) => device.WriteString("\x09");

        /// <summary>
        /// The cursor moves to one lower line.
        /// </summary>
        /// <param name="device">GU256x128c device</param>
        public static void LineFeed(this Gu256x128c device) => device.WriteString("\x0A");

        /// <summary>
        /// The cursor moves to the home position (Most top left)
        /// </summary>
        /// <param name="device">GU256x128c device</param>
        public static void HomePosition(this Gu256x128c device) => device.WriteString("\x0B");

        /// <summary>
        /// The display screen is cleared and the cursor moves to home position
        /// </summary>
        /// <param name="device">GU256x128c device</param>
        public static void DisplayClear(this Gu256x128c device) => device.WriteString("\x0C");

        /// <summary>
        /// The cursor moves to left end of same line.
        /// </summary>
        /// <param name="device">GU256x128c device</param>
        public static void CarriageReturn(this Gu256x128c device) => device.WriteString("\x0D");
    }
}