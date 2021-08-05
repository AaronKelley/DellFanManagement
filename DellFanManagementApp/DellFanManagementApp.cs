using DellFanManagement.DellSmbiosSmiLib;
using DellFanManagement.DellSmbiosSmiLib.DellSmi;
using DellFanManagement.DellSmbiozBzhLib;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Forms;

namespace DellFanManagement.App
{
    static class DellFanManagementApp
    {
        /// <summary>
        /// Version number for the entire package.
        /// </summary>
        public const string Version = "DEV";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static int Main(string[] args)
        {
            if (args.Length == 0)
            {
                // GUI mode.
                try
                {
                    if (UacHelper.IsProcessElevated())
                    {
                        // First thing's first...  Attempt to load the EC control driver.
                        // Shutdown happens immediately after the background thread main loop ends.
                        if (DellSmbiosBzh.Initialize())
                        {
                            // Looks like we're ready to start up the GUI app.
                            // Set process priority to high.
                            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;

                            // Boilerplate code to start the app.
                            Application.SetHighDpiMode(HighDpiMode.DpiUnaware);
                            Application.EnableVisualStyles();
                            Application.SetCompatibleTextRenderingDefault(false);
                            Application.Run(new DellFanManagementGuiForm());
                        }
                        else
                        {
                            MessageBox.Show("Failed to load driver.  Make sure that bzh_dell_smm_io_x64.sys is present in the working directory.  Reboot the system if the EV sign check pass registry setting was just set.",
                                "Dell Fan Management driver check", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return 1;
                        }
                    }
                    else
                    {
                        MessageBox.Show("This program must be run with administrative privileges.", "Dell Fan Management privilege check", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show(string.Format("{0}: {1}\n{2}", exception.GetType().ToString(), exception.Message, exception.StackTrace),
                        "Error starting application", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return 1;
                }

                return 0;
            }
            else
            {
                // CMD mode.
                try
                {
                    Console.WriteLine("Dell Fan Management, version {0}", Version);
                    Console.WriteLine("By Aaron Kelley");
                    Console.WriteLine("Licensed under GPLv3");
                    Console.WriteLine("Source code available at https://github.com/AaronKelley/DellFanManagement");
                    Console.WriteLine();

                    if (UacHelper.IsProcessElevated())
                    {
                        if (args[0].ToLower() == "packagetest")
                        {
                            return PackageTest.RunPackageTests() ? 0 : 1;
                        }
                        else if (args[0].ToLower() == "setthermalsetting")
                        {
                            return SetThermalSetting.ExecuteSetThermalSetting(args);
                        }
                        else if (args[0].ToLower() == "smi-token-dump")
                        {
                            return SmiTokenDump();
                        }
                        else if (args[0].ToLower() == "smi-get-token")
                        {
                            return SmiGetToken(args);
                        }
                        else if (args[0].ToLower() == "smi-set-token")
                        {
                            return SmiSetToken(args);
                        }
                        else
                        {
                            Console.WriteLine("Dell SMM I/O driver by 424778940z");
                            Console.WriteLine("https://github.com/424778940z/bzh-windrv-dell-smm-io");
                            Console.WriteLine();
                            Console.WriteLine("Derived from \"Dell fan utility\" by 424778940z");
                            Console.WriteLine("https://github.com/424778940z/dell-fan-utility");
                            Console.WriteLine();

                            return DellFanCmd.ProcessCommand(args);
                        }
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("This program must be run with administrative privileges.");
                        return 1;
                    }
                }
                catch (Exception exception)
                {
                    Console.Error.WriteLine("{0}: {1}\n{2}", exception.GetType().ToString(), exception.Message, exception.StackTrace);
                    return 1;
                }
            }
        }

        private static int SmiTokenDump()
        {
            for (uint tokenId = 0; tokenId <= 0xFFFF; tokenId++)
            {
                int retryCount = 5;
                bool success = false;
                while (!success && retryCount > 0)
                {
                    try
                    {
                        SmiObject? token = DellSmbiosSmi.GetToken((Token)tokenId);
                        success = true;
                        Console.WriteLine("{0:X4}\t{1}\t{2}\t{3}\t{4}", tokenId, token?.Output1, token?.Output2, token?.Output3, token?.Output4);
                    }
                    catch (Exception)
                    {
                        retryCount--;
                    }
                }
            }
            return 0;
        }

        private static int SmiGetToken(string[] args)
        {
            uint token = uint.Parse(args[1], NumberStyles.HexNumber);

            Console.WriteLine("Reading token {0:X4}", token);

            uint? currentValue = DellSmbiosSmi.GetTokenCurrentValue((Token)token);

            if (currentValue != null)
            {
                Console.WriteLine("  Current value: {0}", currentValue);

                uint? expectedValue = DellSmbiosSmi.GetTokenSetValue((Token)token);

                if (expectedValue != null)
                {
                    Console.WriteLine("  Expected value: {0}", expectedValue);
                    return 0;
                }
                else
                {
                    Console.WriteLine("  Failed to read expected value.");
                    return 1;
                }
            }
            else
            {
                Console.WriteLine("  Failed to read current value.");
                return 1;
            }
        }

        private static int SmiSetToken(string[] args)
        {
            uint token = uint.Parse(args[1], NumberStyles.HexNumber);
            uint targetValue = uint.Parse(args[2]);

            Console.WriteLine("Setting token {0:X4} to value {1}", token, targetValue);

            uint? currentValue = DellSmbiosSmi.GetTokenCurrentValue((Token)token);

            if (currentValue != null)
            {
                Console.WriteLine("  Current value: {0}", currentValue);
            }
            else
            {
                Console.WriteLine("  Failed to read current value.  Trying to set anyway.");
            }

            if (DellSmbiosSmi.SetToken((Token)token, targetValue))
            {
                Console.WriteLine("  Set token successfully.");

                currentValue = DellSmbiosSmi.GetTokenCurrentValue((Token)token);
                if (currentValue != null)
                {
                    Console.WriteLine("  Current value: {0}", currentValue);

                    if (currentValue == targetValue)
                    {
                        return 0;
                    }
                    else if (currentValue != null)
                    {
                        Console.WriteLine("  ...It appears that the value was not set as expected.");
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    Console.WriteLine("  Failed to read new value.");
                    return 1;
                }
            }
            else
            {
                Console.WriteLine("  Failed to set value.");
                return 1;
            }
        }
    }
}
