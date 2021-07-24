namespace DellFanManagement.DellSmbiosSmiLib
{
    /// <summary>
    /// Representation of the levels that the fan may be set to through the SMI interface.
    /// </summary>
    public enum SmiFanLevel
    {
        /// <summary>
        /// Fans are off.
        /// </summary>
        Off,

        /// <summary>
        /// Fans are set to a low level.
        /// </summary>
        Low,

        /// <summary>
        /// Fans are set to a medium level.
        /// </summary>
        Medium,

        /// <summary>
        /// Fans are set to a high level.
        /// </summary>
        High
    }
}
