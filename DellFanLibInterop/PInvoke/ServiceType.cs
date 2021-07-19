namespace DellFanManagement.Interop.PInvoke
{
    /// <summary>
    /// Service types.
    /// </summary>
    /// <seealso cref="https://docs.microsoft.com/en-us/windows/win32/api/winsvc/nf-winsvc-createservicew"/>
    public enum ServiceType : uint
    {
        /// <summary>
        /// Reserved.
        /// </summary>
        Adapter = 0x00000004,

        /// <summary>
        /// File system driver service.
        /// </summary>
        FileSystemDriver = 0x00000002,

        /// <summary>
        /// Driver service.
        /// </summary>
        KernelDriver = 0x00000001,

        /// <summary>
        /// Reserved.
        /// </summary>
        RecognizerDriver = 0x00000008,

        /// <summary>
        /// Service that runs in its own process.
        /// </summary>
        OwnProcess = 0x00000010,

        /// <summary>
        /// Service that shares a process with one or more other services.
        /// </summary>
        ShareProcess = 0x00000020,

        /// <summary>
        /// The service runs in its own process under the logged-on user account.
        /// </summary>
        UserOwnProcess = 0x00000050,

        /// <summary>
        /// The service shares a process with one or more other services that run under the logged-on user account.
        /// </summary>
        UserShareProcess = 0x00000060,

        /// <summary>
        /// The service can interact with the desktop.
        /// </summary>
        InteractiveProcess = 0x00000100
    }
}
