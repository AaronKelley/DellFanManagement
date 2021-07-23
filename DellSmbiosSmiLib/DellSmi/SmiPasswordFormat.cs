namespace DellFanManagement.DellSmbiosSmiLib.DellSmi
{
    /// <summary>
    /// Identifiers for the Dell BIOS password format, which is needed to derivate the security key.
    /// </summary>
    /// <seealso cref="https://github.com/dell/libsmbios/blob/master/src/libsmbios_c/smi/smi_password.c"/>
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
