namespace DellFanManagement.Interop
{
    /// <summary>
    /// Used to specify which fan level to set.
    /// </summary>
    public enum FanLevel : ulong
    {
        /// <summary>
        /// Fan off.
        /// </summary>
        Level0 = 0,

        /// <summary>
        /// Medium fan speed.
        /// </summary>
        Level1 = 1,

        /// <summary>
        /// High fan speed.
        /// </summary>
        Level2 = 2
    }
}
