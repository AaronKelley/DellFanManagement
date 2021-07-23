namespace DellFanManagement.SmmIo.DellSmi
{
    public enum SelectToken : ushort
    {
        /// <summary>
        /// "Standard" tokens apply regardless of battery setting (?).
        /// </summary>
        Standard = 0,

        /// <summary>
        /// "Battery" token applies when the system is on battery power.
        /// </summary>
        Battery = 1,

        /// <summary>
        /// "AC" token applies when the system is on AC power.
        /// </summary>
        AC = 2,

        /// <summary>
        /// Used to get BIOS password information.
        /// </summary>
        PasswordProperties,

        /// <summary>
        /// Used to enable or disable wireless connectivity.
        /// </summary>
        /// /// <seealso cref="https://github.com/torvalds/linux/blob/master/include/uapi/linux/wmi.h"/>
        RfKill = 11,

        /// <summary>
        /// Used to get or set keyboard backlight status.
        /// </summary>
        /// <seealso cref="https://github.com/torvalds/linux/blob/master/drivers/platform/x86/dell/dell-smbios.h"/>
        KeyboardBacklight = 11,

        /// <summary>
        /// For getting or setting "Thermal Mode".
        /// </summary>
        /// <remarks>Source: Dell Power Manager</remarks>
        ThermalMode = 19
    }
}
