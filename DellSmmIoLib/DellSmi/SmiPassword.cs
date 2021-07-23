namespace DellFanManagement.SmmIo.DellSmi
{
    /// <summary>
    /// Identifier for which password operations are being performed on.
    /// </summary>
    public enum SmiPassword : ushort
    {
        /// <summary>
        /// User password.
        /// </summary>
        User = 9,

        /// <summary>
        /// Admin password.
        /// </summary>
        Admin = 10,

        /// <summary>
        /// Owner password.
        /// </summary>
        Owner = 12
    }
}
