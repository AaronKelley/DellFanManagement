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
                    // TODO: Show error message box.
                    Console.Error.WriteLine("{0}: {1}\n{2}", exception.GetType().ToString(), exception.Message, exception.StackTrace);
                }
            }
            else
            {
                // CMD mode.
                try
                {
                    if (args[0] == "PackageTest")
                    {
                        PackageTest.RunPackageTests();
                    }
                    else if (args[0] == "SetThermalSetting")
                    {
                        SetThermalSetting.ExecuteSetThermalSetting(args);
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
