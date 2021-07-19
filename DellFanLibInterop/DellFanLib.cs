using DellFanManagement.Interop.PInvoke;
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace DellFanManagement.Interop
{
    public static class DellFanLib
    {
        /// <summary>
        /// Version number for the entire package.
        /// </summary>
        public static readonly string Version = "DEV";

        /// <summary>
        /// Value to pass to ExecuteCommand when no parameter is needed.
        /// </summary>
        private static readonly ulong NoParameter = 0;

        /// <summary>
        /// Internal name of the SMM I/O driver/service.
        /// </summary>
        private static readonly string DriverName = "BZHDELLSMMIO";

        /// <summary>
        /// Path to use when opening a handle to the driver.
        /// </summary>
        private static readonly string DriverDevicePath = @"\\.\" + DriverName;

        /// <summary>
        /// Name of the driver file.
        /// </summary>
        private static readonly string DriverFilename = "bzh_dell_smm_io_x64.sys";

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
        /// <returns>ulong-max means failure.</returns>
        public static ulong DisableEcFanControl(bool alternate = false)
        {
            return ExecuteCommand(alternate ? SmbiosCommand.DisableEcFanControlAlternate : SmbiosCommand.DisableEcFanControl, NoParameter);
        }

        /// <summary>
        /// Enable EC fan control.
        /// </summary>
        /// <param name="alternate">If ture, attempt alternate method to enable EC fan control.</param>
        /// <returns>ulong-max means failure.</returns>0
        public static ulong EnableEcFanControl(bool alternate = false)
        {
            return ExecuteCommand(alternate ? SmbiosCommand.EnableEcFanControlAlternate : SmbiosCommand.EnableEcFanControl, NoParameter);
        }

        /// <summary>
        /// Set the fan level of one of the fans.
        /// </summary>
        /// <param name="fanIndex">Which fan to set the level of.</param>
        /// <param name="fanLevel">Which fan level to set.</param>
        /// <returns>ulong-max means failure.</returns>
        public static ulong SetFanLevel(FanIndex fanIndex, FanLevel fanLevel)
        {
            ulong parameter = ((ulong)fanLevel << 8) | (ulong)fanIndex;
            return ExecuteCommand(SmbiosCommand.SetFanLevel, parameter);
        }

        /// <summary>
        /// Get the speed of a specific fan.
        /// </summary>
        /// <param name="fanIndex">Which fan to get the speed of.</param>
        /// <returns>Speed in RPM; uint-max means failure.</returns>
        public static ulong GetFanRpm(FanIndex fanIndex)
        {
            return ExecuteCommand(SmbiosCommand.GetFanRpm, (ulong)fanIndex);
        }

        /// <summary>
        /// Load the SMM I/O driver and set things up.
        /// </summary>
        /// <returns>False if the driver failed to load.</returns>
        public static bool Initialize()
        {
            bool result;

            // Check to see if the driver has already been loaded, and if so, just grab a handle to that.
            DriverHandle = CreateFile(DriverDevicePath, FileAccess.ReadWrite, FileShare.None, IntPtr.Zero, FileMode.Open, FileAttributes.Normal, IntPtr.Zero);

            // If the driver is not running, install it
            if (DriverHandle == InvalidHandleValue)
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

                DriverHandle = CreateFile(DriverDevicePath, FileAccess.ReadWrite, FileShare.ReadWrite, IntPtr.Zero, FileMode.Open, FileAttributes.Normal, IntPtr.Zero);

                if (DriverHandle == InvalidHandleValue)
                {
                    return false;
                }
            }
            IsInitialized = true;
            return true;
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

            serviceManagerHandle = OpenSCManager(null, null, ServiceAccess.AllAccess);

            if (serviceManagerHandle != IntPtr.Zero)
            {
                // Install the driver

                ServiceStartType startType = (IsDemandLoaded == true) ? ServiceStartType.DemandStart : ServiceStartType.SystemStart;
                serviceHandle = CreateService(serviceManagerHandle, DriverName, DriverName, ServiceAccess.AllAccess, ServiceType.KernelDriver, startType, ServiceErrorControl.Normal, pszDriverPath, null, IntPtr.Zero, null, null, null);

                CloseServiceHandle(serviceManagerHandle);

                if (serviceHandle == IntPtr.Zero)
                    return false;
            }
            else
                return false;

            CloseServiceHandle(serviceHandle);

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
            QueryServiceConfig serviceConfiguration = new QueryServiceConfig();
            bool result;

            StopDriver();

            serviceManagerHandle = OpenSCManager(null, null, ServiceAccess.AllAccess);

            if (serviceManagerHandle == IntPtr.Zero)
            {
                return false;
            }

            serviceHandle = OpenService(serviceManagerHandle, DriverName, ServiceAccess.AllAccess);
            CloseServiceHandle(serviceManagerHandle);

            if (serviceManagerHandle == IntPtr.Zero)
            {
                return false;
            }

            result = QueryServiceConfig(serviceHandle, IntPtr.Zero, 0, out int bytesNeeded);

            if (GetLastError() == ERROR_INSUFFICIENT_BUFFER)
            {
                IntPtr serviceConfigurationPtr = Marshal.AllocHGlobal(bytesNeeded);

                //serviceConfiguration = (LPQUERY_SERVICE_CONFIG)malloc(bufferSize);
                result = QueryServiceConfig(serviceHandle, serviceConfigurationPtr, bytesNeeded, out _);
                Marshal.PtrToStructure(serviceConfigurationPtr, serviceConfiguration);

                if (!result)
                {
                    Marshal.FreeHGlobal(serviceConfigurationPtr);
                    CloseServiceHandle(serviceHandle);
                    return result;
                }

                // If service is set to load automatically, don't delete it!
                if (serviceConfigurationPtr != IntPtr.Zero && serviceConfiguration.startType == ServiceStartType.DemandStart)
                {
                    result = DeleteService(serviceHandle);
                }

                Marshal.FreeHGlobal(serviceConfigurationPtr);
            }

            CloseServiceHandle(serviceHandle);

            return result;
        }

        /// <summary>
        /// Unload the EC I/O driver.
        /// </summary>
        [DllImport(@"DellFanLib.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Shutdown();

        /// <summary>
        /// Execute an arbitrary command against the SMBIOS.
        /// </summary>
        /// <param name="command">Command identifier.</param>
        /// <param name="argument">Command argument/parameter.</param>
        /// <returns>Command result *ulong-max means failure).</returns>
        [DllImport(@"DellFanLib.dll", EntryPoint = "DellSmmIo", CallingConvention = CallingConvention.Cdecl)]
        public static extern ulong ExecuteCommand(SmbiosCommand command, ulong argument);

        /// <summary>
        /// Closes a handle to a service control manager or service object.
        /// </summary>
        /// <param name="scObject">A handle to the service control manager object or the service object to
        /// close.</param>
        /// <returns>If the function succeeds, the return value is nonzero.</returns>
        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CloseServiceHandle(IntPtr scObject);

        /// <summary>
        /// Creates or opens a file or I/O device.
        /// </summary>
        /// <param name="filename">The name of the file or device to be created or opened.</param>
        /// <param name="desiredAccess">The requested access to the file or device.</param>
        /// <param name="share">The requested sharing mode of the file or device.</param>
        /// <param name="securityAttributes">A pointer to a SECURITY_ATTRIBUTES structure that contains two separate but
        /// related data members: an optional security descriptor, and a Boolean value that determines whether the
        /// returned handle can be inherited by child processes.</param>
        /// <param name="creationDisposition">An action to take on a file or device that exists or does not
        /// exist.</param>
        /// <param name="flagsAndAttributes">The file or device attributes and flags.</param>
        /// <param name="templateFile">A valid handle to a template file with the GENERIC_READ access right.</param>
        /// <returns>If the function succeeds, the return value is an open handle to the specified file, device, named
        /// pipe, or mail slot.  If the function fails, the return value is InvalidHandleValue.</returns>
        /// <seealso cref="http://pinvoke.net/default.aspx/kernel32/CreateFile.html"/>
        /// <seealso cref="https://docs.microsoft.com/en-us/windows/win32/api/fileapi/nf-fileapi-createfilew"/>
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CreateFile(
            [MarshalAs(UnmanagedType.LPTStr)] string filename,
            [MarshalAs(UnmanagedType.U4)] FileAccess desiredAccess,
            [MarshalAs(UnmanagedType.U4)] FileShare share,
            IntPtr securityAttributes,
            [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition,
            [MarshalAs(UnmanagedType.U4)] FileAttributes flagsAndAttributes,
            IntPtr templateFile
        );

        /// <summary>
        /// Creates a service object and adds it to the specified service control manager database.
        /// </summary>
        /// <param name="hSCManager">A handle to the service control manager database.</param>
        /// <param name="serviceName">The name of the service to install.</param>
        /// <param name="displayName">The display name to be used by user interface programs to identify the
        /// service.</param>
        /// <param name="desiredAccess">The access to the service.</param>
        /// <param name="serviceType">The service type.</param>
        /// <param name="startType">The service start options.</param>
        /// <param name="errorControl">The severity of the error, and action taken, if this service fails to
        /// start.</param>
        /// <param name="binaryPathName">The fully qualified path to the service binary file.</param>
        /// <param name="loadOrderGroup">The names of the load ordering group of which this service is a member.</param>
        /// <param name="tagId">A pointer to a variable that receives a tag value that is unique in the group specified
        /// in the lpLoadOrderGroup parameter.</param>
        /// <param name="dependencies">A pointer to a double null-terminated array of null-separated names of services
        /// or load ordering groups that the system must start before this service.</param>
        /// <param name="serviceStartName">The name of the account under which the service should run.</param>
        /// <param name="password">The password to the account name specified by the serviceStartName parameter.</param>
        /// <returns>If the function succeeds, the return value is a handle to the service.  If the function fails, the
        /// return value is NULL.</returns>
        /// <seealso cref="https://www.pinvoke.net/default.aspx/advapi32.createservice"/>
        /// <seealso cref="https://docs.microsoft.com/en-us/windows/win32/api/winsvc/nf-winsvc-createservicew"/>
        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CreateService(
            IntPtr hSCManager,
            [MarshalAs(UnmanagedType.LPTStr)] string serviceName,
            [MarshalAs(UnmanagedType.LPTStr)] string displayName,
            [MarshalAs(UnmanagedType.U4)] ServiceAccess desiredAccess,
            [MarshalAs(UnmanagedType.U4)] ServiceType serviceType,
            [MarshalAs(UnmanagedType.U4)] ServiceStartType startType,
            [MarshalAs(UnmanagedType.U4)] ServiceErrorControl errorControl,
            [MarshalAs(UnmanagedType.LPTStr)] string binaryPathName,
            [MarshalAs(UnmanagedType.LPTStr)] string loadOrderGroup,
            IntPtr tagId,
            [MarshalAs(UnmanagedType.LPTStr)] string dependencies,
            [MarshalAs(UnmanagedType.LPTStr)] string serviceStartName,
            [MarshalAs(UnmanagedType.LPTStr)] string password
        );

        /// <summary>
        /// Establishes a connection to the service control manager on the specified computer and opens the specified
        /// service control manager database.
        /// </summary>
        /// <param name="machineName">The name of the target computer.</param>
        /// <param name="databaseName">The name of the service control manager database.</param>
        /// <param name="desiredAccess">The access to the service control manager.</param>
        /// <returns>If the function succeeds, the return value is a handle to the specified service control manager
        /// database. If the function fails, the return value is NULL.</returns>
        /// <seealso cref="http://www.pinvoke.net/default.aspx/advapi32/OpenSCManager.html"/>
        /// <seealso cref="https://docs.microsoft.com/en-us/windows/win32/api/winsvc/nf-winsvc-openscmanagerw"/>
        [DllImport("advapi32.dll", EntryPoint = "OpenSCManagerW", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern IntPtr OpenSCManager(
            [MarshalAs(UnmanagedType.LPTStr)] string machineName,
            [MarshalAs(UnmanagedType.LPTStr)] string databaseName,
            [MarshalAs(UnmanagedType.U4)] ServiceAccess desiredAccess
        );

        /// <summary>
        /// Opens an existing service.
        /// </summary>
        /// <param name="scManager">A handle to the service control manager database.</param>
        /// <param name="serviceName">The name of the service to be opened.</param>
        /// <param name="desiredAccess">The access to the service.</param>
        /// <returns>If the function succeeds, the return value is a handle to the service. If the function fails, the
        /// return value is NULL.</returns>
        /// <seealso cref="http://www.pinvoke.net/default.aspx/advapi32.openservice"/>
        /// <seealso cref="https://docs.microsoft.com/en-us/windows/win32/api/winsvc/nf-winsvc-openservicew"/>
        [DllImport("advapi32.dll", EntryPoint = "OpenServiceW", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern IntPtr OpenService(
            IntPtr scManager,
            [MarshalAs(UnmanagedType.LPTStr)] string serviceName,
            [MarshalAs(UnmanagedType.U4)] ServiceAccess desiredAccess
        );

        /// <summary>
        /// Retrieves the optional configuration parameters of the specified service.
        /// </summary>
        /// <param name="hService">A handle to the service.</param>
        /// <param name="buffer">A pointer to the buffer that receives the service configuration information.</param>
        /// <param name="bufferSize">The size of the structure pointed to by the lpBuffer parameter, in bytes.</param>
        /// <param name="bytesNeeded">A pointer to a variable that receives the number of bytes required to store the
        /// configuration information, if the function fails with ERROR_INSUFFICIENT_BUFFER.</param>
        /// <returns>If the function succeeds, the return value is nonzero.</returns>
        /// <seealso cref="https://docs.microsoft.com/en-us/windows/win32/api/winsvc/nf-winsvc-queryserviceconfigw"/>
        [DllImport("advapi32.dll", EntryPoint = "QueryServiceConfigW", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern bool QueryServiceConfig(
            IntPtr hService,
            IntPtr buffer,
            int bufferSize,
            out int bytesNeeded
        );

        /// <summary>
        /// Represents an invalid device/file handle.
        /// </summary>
        private static readonly IntPtr InvalidHandleValue = new IntPtr(-1);
    }
}
