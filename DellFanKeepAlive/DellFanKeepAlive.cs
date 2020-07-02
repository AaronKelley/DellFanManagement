using DellFanManagement.Interop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace DellFanManagement.KeepAlive
{
    /// <summary>
    /// Main program class.
    /// </summary>
    static class DellFanKeepAlive
    {
        /// <summary>
        /// Keep track of the lowest temperature on record for each component.
        /// </summary>
        private static readonly Dictionary<string, int> minValues = new Dictionary<string, int>();
        
        /// <summary>
        /// Keep track of the highest temperature on record for each component.
        /// </summary>
        private static readonly Dictionary<string, int> maxValues = new Dictionary<string, int>();

        /// <summary>
        /// Switch to true if anything is written to the error log.
        /// </summary>
        private static bool errorsDetected = false;

        /// <summary>
        /// Main method; start of the program.
        /// </summary>
        /// <param name="args">Command-line parameters</param>
        /// <returns>Status/error code</returns>
        static int Main(string[] args)
        {
            try
            {
                int temperatureLowerThreshold = int.Parse(args[0]);
                int temperatureUpperThreshold = int.Parse(args[1]);
                ulong rpmThreshold = ulong.Parse(args[2]);
                
                int sleepInterval = 1;
                if (args.Length > 3)
                {
                    sleepInterval = int.Parse(args[3]);
                }

                FixConsole();

                Console.Title = "DellFanKeepAlive";
                Console.OutputEncoding = Encoding.UTF8;
                Console.BackgroundColor = ConsoleColor.DarkRed;

                string titleText =
                    "DellFanKeepAlive version " + DellFanLib.Version + " • By Aaron Kelley • GPLv3 • https://github.com/AaronKelley/DellFanManagement\n" +
                    "Dell SMM I/O driver by 424778940z • https://github.com/424778940z/bzh-windrv-dell-smm-io\n" +
                    "Open Hardware Monitor version 0.9.5 • Copyright © 2009-2020 Michael Möller and contributors (MPL 2.0)\n" +
                    "Icon made from Icon Fonts (http://www.onlinewebfonts.com/icon/), licensed by CC BY 3.0\n" +
                    "\n" +
                    "Configuration: lower temperature threshold " + temperatureLowerThreshold + ", upper temperature threshold " + temperatureUpperThreshold + ", RPM threshold " + rpmThreshold + "\n" +
                    "\n";

                if (DellFanLib.Initialize())
                {
                    TemperatureReader temperatureReader = new TemperatureReader();
                    DellFanLib.EnableEcFanControl();
                    bool ecFanControlEnabled = true;
                    DateTime ecFanControlChangeTime = DateTime.Now;
                    DateTime lastUpdateTime = DateTime.Now;
                    StringBuilder statusString = new StringBuilder();

                    ulong rpm1;
                    ulong rpm2 = ulong.MaxValue;
                    bool fan2Present = true;
                    bool firstRun = true;

                    Console.Clear();

                    while (true)
                    {
                        try
                        {
                            // Check last update time.
                            DateTime currentTime = DateTime.Now;
                            if (((DateTimeOffset)currentTime).ToUnixTimeSeconds() - ((DateTimeOffset)lastUpdateTime).ToUnixTimeSeconds() > 30)
                            {
                                string message = string.Format("A long time passed between updates - {0} - {1}", lastUpdateTime.ToString(), currentTime.ToString());
                                lastUpdateTime = currentTime;
                                throw new Exception(message);
                            }

                            lastUpdateTime = currentTime;

                            statusString.Clear();
                            statusString.Append(titleText);
                            statusString.AppendFormat("Current time: {0,-30}\n\n", lastUpdateTime);

                            bool thresholdsMet = true;

                            IReadOnlyDictionary<string, int> temperatures = temperatureReader.GetCpuCoreTemperatures();
                            foreach (string key in temperatures.Keys)
                            {
                                int temperature = temperatures[key];

                                if (temperature > (ecFanControlEnabled ? temperatureLowerThreshold : temperatureUpperThreshold))
                                {
                                    thresholdsMet = false;
                                }
                            }

                            rpm1 = DellFanLib.GetFanRpm(FanIndex.Fan1);

                            if (fan2Present)
                            {
                                rpm2 = DellFanLib.GetFanRpm(FanIndex.Fan2);

                                if (rpm2 == uint.MaxValue && firstRun)
                                {
                                    // No fan 2.
                                    fan2Present = false;
                                }

                                firstRun = false;
                            }

                            if (rpm1 == 0 || rpm1 > rpmThreshold || (fan2Present && (rpm2 == 0 || rpm2 > rpmThreshold)))
                            {
                                thresholdsMet = false;
                            }

                            statusString.AppendFormat("Temperature and fan speed thresholds are {0,-8}\n", thresholdsMet ? "met." : "not met.");
                            statusString.AppendFormat("{0,-75}\n\n", ecFanControlEnabled ?
                                string.Format("The EC has had control of the system fans since {0}.", ecFanControlChangeTime.ToString()) :
                                string.Format("The fan speed has been locked since {0}.", ecFanControlChangeTime.ToString())
                            );

                            statusString.Append("System temperatures:\n");
                            foreach (string key in temperatures.Keys)
                            {
                                string temperature = temperatures[key] != 0 ? temperatures[key].ToString() : "--";
                                statusString.AppendFormat("  {0,-14}{1,3}", key, temperature);
                                CheckValues(key, temperatures[key]);
                                if (minValues.ContainsKey(key) && maxValues.ContainsKey(key))
                                {
                                    statusString.AppendFormat("  ( {0,3} - {1,3} )", minValues[key], maxValues[key]);
                                }
                                statusString.Append("\n");
                            }

                            statusString.Append("\n");
                            statusString.Append("Fan speeds:\n");
                            statusString.AppendFormat("  {0,-9}{1,5}\n", "Fan #1", rpm1);
                            if (fan2Present)
                            {
                                statusString.AppendFormat("  {0,-9}{1,5}\n", "Fan #2", rpm2);
                            }

                            if (ecFanControlEnabled && thresholdsMet)
                            {
                                DellFanLib.DisableEcFanControl();
                                ecFanControlEnabled = false;
                                ecFanControlChangeTime = DateTime.Now;
                                Console.BackgroundColor = ConsoleColor.DarkBlue;
                                Console.Clear();
                            }
                            else if (!ecFanControlEnabled && !thresholdsMet)
                            {
                                DellFanLib.EnableEcFanControl();
                                ecFanControlEnabled = true;
                                ecFanControlChangeTime = DateTime.Now;
                                Console.BackgroundColor = ConsoleColor.DarkRed;
                                Console.Clear();
                            }

                            if (errorsDetected)
                            {
                                statusString.Append("\n!! Errors detected, check error log file for details !!\n");
                            }

                            FixConsole();
                            Console.CursorLeft = 0;
                            Console.CursorTop = 0;
                            Console.Write(statusString);
                        }
                        catch (Exception exception)
                        {
                            LogError(exception);
                        }

                        Thread.Sleep(1000 * sleepInterval);
                    }
                }
                else
                {
                    Console.Error.WriteLine("Failed to start driver.");
                    Console.Error.WriteLine();
                    Console.Error.WriteLine("Please make sure that bzh_dell_smm_io_x64.sys is present in the working directory,");
                    Console.Error.WriteLine("and that measures needed to allow the signature to be verified have been performed.");
                    Console.Error.WriteLine("Also, make sure that you are running the program \"as administrator\".");
                }
            }
            catch (DllNotFoundException)
            {
                Console.Error.WriteLine("Unable to load DellFanLib.dll");
                Console.Error.WriteLine("Make sure that the file is present.  If it is, install the required Visual C++ redistributable:");
                Console.Error.WriteLine("https://aka.ms/vs/16/release/vc_redist.x64.exe");
                return -1;
            }
            catch (Exception exception)
            {
                Console.Error.WriteLine("Error: {0}", exception.Message);
                Console.Error.WriteLine(exception.StackTrace);
            }

            DellFanLib.Shutdown();
            return 0;
        }

        /// <summary>
        /// Update minimum and maximum temperature readings for each component.
        /// </summary>
        /// <param name="key">Component name</param>
        /// <param name="value">Current temperature value</param>
        private static void CheckValues(string key, int value)
        {
            if (value != 0)
            {
                if (!minValues.ContainsKey(key) || value < minValues[key])
                {
                    minValues[key] = value;
                }

                if (!maxValues.ContainsKey(key) || value > maxValues[key])
                {
                    maxValues[key] = value;
                }
            }
        }

        /// <summary>
        /// Make sure that the console size snaps back if there is a change.
        /// </summary>
        private static void FixConsole()
        {
            // Try to change the console size (silently fail if there is a problem).
            try
            {
                if (Console.WindowHeight != 30)
                {
                    Console.WindowHeight = 30;
                }

                if (Console.WindowWidth != 120)
                {
                    Console.WindowWidth = 120;
                }

                if (Console.BufferHeight != 30)
                {
                    Console.BufferHeight = 30;
                }

                if (Console.BufferWidth != 120)
                {
                    Console.BufferWidth = 120;
                }
            }
            catch (Exception)
            {
                // No action.
            }
        }

        /// <summary>
        /// If an exception occurs during normal operation, write the details to a log file.
        /// </summary>
        /// <param name="exception">Exception to log</param>
        private static void LogError(Exception exception)
        {
            errorsDetected = true;

            string errorText = string.Format("{0} - {1} {2}\n{3}\n\n", DateTime.Now.ToString(), exception.GetType().ToString(), exception.Message, exception.StackTrace);
            File.AppendAllText("DellFanKeepAlive-Errors.log", errorText);
        }
    }
}
