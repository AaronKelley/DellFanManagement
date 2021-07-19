namespace DellFanManagement.Interop.PInvoke
{
    /// <summary>
    /// The service start options.
    /// </summary>
    /// <seealso cref="https://docs.microsoft.com/en-us/windows/win32/api/winsvc/nf-winsvc-createservicew"/>
    public enum ServiceStartType : uint
    {
        /// <summary>
        /// A service started automatically by the service control manager during system startup.
        /// </summary>
        AutoStart = 0x00000002,

        /// <summary>
        /// A device driver started by the system loader.
        /// </summary>
        BootStart = 0x00000000,

        /// <summary>
        /// A service started by the service control manager when a process calls the StartService function.
        /// </summary>
        DemandStart = 0x00000003,

        /// <summary>
        /// A service that cannot be started.
        /// </summary>
        Disabled = 0x00000004,

        /// <summary>
        /// A device driver started by the IoInitSystem function.
        /// </summary>
        SystemStart = 0x00000001
    }
}
