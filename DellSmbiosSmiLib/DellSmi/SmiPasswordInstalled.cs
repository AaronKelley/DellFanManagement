namespace DellFanManagement.DellSmbiosSmiLib.DellSmi
{
    /// <summary>
    /// For checking whether a BIOS password has been set.
    /// </summary>
    public enum SmiPasswordInstalled : byte
    {
        /// <summary>
        /// There is a password set.
        /// </summary>
        Installed = 0,

        /// <summary>
        /// There is not a password set.
        /// </summary>
        NotInstalled = 1
    }
}
