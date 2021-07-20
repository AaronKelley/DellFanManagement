using System.Runtime.InteropServices;

namespace DellFanManagement.DellSmbiozBzhLib.PInvoke
{
    /// <summary>
    /// Contains status information for a service.
    /// </summary>
    /// <seealso cref="http://www.pinvoke.net/default.aspx/Structures/SERVICE_STATUS.html"/>
    /// <seealso cref="https://docs.microsoft.com/en-us/windows/win32/api/winsvc/ns-winsvc-service_status"/>
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    public struct ServiceStatus
    {
        /// <summary>
        /// The type of service.
        /// </summary>
        public ServiceType ServiceType;

        /// <summary>
        /// The current state of the service.
        /// </summary>
        public uint CurrentState;

        /// <summary>
        /// The control codes the service accepts and processes in its handler function.
        /// </summary>
        public uint ControlsAccepted;

        /// <summary>
        /// The error code the service uses to report an error that occurs when it is starting or stopping.
        /// </summary>
        public uint Win32ExitCode;

        /// <summary>
        /// A service-specific error code that the service returns when an error occurs while the service is starting or
        /// stopping.
        /// </summary>
        public uint ServiceSpecificExitCode;

        /// <summary>
        /// The check-point value the service increments periodically to report its progress during a lengthy start,
        /// stop, pause, or continue operation.
        /// </summary>
        public uint CheckPoint;

        /// <summary>
        /// The estimated time required for a pending start, stop, pause, or continue operation, in milliseconds.
        /// </summary>
        public uint WaitHint;
    }
}
