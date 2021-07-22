namespace DellFanManagement.App.FanSpeedReaders
{
    /// <summary>
    /// Determine system capabilities and select an appropriate fan speed reader object to use.
    /// </summary>
    public static class FanSpeedReaderFactory
    {
        /// <summary>
        /// Selects a fan speed reader and returns it.
        /// </summary>
        /// <returns>Fan speed reader appropriate for the system.</returns>
        public static IFanSpeedReader GetFanSpeedReader()
        {
            // Right now, we just have one choice.
            return new BzhFanSpeedReader();
        }
    }
}
