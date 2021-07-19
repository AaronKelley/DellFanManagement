namespace DellFanManagement.Interop.PInvoke
{
    /// <summary>
    /// The severity of the error, and action taken, if this service fails to start.
    /// </summary>
    /// <seealso cref="https://docs.microsoft.com/en-us/windows/win32/api/winsvc/nf-winsvc-createservicew"/>
    public enum ServiceErrorControl : uint
    {
        /// <summary>
        /// The startup program logs the error in the event log, if possible. If the last-known-good configuration is
        /// being started, the startup operation fails. Otherwise, the system is restarted with the last-known good
        /// configuration.
        /// </summary>
        Critical = 0x00000003,

        /// <summary>
        /// The startup program ignores the error and continues the startup operation.
        /// </summary>
        Ignore = 0x00000000,

        /// <summary>
        /// The startup program logs the error in the event log but continues the startup operation.
        /// </summary>
        Normal = 0x00000001,

        /// <summary>
        /// The startup program logs the error in the event log. If the last-known-good configuration is being started,
        /// the startup operation continues. Otherwise, the system is restarted with the last-known-good configuration.
        /// </summary>
        Severe = 0x00000002
    }
}
