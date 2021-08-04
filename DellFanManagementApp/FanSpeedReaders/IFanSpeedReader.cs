namespace DellFanManagement.App.FanSpeedReaders
{
    /// <summary>
    /// Base class for fan speeed reader implementations, which use different methods to read the fan speed.
    /// </summary>
    public interface IFanSpeedReader
    {
        /// <summary>
        /// Get the current system fan speeds.
        /// </summary>
        /// <returns>Object containing speeds for two fans.  (null reading means fan not present, or error
        /// reading.)</returns>
        public abstract FanSpeeds GetFanSpeeds();
    }
}
