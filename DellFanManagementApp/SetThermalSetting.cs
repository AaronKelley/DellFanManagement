using DellFanManagement.DellSmbiosSmiLib;
using System;

namespace DellFanManagement.App
{
    /// <summary>
    /// Used to set the thermal setting of the system while running the program from the command line.
    /// </summary>
    static class SetThermalSetting
    {
        /// <summary>
        /// Set the system thermal setting according to the setting provided on the command line.
        /// </summary>
        /// <param name="args">Command line arguments</param>
        /// <returns>Zero on success, non-zero on failure</returns>
        public static int ExecuteSetThermalSetting(string[] args)
        {
            if (args.Length != 2)
            {
                DellFanCmd.Usage();
                return -1;
            }

            ThermalSetting newSetting, currentSetting;

            // Determine the requested setting from the command line parameter.
            switch (args[1].ToLower())
            {
                case "optimized":
                    newSetting = ThermalSetting.Optimized;
                    break;
                case "cool":
                    newSetting = ThermalSetting.Cool;
                    break;
                case "quiet":
                    newSetting = ThermalSetting.Quiet;
                    break;
                case "performance":
                    newSetting = ThermalSetting.Performance;
                    break;
                default:
                    DellFanCmd.Usage();
                    return -1;
            }

            // Check the current setting.
            currentSetting = DellSmbiosSmi.GetThermalSetting();
            Console.WriteLine("Thermal setting, before change: {0}", currentSetting);

            // Apply the new setting.
            if (!DellSmbiosSmi.SetThermalSetting(newSetting))
            {
                Console.Error.WriteLine("Failed to apply the new thermal setting.");
                return -1;
            }

            // Check the new setting.
            currentSetting = DellSmbiosSmi.GetThermalSetting();
            Console.WriteLine("Thermal setting, after change:  {0}", currentSetting);

            if (currentSetting == ThermalSetting.Error)
            {
                return -1;
            }

            return 0;
        }
    }
}
