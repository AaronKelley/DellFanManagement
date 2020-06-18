using DellFanManagement.Interop;
using System;
using System.Threading;

namespace DellFanManagement.Cmd
{
    /// <summary>
    /// Single class which contains the entire program.
    /// </summary>
    static class DellFanCmd
    {
        /// <summary>
        /// Main method; start of the program.
        /// </summary>
        /// <param name="args">Command-line parameters</param>
        /// <returns>Status/error code</returns>
        static int Main(string[] args)
        {
            int returnCode = 0;

            try
            {
                Console.WriteLine("DellFanCmd {0}", DellFanLib.Version);
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

                if (args.Length != 1)
                {
                    Usage();
                }
                else
                {
                    // Behavior variables.
                    bool disableEcFanControl = false;
                    bool enableEcFanControl = false;
                    bool setFansToMax = false;
                    bool useAlternateCommand = false;
                    bool setFanLevel = false;
                    bool getFanRpm = false;
                    bool runTest = false;
                    FanIndex fanSelection = FanIndex.Fan1;
                    FanLevel fanLevel = FanLevel.Level0;

                    // Figure out what was requested.
                    if (args[0] == "ec-disable")
                    {
                        disableEcFanControl = true;
                        setFansToMax = true;
                    }
                    else if (args[0] == "ec-disable-nofanchg")
                    {
                        disableEcFanControl = true;
                    }
                    else if (args[0] == "ec-enable")
                    {
                        enableEcFanControl = true;
                    }
                    else if (args[0] == "ec-disable-alt")
                    {
                        disableEcFanControl = true;
                        setFansToMax = true;
                        useAlternateCommand = true;
                    }
                    else if (args[0] == "ec-disable-alt-nofanchg")
                    {
                        disableEcFanControl = true;
                        useAlternateCommand = true;
                    }
                    else if (args[0] == "ec-enable-alt")
                    {
                        enableEcFanControl = true;
                        useAlternateCommand = true;
                    }
                    else if (args[0] == "fan1-level0")
                    {
                        setFanLevel = true;
                        fanSelection = FanIndex.Fan1;
                        fanLevel = FanLevel.Level0;
                    }
                    else if (args[0] == "fan1-level1")
                    {
                        setFanLevel = true;
                        fanSelection = FanIndex.Fan1;
                        fanLevel = FanLevel.Level1;
                    }
                    else if (args[0] == "fan1-level2")
                    {
                        setFanLevel = true;
                        fanSelection = FanIndex.Fan1;
                        fanLevel = FanLevel.Level2;
                    }
                    else if (args[0] == "fan2-level0")
                    {
                        setFanLevel = true;
                        fanSelection = FanIndex.Fan2;
                        fanLevel = FanLevel.Level0;
                    }
                    else if (args[0] == "fan2-level1")
                    {
                        setFanLevel = true;
                        fanSelection = FanIndex.Fan2;
                        fanLevel = FanLevel.Level1;
                    }
                    else if (args[0] == "fan2-level2")
                    {
                        setFanLevel = true;
                        fanSelection = FanIndex.Fan2;
                        fanLevel = FanLevel.Level2;
                    }
                    else if (args[0] == "rpm-fan1")
                    {
                        getFanRpm = true;
                        fanSelection = FanIndex.Fan1;
                    }
                    else if (args[0] == "rpm-fan2")
                    {
                        getFanRpm = true;
                        fanSelection = FanIndex.Fan2;
                    }
                    else if (args[0] == "test")
                    {
                        runTest = true;
                    }
                    else if (args[0] == "test-alt")
                    {
                        runTest = true;
                        useAlternateCommand = true;
                    }
                    else
                    {
                        Usage();
                        return -3;
                    }

                    // Execute request.

                    // Load driver first.
                    if (LoadDriver())
                    {
                        if (disableEcFanControl)
                        {
                            // Disable EC fan control.
                            Console.WriteLine("Attempting to disable EC control of the fan...");

                            ulong result = DellFanLib.DisableEcFanControl(useAlternateCommand);

                            if (result == ulong.MaxValue)
                            {
                                Console.Error.WriteLine("Failed.");
                                UnloadDriver();
                                return -1;
                            }

                            Console.WriteLine(" ...Success.");

                            if (setFansToMax)
                            {
                                // Crank the fans up, for safety.
                                Console.WriteLine("Setting fan 1 speed to maximum...");
                                result = DellFanLib.SetFanLevel(FanIndex.Fan1, FanLevel.Level2);
                                if (result == ulong.MaxValue)
                                {
                                    Console.Error.WriteLine("Failed.");
                                }

                                Console.WriteLine("Setting fan 2 speed to maximum...");
                                result = DellFanLib.SetFanLevel(FanIndex.Fan2, FanLevel.Level2);
                                if (result == ulong.MaxValue)
                                {
                                    Console.Error.WriteLine("Failed.  (Maybe your system just has one fan?)");
                                }
                            }
                            else
                            {
                                Console.WriteLine("WARNING: CPU and GPU are not designed to run under load without active cooling.");
                                Console.WriteLine("Make sure that you have alternate fan speed control measures in place.");
                            }
                        }
                        else if (enableEcFanControl)
                        {
                            // Enable EC fan control.
                            Console.WriteLine("Attempting to enable EC control of the fan...");

                            ulong result = DellFanLib.EnableEcFanControl(useAlternateCommand);

                            if (result == ulong.MaxValue)
                            {
                                Console.Error.WriteLine("Failed.");
                                UnloadDriver();
                                return -1;
                            }

                            Console.WriteLine(" ...Success.");
                        }
                        else if (setFanLevel)
                        {
                            // Set the fan to a specific level.
                            Console.WriteLine("Attempting to set the fan level...");
                            ulong result = DellFanLib.SetFanLevel(fanSelection, fanLevel);

                            if (result == ulong.MaxValue)
                            {
                                Console.Error.WriteLine("Failed.");
                                UnloadDriver();
                                return -1;
                            }

                            Console.WriteLine(" ...Success.");
                        }
                        else if (getFanRpm)
                        {
                            // Query the fan RPM.
                            Console.WriteLine("Attempting to query the fan RPM...");
                            ulong result = DellFanLib.GetFanRpm(fanSelection);

                            if (result == ulong.MaxValue)
                            {
                                Console.Error.WriteLine("Failed.");
                                UnloadDriver();
                                return -1;
                            }

                            Console.WriteLine(" Result: {0}", result);
                            returnCode = int.Parse(result.ToString());
                        }
                        else if (runTest)
                        {
                            // Test all of the fan levels and report RPMs.

                            ulong rpmIdleFan1;
                            ulong rpmLevel0Fan1;
                            ulong rpmLevel1Fan1;
                            ulong rpmLevel2Fan1;

                            ulong? rpmIdleFan2 = null;
                            ulong? rpmLevel0Fan2 = null;
                            ulong? rpmLevel1Fan2 = null;
                            ulong? rpmLevel2Fan2 = null;

                            int sleepInterval = 7500;
                            bool fan2Present = true;

                            // Disable EC fan control.
                            Console.WriteLine("Disabling EC fan control...");
                            DellFanLib.DisableEcFanControl(useAlternateCommand);

                            // Query current idle fan levels.
                            rpmIdleFan1 = DellFanLib.GetFanRpm(FanIndex.Fan1);
                            DellFanLib.SetFanLevel(FanIndex.Fan1, FanLevel.Level0);

                            rpmIdleFan2 = DellFanLib.GetFanRpm(FanIndex.Fan2);
                            ulong result = DellFanLib.SetFanLevel(FanIndex.Fan2, FanLevel.Level0);

                            if (result == uint.MaxValue)
                            {
                                // No fan 2?
                                fan2Present = false;
                                Console.WriteLine("Looks like fan 2 is not present, system only has one fan?");
                            }

                            // Measure fan 1.
                            Console.WriteLine("Measuring: Fan 1, level 0...");
                            Thread.Sleep(sleepInterval);
                            rpmLevel0Fan1 = DellFanLib.GetFanRpm(FanIndex.Fan1);

                            Console.WriteLine("Measuring: Fan 1, level 1..."); 
                            DellFanLib.SetFanLevel(FanIndex.Fan1, FanLevel.Level1);
                            Thread.Sleep(sleepInterval);
                            rpmLevel1Fan1 = DellFanLib.GetFanRpm(FanIndex.Fan1);

                            Console.WriteLine("Measuring: Fan 1, level 2..."); 
                            DellFanLib.SetFanLevel(FanIndex.Fan1, FanLevel.Level2);
                            Thread.Sleep(sleepInterval);
                            rpmLevel2Fan1 = DellFanLib.GetFanRpm(FanIndex.Fan1);

                            DellFanLib.SetFanLevel(FanIndex.Fan1, FanLevel.Level0);

                            if (fan2Present)
                            {
                                // Measure fan 2.
                                Console.WriteLine("Measuring: Fan 2, level 0...");
                                rpmLevel0Fan2 = DellFanLib.GetFanRpm(FanIndex.Fan2);

                                Console.WriteLine("Measuring: Fan 2, level 1..."); 
                                DellFanLib.SetFanLevel(FanIndex.Fan2, FanLevel.Level1);
                                Thread.Sleep(sleepInterval);
                                rpmLevel1Fan2 = DellFanLib.GetFanRpm(FanIndex.Fan2);

                                Console.WriteLine("Measuring: Fan 2, level 2..."); 
                                DellFanLib.SetFanLevel(FanIndex.Fan2, FanLevel.Level2);
                                Thread.Sleep(sleepInterval);
                                rpmLevel2Fan2 = DellFanLib.GetFanRpm(FanIndex.Fan2);
                            }

                            // Enable EC fan control.
                            Console.WriteLine("Enabling EC fan control...");
                            DellFanLib.EnableEcFanControl(useAlternateCommand);

                            Console.WriteLine("Test procedure is finished.");
                            Console.WriteLine();
                            Console.WriteLine();

                            // Output results.
                            Console.WriteLine("Fan 1 initial speed: {0}", rpmIdleFan1);
                            if (fan2Present)
                            {
                                Console.WriteLine("Fan 2 initial speed: {0}", rpmIdleFan2);
                            }
                            Console.WriteLine();

                            Console.WriteLine("Fan 1 level 0 speed: {0}", rpmLevel0Fan1);
                            Console.WriteLine("Fan 1 level 1 speed: {0}", rpmLevel1Fan1);
                            Console.WriteLine("Fan 1 level 2 speed: {0}", rpmLevel2Fan1);
                            Console.WriteLine();

                            if (fan2Present)
                            {
                                Console.WriteLine("Fan 2 level 0 speed: {0}", rpmLevel0Fan2);
                                Console.WriteLine("Fan 2 level 1 speed: {0}", rpmLevel1Fan2);
                                Console.WriteLine("Fan 2 level 2 speed: {0}", rpmLevel2Fan2);
                                Console.WriteLine();
                            }
                        }

                        // Unload driver.
                        UnloadDriver();
                    }
                }
            }
            catch (DllNotFoundException)
            {
                Console.Error.WriteLine("Unable to load DellFanLib.dll");
                Console.Error.WriteLine("Make sure that the file is present.  If it is, install the required Visual C++ redistributable:");
                Console.Error.WriteLine("https://aka.ms/vs/16/release/vc_redist.x64.exe");
                returnCode = -1;
            }
            catch (Exception exception)
            {
                Console.Error.WriteLine("Error: {0}", exception.Message);
                Console.Error.WriteLine(exception.StackTrace);
                returnCode = -1;
                UnloadDriver();
            }

            return returnCode;
        }

