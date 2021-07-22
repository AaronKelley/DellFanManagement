using DellFanManagement.DellSmbiozBzhLib.PInvoke;
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace DellFanManagement.DellSmbiozBzhLib
{
    /// <summary>
    /// Handles issuing commands to the Dell SMBIOS via the BZH SMM I/O driver.
    /// </summary>
    /// <remarks>Derived from "Dell Fan Utility" by 424778940z.</remarks>
    /// <seealso cref="https://github.com/424778940z/bzh-windrv-dell-smm-io"/>
    /// <seealso cref="https://github.com/424778940z/dell-fan-utility"/>
    public static class DellSmbiosBzh
    {
        /// <summary>
        /// Version number for the entire package.
        /// </summary>
        public const string Version = "DEV";

        /// <summary>
        /// Value to pass to ExecuteCommand when no parameter is needed.
        /// </summary>
        private const uint NoParameter = 0;

        /// <summary>
        /// Internal name of the SMM I/O driver/service.
        /// </summary>
        private const string DriverName = "BZHDELLSMMIO";

        /// <summary>
        /// Path to use when opening a handle to the driver.
        /// </summary>
        private const string DriverDevicePath = @"\\.\" + DriverName;

        /// <summary>
        /// Name of the driver file.
        /// </summary>
        private const string DriverFilename = "bzh_dell_smm_io_x64.sys";

        /// <summary>
        /// "Secret key" used to communicate with the driver.
        /// </summary>
        private const int IoctlCode = (0xB424 << 16) | (0xB42 << 2);

        /// <summary>
        /// Driver handle, set during initialization.
        /// </summary>
        private static IntPtr DriverHandle = IntPtr.Zero;

        /// <summary>
        /// Represents whether or not the driver has been initialized.
        /// </summary>
        private static bool IsInitialized = false;

        /// <summary>
        /// Disable EC fan control.
        /// </summary>
        /// <param name="alternate">If true, attempt alternate method to disable EC fan control.</param>
        /// <returns>True on success, false on failure.</returns>
        public static bool DisableEcFanControl(bool alternate = false)
        {
            return ExecuteCommand(alternate ? SmbiosCommand.DisableEcFanControlAlternate : SmbiosCommand.DisableEcFanControl) != null;
        }

        /// <summary>
        /// Enable EC fan control.
        /// </summary>
        /// <param name="alternate">If ture, attempt alternate method to enable EC fan control.</param>
        /// <returns>True on success, false on failure.</returns>
        public static bool EnableEcFanControl(bool alternate = false)
        {
            return ExecuteCommand(alternate ? SmbiosCommand.EnableEcFanControlAlternate : SmbiosCommand.EnableEcFanControl) != null;
        }

        /// <summary>
        /// Set the fan level of one of the fans.
        /// </summary>
        /// <param name="fanIndex">Which fan to set the level of.</param>
        /// <param name="fanLevel">Which fan level to set.</param>
        /// <returns>ulong-max means failure.</returns>
        public static bool SetFanLevel(FanIndex fanIndex, FanLevel fanLevel)
        {
            uint parameter = ((uint)fanLevel << 8) | (uint)fanIndex;
            return ExecuteCommand(SmbiosCommand.SetFanLevel, parameter) != null;
        }

        /// <summary>
        /// Get the speed of a specific fan.
        /// </summary>
        /// <param name="fanIndex">Which fan to get the speed of.</param>
        /// <returns>Speed in RPM; null means failure.</returns>
        public static uint? GetFanRpm(FanIndex fanIndex)
        {
            uint? result = ExecuteCommand(SmbiosCommand.GetFanRpm, (uint)fanIndex);

            return result;
        }

        /// <summary>
        /// Load the SMM I/O driver and set things up.
        /// </summary>
        /// <returns>False if the driver failed to load.</returns>
        public static bool Initialize()
        {
            bool result;

            // Check to see if the driver has already been loaded, and if so, just grab a handle to that.
            DriverHandle = ServiceMethods.CreateFile(DriverDevicePath, FileAccess.ReadWrite, FileShare.None, IntPtr.Zero, FileMode.Open, FileAttributes.Normal, IntPtr.Zero);

            // If the driver is not running, install it
            if (DriverHandle == ServiceMethods.InvalidHandleValue)
            {
                string driverPath = string.Format("{0}{1}{2}", Directory.GetCurrentDirectory(), Path.DirectorySeparatorChar, DriverFilename);
                result = InstallDriver(driverPath, true);
                if (!result)
                {
                    return false;
                }

                result = StartDriver();

                if (!result)
                {
                    return false;
                }

                DriverHandle = ServiceMethods.CreateFile(DriverDevicePath, FileAccess.ReadWrite, FileShare.ReadWrite, IntPtr.Zero, FileMode.Open, FileAttributes.Normal, IntPtr.Zero);

                if (DriverHandle == ServiceMethods.InvalidHandleValue)
                {
                    return false;
                }
            }
            IsInitialized = true;
            return true;
        }

        /// <summary>
        /// Unload the EC I/O driver.
        /// </summary>
        public static void Shutdown()
        {
            if (DriverHandle != ServiceMethods.InvalidHandleValue)
            {
                ServiceMethods.CloseHandle(DriverHandle);
            }
            RemoveDriver();
            IsInitialized = false;
        }

        /// <summary>
        /// Install the SMM I/O driver.
        /// </summary>
        /// <returns>True if successful, false if not.</returns>
        private static bool InstallDriver(string pszDriverPath, bool IsDemandLoaded)
        {
            IntPtr serviceManagerHandle;
            IntPtr serviceHandle;

            // Remove any previous instance of the driver
            RemoveDriver();

            serviceManagerHandle = ServiceMethods.OpenSCManager(null, null, ServiceAccess.ServiceManagerAllAccess);

            if (serviceManagerHandle != IntPtr.Zero)
            {
                // Install the driver

                ServiceStartType startType = (IsDemandLoaded == true) ? ServiceStartType.DemandStart : ServiceStartType.SystemStart;
                serviceHandle = ServiceMethods.CreateService(serviceManagerHandle, DriverName, DriverName, ServiceAccess.AllAccess, ServiceType.KernelDriver, startType, ServiceErrorControl.Normal, pszDriverPath, null, IntPtr.Zero, null, null, null);

                ServiceMethods.CloseServiceHandle(serviceManagerHandle);

                if (serviceHandle == IntPtr.Zero)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

            ServiceMethods.CloseServiceHandle(serviceHandle);

            return true;
        }

        /// <summary>
        /// Remove the SMM I/O driver.
        /// </summary>
        /// <returns>True if successful, false if not.</returns>
        private static bool RemoveDriver()
        {
            IntPtr serviceManagerHandle;
            IntPtr serviceHandle;
            bool result;

            StopDriver();

            serviceManagerHandle = ServiceMethods.OpenSCManager(null, null, ServiceAccess.ServiceManagerAllAccess);

            if (serviceManagerHandle == IntPtr.Zero)
            {
                return false;
            }

            serviceHandle = ServiceMethods.OpenService(serviceManagerHandle, DriverName, ServiceAccess.AllAccess);
            ServiceMethods.CloseServiceHandle(serviceManagerHandle);

            if (serviceManagerHandle == IntPtr.Zero)
            {
                return false;
            }

            result = ServiceMethods.QueryServiceConfig(serviceHandle, IntPtr.Zero, 0, out int bytesNeeded);

            if ((ErrorCode)Marshal.GetLastWin32Error() == ErrorCode.InsufficientBuffer)
            {
                IntPtr serviceConfigurationPtr = Marshal.AllocHGlobal(bytesNeeded);

                result = ServiceMethods.QueryServiceConfig(serviceHandle, serviceConfigurationPtr, bytesNeeded, out _);
                QueryServiceConfig serviceConfiguration = (QueryServiceConfig) Marshal.PtrToStructure(serviceConfigurationPtr, typeof(QueryServiceConfig));

                if (!result)
                {
                    Marshal.FreeHGlobal(serviceConfigurationPtr);
                    ServiceMethods.CloseServiceHandle(serviceHandle);
                    return result;
                }

                // If service is set to load automatically, don't delete it!
                if (serviceConfigurationPtr != IntPtr.Zero && serviceConfiguration.startType == ServiceStartType.DemandStart)
                {
                    result = ServiceMethods.DeleteService(serviceHandle);
                }

                Marshal.FreeHGlobal(serviceConfigurationPtr);
            }

            ServiceMethods.CloseServiceHandle(serviceHandle);

            return result;
        }

        /// <summary>
        /// Start the SMM I/O driver.
        /// </summary>
        /// <returns>True on success, false on failure.</returns>
        private static bool StartDriver()
        {
            IntPtr serviceManagerHandle;
            IntPtr serviceHandle;
            bool result;

            serviceManagerHandle = ServiceMethods.OpenSCManager(null, null, ServiceAccess.ServiceManagerAllAccess);

            if (serviceManagerHandle != IntPtr.Zero)
            {
                serviceHandle = ServiceMethods.OpenService(serviceManagerHandle, DriverName, ServiceAccess.AllAccess);

                ServiceMethods.CloseServiceHandle(serviceManagerHandle);

                if (serviceHandle != IntPtr.Zero)
                {
                    result = ServiceMethods.StartService(serviceHandle, 0, null) || (ErrorCode)Marshal.GetLastWin32Error() == ErrorCode.ServiceAlreadyRunning;

                    ServiceMethods.CloseServiceHandle(serviceHandle);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

            return result;
        }

        /// <summary>
        /// Stop the SMM I/O driver.
        /// </summary>
        /// <returns>True on success, false on failure.</returns>
        private static bool StopDriver()
        {
            IntPtr serviceManagerHandle;
            IntPtr serviceHandle;
            ServiceStatus serviceStatus = new ServiceStatus();
            bool result;

            serviceManagerHandle = ServiceMethods.OpenSCManager(null, null, ServiceAccess.ServiceManagerAllAccess);

            if (serviceManagerHandle != IntPtr.Zero)
            {
                serviceHandle = ServiceMethods.OpenService(serviceManagerHandle, DriverName, ServiceAccess.AllAccess);

                ServiceMethods.CloseServiceHandle(serviceManagerHandle);

                if (serviceHandle != IntPtr.Zero)
                {
                    result = ServiceMethods.ControlService(serviceHandle, ServiceControl.Stop, ref serviceStatus);
                    ServiceMethods.CloseServiceHandle(serviceHandle);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

            return result;
        }

        /// <summary>
        /// Execute an arbitrary command against the SMBIOS.
        /// </summary>
        /// <param name="driverHandle">Handle to the SMM I/O driver/service.</param>
        /// <param name="command">Command identifier.</param>
        /// <param name="argument">Command argument/parameter.</param>
        /// <returns>Command result (null means failure).</returns>
        public static uint? ExecuteCommand(SmbiosCommand command, uint argument = NoParameter)
        {
            if (!IsInitialized)
            {
                return null;
            }

            SmbiosPackage package = new SmbiosPackage
            {
                Command = (uint)command,
                Data = argument,
                Output1 = 0,
                Output2 = 0
            };

            uint resultSize = 0;

            bool status = ServiceMethods.DeviceIoControl(DriverHandle, IoctlCode, ref package, Marshal.SizeOf(package), ref package, Marshal.SizeOf(package), ref resultSize, IntPtr.Zero);

            if (status == false)
            {
                return null;
            }
            else
            {
                if (package.Command != uint.MaxValue)
                {
                    return package.Command;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
