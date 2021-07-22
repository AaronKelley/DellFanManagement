namespace DellFanManagement.SmmIo
{
    /// <summary>
    /// Each "token" represents an aspect of the system behavior that can be configured.
    /// </summary>
    /// <seealso cref="https://github.com/dell/libsmbios/blob/master/doc/token_list.csv"/>
    public enum Token : uint
    {
        /// <summary>
        /// LCD brightness value.
        /// </summary>
        LcdBrightness = 0x007D,

        /// <summary>
        /// Disk controller set to "AHCI" mode.
        /// </summary>
        DiskControllerModeAhci = 0x0138,

        /// <summary>
        /// Disk controller set to "RAID" mode.
        /// </summary>
        DiskControllerModeRaid = 0x0139,

        /// <summary>
        /// Keyboard illumunation always off.
        /// </summary>
        KeyboardIlluminationOff = 0x01E1,

        /// <summary>
        /// Keyboard illumination always on.
        /// </summary>
        KeyboardIlluminationOn = 0x01E2,

        /// <summary>
        /// Keyboard illumination depending on ambient light level.
        /// </summary>
        KeyboardIlluminationAuto = 0x01E3,

        /// <summary>
        /// Keyboard illumination level set to 25%.
        /// </summary>
        KeyboardIlluminationAuto25 = 0x02EA,

        /// <summary>
        /// Keyboard illumination level set to 50%.
        /// </summary>
        KeyboardIlluminationAuto50 = 0x02EB,

        /// <summary>
        /// Keyboard illumination level set to 75%.
        /// </summary>
        KeyboardIlluminationAuto75 = 0x02EC,

        /// <summary>
        /// Fans run at max speed.
        /// </summary>
        FanControlOverrideEnable = 0x02FD,

        /// <summary>
        /// Fans run at speed based on environment.
        /// </summary>
        FanControlOverrideDisable = 0x02FE,

        /// <summary>
        /// Keyboard illumination level set to 100%.
        /// </summary>
        KeyboardIlluminationAuto100 = 0x02F6,

        FanSpeedAuto = 0x0332,
        FanSpeedHigh = 0x0333,
        FanSpeedMedium = 0x0334,
        FanSpeedLow = 0x0335,
        FanSpeedMediumHigh = 0x0405,
        FanSpeedMediumLow = 0x0406
    }
}