        /// <summary>
        /// Print usage directions to the console.
        /// </summary>
        private static void Usage()
        {
            Console.Write(
                "Usage: DellFanCmd [command]\n" +
                "Available commands:\n" +
                "  ec-disable              Turn EC fan control off (fan goes to manual control)\n" +
                "  ec-disable-nofanchg     Turn EC fan control off and don't change the fan speed\n" +
                "  ec-enable               Turn EC fan control on (fan goes to automatic control)\n" +
                "  test                    Try turning EC fan control off,\n" +
                "                          and record the fan RPM at different levels.\n" +
                "  fan1-rpm                Report RPM for fan 1\n" +
                "  fan2-rpm                Report RPM for fan 2\n" +
                "                          (RPMs are reported via status/error code)\n" +
                "\n" +
                "After EC fan control is off, you may use:\n" +
                "  fan1-level0             Set fan 1 to level 0 (0%)\n" +
                "  fan1-level1             Set fan 1 to level 1 (50%)\n" +
                "  fan1-level2             Set fan 1 to level 2 (100%)\n" +
                "  fan2-level0             Set fan 2 to level 0 (0%)\n" +
                "  fan2-level1             Set fan 2 to level 1 (50%)\n" +
                "  fan2-level2             Set fan 2 to level 2 (100%)\n" +
                "\n" +
                "Append \"-alt\" to EC disable or enable commands to attempt alternate method.\n" +
                "(Example: ec-disable-alt)\n");
        }

        /// <summary>
        /// Load the SMM I/O driver.
        /// </summary>
        /// <returns>True if successful, false if not</returns>
        public static bool LoadDriver()
        {
            Console.WriteLine("Loading SMM I/O driver...");
            bool result = DellFanLib.Initialize();
            if (!result)
            {
                Console.Error.WriteLine("Failed.");
                Console.Error.WriteLine();
                Console.Error.WriteLine("Please make sure that bzh_dell_smm_io_x64.sys is present in the working directory,");
                Console.Error.WriteLine("and that measures needed to allow the signature to be verified have been performed.");
                Console.Error.WriteLine("Also, make sure that you are running the program \"as administrator\".");
                Console.Error.WriteLine();

                Console.WriteLine("Attempting driver cleanup after failed driver load, errors may follow.");
                UnloadDriver();

                return false;
            }

            Console.WriteLine(" ...Success.");
            return true;
        }

        /// <summary>
        /// Unload the SMM I/O driver.
        /// </summary>
        public static void UnloadDriver()
        {
            Console.WriteLine("Unloading SMM I/O driver...");
            DellFanLib.Shutdown();
            Console.WriteLine(" ...Done.");
        }
    }
}
