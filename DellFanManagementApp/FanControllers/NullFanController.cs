namespace DellFanManagement.App.FanControllers
{
    /// <summary>
    /// This fan controller can't actually control the fans.
    /// </summary>
    class NullFanController : FanController
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public NullFanController()
        {
            IsAutomaticFanControlDisableSupported = false;
            IsSpecificFanControlSupported = false;
            IsIndividualFanControlSupported = false;
        }

        /// <summary>
        /// Disable automatic fan control.
        /// </summary>
        /// <returns>True on success, false on failure.</returns>
        public override bool DisableAutomaticFanControl()
        {
            return false;
        }

        /// <summary>
        /// Enable automatic fan control.
        /// </summary>
        /// <returns>True on success, false on failure.</returns>
        public override bool EnableAutomaticFanControl()
        {
            return false;
        }

        /// <summary>
        /// Set the fan speed.
        /// </summary>
        /// <param name="level">Speed level to set.</param>
        /// <param name="fanIndex">Which fan to set.</param>
        /// <returns>True on succes, false on failure.</returns>
        public override bool SetFanLevel(FanLevel level, FanIndex fanIndex)
        {
            return false;
        }
    }
}
