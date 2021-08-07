using DellFanManagement.DellSmbiozBzhLib;

namespace DellFanManagement.App.FanControllers
{
    /// <summary>
    /// Allows fan speed control using the BZH SMM I/O driver.
    /// </summary>
    class BzhFanController : FanController
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public BzhFanController()
        {
            if (DellSmbiosBzh.Initialize())
            {
                if (DellSmbiosBzh.GetFanRpm(BzhFanIndex.Fan1) == null)
                {
                    // This system can't be managed with the BZH driver.
                    Log.Write("Successfully loaded the BZH driver, but it appears that it cannot control the fans on this system.");
                    DellSmbiosBzh.Shutdown();
                }
            }
            else
            {
                Log.Write("Unable to load BZH driver; check for bzh_dell_smm_io_x64.sys in the working directory, and Windows must be configured to allow drivers without Microsoft cross-signatures to be loaded.");
            }

            IsAutomaticFanControlDisableSupported = DellSmbiosBzh.IsInitialized;
            IsSpecificFanControlSupported = DellSmbiosBzh.IsInitialized;
            IsIndividualFanControlSupported = DellSmbiosBzh.IsInitialized;
        }

        /// <summary>
        /// Disable automatic fan control.
        /// </summary>
        /// <returns>True on success, false on failure.</returns>
        public override bool DisableAutomaticFanControl()
        {
            if (DellSmbiosBzh.IsInitialized)
            {
                return DellSmbiosBzh.DisableAutomaticFanControl();
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Enable automatic fan control.
        /// </summary>
        /// <returns>True on success, false on failure.</returns>
        public override bool EnableAutomaticFanControl()
        {
            if (DellSmbiosBzh.IsInitialized)
            {
                return DellSmbiosBzh.EnableAutomaticFanControl();
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Set the fan speed.
        /// </summary>
        /// <param name="level">Speed level to set.</param>
        /// <param name="fanIndex">Which fan to set.</param>
        /// <returns>True on succes, false on failure.</returns>
        public override bool SetFanLevel(FanLevel level, FanIndex fanIndex)
        {
            if (DellSmbiosBzh.IsInitialized)
            {
                bool result1 = true;
                bool result2 = true;

                BzhFanLevel bzhLevel;
                switch (level)
                {
                    case FanLevel.Off:
                        bzhLevel = BzhFanLevel.Level0;
                        break;
                    case FanLevel.Medium:
                        bzhLevel = BzhFanLevel.Level1;
                        break;
                    case FanLevel.High:
                        bzhLevel = BzhFanLevel.Level2;
                        break;
                    default:
                        return false;
                }

                if (fanIndex == FanIndex.Fan1 || fanIndex == FanIndex.AllFans)
                {
                    result1 = DellSmbiosBzh.SetFanLevel(BzhFanIndex.Fan1, bzhLevel);
                }

                if (fanIndex == FanIndex.Fan2 || fanIndex == FanIndex.AllFans)
                {
                    result2 = DellSmbiosBzh.SetFanLevel(BzhFanIndex.Fan2, bzhLevel);
                }

                return result1 && result2;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Unload the BZH driver.
        /// </summary>
        public override void Shutdown()
        {
            DellSmbiosBzh.Shutdown();
        }
    }
}
