using DellFanManagement.DellSmbiosSmiLib;

namespace DellFanManagement.App.FanControllers
{
    /// <summary>
    /// Allows fan speed control using the WMI/SMI interface.
    /// </summary>
    class SmiFanController : FanController
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public SmiFanController()
        {
            IsAutomaticFanControlDisableSupported = true;
            IsSpecificFanControlSupported = true;
            IsIndividualFanControlSupported = false;
        }

        /// <summary>
        /// Disable automatic fan control.
        /// </summary>
        /// <returns>True on success, false on failure.</returns>
        public override bool DisableAutomaticFanControl()
        {
            return DellSmbiosSmi.DisableAutomaticFanControl();
        }

        /// <summary>
        /// Enable automatic fan control.
        /// </summary>
        /// <returns>True on success, false on failure.</returns>
        public override bool EnableAutomaticFanControl()
        {
            return DellSmbiosSmi.EnableAutomaticFanControl();
        }

        /// <summary>
        /// Set the fan speed.
        /// </summary>
        /// <param name="level">Speed level to set.</param>
        /// <param name="fanIndex">Which fan to set.</param>
        /// <returns>True on succes, false on failure.</returns>
        public override bool SetFanLevel(FanLevel level, FanIndex fanIndex)
        {
            if (fanIndex != FanIndex.AllFans)
            {
                // Can't control fans individually via SMI.
                return false;
            }

            SmiFanLevel smiLevel;
            switch (level)
            {
                case FanLevel.Off:
                    smiLevel = SmiFanLevel.Off;
                    break;
                case FanLevel.Medium:
                    smiLevel = SmiFanLevel.Low;
                    break;
                case FanLevel.High:
                    smiLevel = SmiFanLevel.High;
                    break;
                default:
                    return false;
            }

            return DellSmbiosSmi.SetFanLevel(smiLevel);
        }

        /// <summary>
        /// No shutdown method is needed for this fan controller.
        /// </summary>
        public override void Shutdown()
        {
            // Take no action.
        }
    }
}
