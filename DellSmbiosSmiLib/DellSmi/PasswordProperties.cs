namespace DellFanManagement.DellSmbiosSmiLib.DellSmi
{
    /// <summary>
    /// Contains property about the BIOS passwords supported on the target system.
    /// </summary>
    public struct PasswordProperties
    {
        /// <summary>
        /// 0 = password is set; 1 = password is not set.
        /// </summary>
        public SmiPasswordInstalled Installed;

        /// <summary>
        /// Maximum password length, in characters.
        /// </summary>
        public byte MaximumLength;

        /// <summary>
        /// Minimum password length, in characters.
        /// </summary>
        public byte MinimumLength;

        /// <summary>
        /// Password characteristics (bit field).
        /// </summary>
        public byte Characteristics;

        /// <summary>
        /// Minimum number of alphabetic characters in the password.
        /// </summary>
        public byte MinimumAlphabeticCharacters;

        /// <summary>
        /// Minimum number of numeric characters in the password.
        /// </summary>
        public byte MinimumNumericCharacters;

        /// <summary>
        /// Minimum number of special characters in the password.
        /// </summary>
        public byte MinimumSpecialCharacters;

        /// <summary>
        /// Maximum number of repeating characters in the password.
        /// </summary>
        public byte MaximumRepeatingCharacters;
    }
}
