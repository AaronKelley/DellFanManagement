namespace DellFanManagement.SmmIo.DellSmi
{
    public enum ClassToken : ushort
    {
        /// <summary>
        /// Read a token.
        /// </summary>
        /// <seealso cref="https://github.com/torvalds/linux/blob/master/include/uapi/linux/wmi.h"/>
        TokenRead = 0,

        /// <summary>
        /// Write a token.
        /// </summary>
        /// <seealso cref="https://github.com/torvalds/linux/blob/master/include/uapi/linux/wmi.h"/>
        TokenWrite = 1,

        /// <summary>
        /// For dealing with the keyboard backlight.
        /// </summary>
        /// <seealso cref="https://github.com/torvalds/linux/blob/master/drivers/platform/x86/dell/dell-smbios.h"/>
        KeyboardBacklight = 4,

        /// <summary>
        /// For getting or setting various information.
        /// </summary>
        /// <remarks>Source: Dell Power Manager</remarks>
        Info = 17
    }
}
