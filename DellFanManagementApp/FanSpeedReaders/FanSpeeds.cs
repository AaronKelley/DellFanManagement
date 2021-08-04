namespace DellFanManagement.App.FanSpeedReaders
{
    /// <summary>
    /// An object for holding the speeds of both system fans.
    /// </summary>
    public class FanSpeeds
    {
        /// <summary>
        /// Speed of fan 1.
        /// </summary>
        public uint? Fan1Rpm;

        /// <summary>
        /// Speed of fan 2.
        /// </summary>
        public uint? Fan2Rpm;
    }
}
