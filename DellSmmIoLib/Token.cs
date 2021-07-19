namespace DellFanManagement.SmmIo
{
    /// <summary>
    /// Each "token" represents an aspect of the system behavior that can be configured.
    /// </summary>
    /// <seealso cref="https://github.com/dell/libsmbios/blob/master/doc/token_list.csv"/>
    public enum Token : uint
    {
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
        KeyboardIlluminationAuto = 0x01E3
    }
}
