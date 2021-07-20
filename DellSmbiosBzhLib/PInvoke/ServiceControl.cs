using System;

namespace DellFanManagement.DellSmbiozBzhLib.PInvoke
{
    /// <summary>
    /// Service control codes.
    /// </summary>
    [Flags]
    public enum ServiceControl : uint
    {
        /// <summary>
        /// Notifies a service that it should stop.
        /// </summary>
        Stop = 0x1,

        /// <summary>
        /// Notifies a service that it should pause.
        /// </summary>
        Pause = 0x2,

        /// <summary>
        /// Notifies a paused service that it should resume.
        /// </summary>
        Continue = 0x3,

        /// <summary>
        /// Notifies a service that it should report its current status information to the service control manager.
        /// </summary>
        Interrogate = 0x4,

        /// <summary>
        /// Notifies a service that its startup parameters have changed.
        /// </summary>
        ParametersChanged = 0x6,

        /// <summary>
        /// Notifies a network service that there is a new component for binding.
        /// </summary>
        NetBindAdd = 0x7,

        /// <summary>
        /// Notifies a network service that a component for binding has been removed.
        /// </summary>
        NetBindRemove = 0x8,

        /// <summary>
        /// Notifies a network service that a disabled binding has been enabled.
        /// </summary>
        NetBindEnable = 0x9,

        /// <summary>
        /// Notifies a network service that one of its bindings has been disabled.
        /// </summary>
        NetBindDisable = 0xA
    }
}
