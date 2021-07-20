namespace DellFanManagement.DellSmbiozBzhLib.PInvoke
{
    /// <summary>
    /// Windows per-thread error codes.
    /// </summary>
    /// <seealso cref="https://docs.microsoft.com/en-us/windows/win32/debug/system-error-codes"/>
    public enum ErrorCode : int
    {
        /// <summary>
        /// The data area passed to a system call is too small.
        /// </summary>
        InsufficientBuffer = 0x7A,

        /// <summary>
        /// An instance of the service is already running.
        /// </summary>
        ServiceAlreadyRunning = 0x420
    }
}
