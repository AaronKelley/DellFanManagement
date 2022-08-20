namespace DellFanManagement.App
{
    /// <summary>
    /// Identifies different colors for the system tray icon.
    /// </summary>
    public enum TrayIconColor
    {
        /// <summary>
        /// Gray (consistency mode is not enabled).
        /// </summary>
        Gray,

        /// <summary>
        /// Blue (consistency mode in ideal state).
        /// </summary>
        Blue,

        /// <summary>
        /// Red (consistency mode in non-ideal state).
        /// </summary>
        Red
    }
}
