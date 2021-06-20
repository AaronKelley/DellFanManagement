using DellFanManagement.Interop;
using System;
using System.Windows.Forms;

namespace DellFanManagement.App
{
    static class DellFanManagementApp
    {
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
                        Application.SetHighDpiMode(HighDpiMode.SystemAware);
                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);
                        Application.Run(new DellFanManagementGuiForm());
                    }
                    else
                    {
                        MessageBox.Show("This program must be run with administrative privileges.", "Dell Fan Management privilege check", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show(string.Format("{0}: {1}\n{2}", exception.GetType().ToString(), exception.Message, exception.StackTrace), "Error starting application", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return 1;
                }

                return 0;
            }
            else
            {
                // CMD mode.
                try
                {
                    Console.WriteLine("Dell Fan Management, version {0}", DellFanLib.Version);
                    Console.WriteLine("By Aaron Kelley");
                    Console.WriteLine("Licensed under GPLv3");
                    Console.WriteLine("Source code available at https://github.com/AaronKelley/DellFanManagement");
                    Console.WriteLine();
                    Console.WriteLine("Dell SMM I/O driver by 424778940z");
                    Console.WriteLine("https://github.com/424778940z/bzh-windrv-dell-smm-io");
                    Console.WriteLine();
                    Console.WriteLine("Derived from \"Dell fan utility\" by 424778940z");
                    Console.WriteLine("https://github.com/424778940z/dell-fan-utility");
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
                        else
                        {
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
    }
}
