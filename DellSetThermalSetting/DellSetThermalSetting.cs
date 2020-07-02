using DellFanManagement.Interop;
using DellFanManagement.SmmIo;
using System;

namespace DellFanManagement.SetThermalSetting
{
    class DellSetThermalSetting
    {
        /// <summary>
        /// A simple program that adjusts the thermal setting on a Dell laptop.
        /// </summary>
        /// <param name="args">The program expects a single parameter, which is the name of the thermal setting to apply</param>
        /// <returns>0 on success, -1 on error</returns>
        static int Main(string[] args)
        {
            try
            {
                Console.WriteLine("DellSetThermalSetting {0}", DellFanLib.Version);
                Console.WriteLine("By Aaron Kelley");
                Console.WriteLine("Licensed under GPLv3");
                Console.WriteLine("Source code available at https://github.com/AaronKelley/DellFanManagement");
                Console.WriteLine();

                if (args.Length != 1)
                {
                    Usage();
                    return -1;
                }

                ThermalSetting newSetting, currentSetting;

                // Determine the requested setting from the command line parameter.
                switch (args[0].ToLower())
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
                    case "ultraperformance":
                        newSetting = ThermalSetting.UltraPerformance;
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
            }
            catch (Exception exception)
            {
                Console.Error.WriteLine("Exception: {0}", exception.Message);
                Console.Error.WriteLine(exception.StackTrace);
                Console.Error.WriteLine();
                Usage();
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
                    "Usage is simple:\n" +
                    "  DellSetThermalSetting.exe Optimized          Set system to optimized mode\n" +
                    "  DellSetThermalSetting.exe Cool               Set system to cool mode\n" +
                    "  DellSetThermalSetting.exe Quiet              Set system to quiet mode\n" +
                    "  DellSetThermalSetting.exe UltraPerformance   Set system to ultra performance mode\n" +
                    "\n" +
                    "The program must be run elevated / \"as administrator\".\n"
                );
        }
    }
}
