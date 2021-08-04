namespace DellFanManagement.DellSmbiozBzhLib.PInvoke
{
    /// <summary>
    /// Specific access rights for the Service Control Manager.
    /// </summary>
    /// <seealso cref="http://www.pinvoke.net/default.aspx/Enums/SERVICE_ACCESS.html"/>
    /// <seealso cref="https://docs.microsoft.com/en-us/windows/win32/services/service-security-and-access-rights"/>
    [System.Flags]
    public enum ServiceAccess : uint
    {
       /// <summary>
        /// Request all rights to a standard service.
        /// </summary>
        AllAccess = 0xF01FF,

        /// <summary>
        /// Request all rights to the service manager.
        /// </summary>
        ServiceManagerAllAccess = 0xF003F
    }
}
