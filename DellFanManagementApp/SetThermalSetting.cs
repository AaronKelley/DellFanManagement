using DellFanManagement.SmmIo;
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
                Usage();
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
                    Usage();
                    return -1;
            }

            // Check the current setting.
            currentSetting = DellSmmIoLib.GetThermalSetting();
            Console.WriteLine("Thermal setting, before change: {0}", currentSetting);

            // Apply the new setting.
            if (!DellSmmIoLib.SetThermalSetting(newSetting))
            {
                Console.Error.WriteLine("Failed to apply the new thermal setting.");
                Usage();
                return -1;
            }

            // Check the new setting.
            currentSetting = DellSmmIoLib.GetThermalSetting();
            Console.WriteLine("Thermal setting, after change:  {0}", currentSetting);

            if (currentSetting == ThermalSetting.Error)
            {
                return -1;
            }

            return 0;
        }

        /// <summary>
        /// Print a message to the console about how to use the program.
        /// </summary>
        private static void Usage()
        {
            Console.WriteLine(
                    "\n" +
                    "To set the system thermal setting:\n" +
                    "  DellFanManagement.exe SetThermalSetting Optimized    Set system to optimized mode\n" +
                    "  DellFanManagement.exe SetThermalSetting Cool         Set system to cool mode\n" +
                    "  DellFanManagement.exe SetThermalSetting Quiet        Set system to quiet mode\n" +
                    "  DellFanManagement.exe SetThermalSetting Performance  Set system to performance mode\n" +
                    "\n" +
                    "The program must be run elevated / \"as administrator\".\n"
                );
        }
    }
}
