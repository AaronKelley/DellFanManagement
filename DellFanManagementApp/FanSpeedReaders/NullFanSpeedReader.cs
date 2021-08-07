namespace DellFanManagement.App.FanSpeedReaders
{
    /// <summary>
    /// This fan speed reader can't read the fan speed and always returns null.
    /// </summary>
    class NullFanSpeedReader : IFanSpeedReader
    {
        /// <summary>
        /// Get the current system fan speeds.
        /// </summary>
        /// <returns>Object containing speeds for two fans.  (null reading means fan not present, or error
        /// reading.)</returns>
        public FanSpeeds GetFanSpeeds()
        {
            return new FanSpeeds()
            {
                Fan1Rpm = null,
                Fan2Rpm = null
            };
        }
    }
}
