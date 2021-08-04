namespace DellFanManagement.DellSmbiosSmiLib.DellSmi
{
    /// <summary>
    /// For checking the status of a BIOS password check.
    /// </summary>
    public enum SmiPasswordCheckResult : uint
    {
        /// <summary>
        /// The provided password was correct.
        /// </summary>
        Correct = 0,

        /// <summary>
        /// The provided password was not correct.
        /// </summary>
        Incorrect = 2
    }
}
