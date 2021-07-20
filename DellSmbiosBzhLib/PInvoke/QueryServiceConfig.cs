using System.Runtime.InteropServices;

namespace DellFanManagement.DellSmbiozBzhLib.PInvoke
{
    /// <summary>
    /// Contains configuration information for an installed service. It is used by the QueryServiceConfig function.
    /// </summary>
    /// <seealso cref="https://docs.microsoft.com/en-us/windows/win32/api/winsvc/ns-winsvc-query_service_configw"/>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct QueryServiceConfig
    {
        /// <summary>
        /// The type of service.
        /// </summary>
        public ServiceType serviceType;

        /// <summary>
        /// When to start the service.
        /// </summary>
        public ServiceStartType startType;

        /// <summary>
        /// The severity of the error, and action taken, if this service fails to start.
        /// </summary>
        public ServiceErrorControl errorControl;

        /// <summary>
        /// The fully qualified path to the service binary file.
        /// </summary>
        public string binaryPathName;

        /// <summary>
        /// The name of the load ordering group to which this service belongs.
        /// </summary>
        public string loadOrderGroup;

        /// <summary>
        /// A unique tag value for this service in the group specified by the lpLoadOrderGroup parameter.
        /// </summary>
        public uint tagId;

        /// <summary>
        /// A pointer to an array of null-separated names of services or load ordering groups that must start before
        /// this service.
        /// </summary>
        public string dependencies;

        /// <summary>
        /// If the service type is SERVICE_WIN32_OWN_PROCESS or SERVICE_WIN32_SHARE_PROCESS, this member is the name of
        /// the account that the service process will be logged on as when it runs.
        /// </summary>
        public string serviceStartName;

        /// <summary>
        /// The display name to be used by service control programs to identify the service.
        /// </summary>
        public string displayName;
    }
}
