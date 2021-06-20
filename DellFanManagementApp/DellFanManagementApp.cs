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
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                // GUI mode.
                try
                {
                    Application.SetHighDpiMode(HighDpiMode.SystemAware);
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new DellFanManagementGuiForm());
                }
                catch (Exception exception)
                {
                    MessageBox.Show(string.Format("{0}: {1}\n{2}", exception.GetType().ToString(), exception.Message, exception.StackTrace), "Error starting application", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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

                    if (args[0].ToLower() == "packagetest")
                    {
                        PackageTest.RunPackageTests();
                    }
                    else if (args[0].ToLower() == "setthermalsetting")
                    {
                        SetThermalSetting.ExecuteSetThermalSetting(args);
                    }
                    else
                    {
                        DellFanCmd.ProcessCommand(args);
                    }
                }
                catch (Exception exception)
                {
                    Console.Error.WriteLine("{0}: {1}\n{2}", exception.GetType().ToString(), exception.Message, exception.StackTrace);
                }
            }
        }
    }
}
