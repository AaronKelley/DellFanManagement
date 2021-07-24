namespace DellFanManagement.DellSmbiosSmiLib.DellSmi
{
    /// <summary>
    /// Identifier for which password operations are being performed on.
    /// </summary>
    /// <seealso cref="https://github.com/dell/libsmbios/blob/master/src/libsmbios_c/smi/smi_password.c"/>
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
