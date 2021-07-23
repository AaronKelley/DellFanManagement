namespace DellFanManagement.SmmIo.DellSmi
{
    /// <summary>
    /// Identifiers for the Dell BIOS password format, which is needed to derivate the security key.
    /// </summary>
    public enum SmiPasswordFormat : uint
    {
        /// <summary>
        /// Password needs to be provided in scancode format.
        /// </summary>
        Scancode = 0,

        /// <summary>
        /// Password needs to be provided in ASCII format.
        /// </summary>
        Ascii = 1
    }
}
