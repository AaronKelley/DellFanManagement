namespace DellFanManagement.SmmIo
{
    /// <summary>
    /// Representation of a "thermal setting".
    /// </summary>
    public enum ThermalSetting : uint
    {
        /// <summary>
        /// Represents an error retrieving the system's thermal setting.
        /// </summary>
        Error = 0,

        /// <summary>
        /// Optimized; default value.
        /// </summary>
        Optimized = 1,

        /// <summary>
        /// Optimize for low surface temperature.
        /// </summary>
        Cool = 2,

        /// <summary>
        /// Optimize for low noise.
        /// </summary>
        Quiet = 4,

        /// <summary>
        /// Optimize for maximum performance.
        /// </summary>
        Performance = 8
    }
}
