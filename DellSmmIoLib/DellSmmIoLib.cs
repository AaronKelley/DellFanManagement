using System;
using System.Management;
using System.Runtime.InteropServices;

namespace DellFanManagement.SmmIo
{
    /// <summary>
    /// Handles issuing commands to the SMM BIOS via the ACPI/WMI interface.
    /// </summary>
    public static class DellSmmIoLib
    {
        private static readonly string WmiScopeRoot = "root/wmi";
        private static readonly string WmiClassNameBdat = "BDat";
        private static readonly string WmiClassNameBfn = "BFn";
        private static readonly string WmiBfnMethodDobfn = "DoBFn";
        private static readonly string AcpiManagementInterfaceHardwareId = @"ACPI\PNP0C14\0_0";

        private static readonly int MinimumBufferLength = 36;
        private static readonly int BufferLength = 32768;

        /// <summary>
        /// Get the current thermal setting for the system.
        /// </summary>
        /// <returns>Current thermal setting; ThermalSetting.Error on error</returns>
        public static ThermalSetting GetThermalSetting()
        {
            try
            {
                DellSmmBiosMessage message = new DellSmmBiosMessage
                {
                    Class = ClassToken.Info,
                    Selector = SelectToken.ThermalMode
                };

                bool result = ExecuteCommand(ref message);
                if (result)
                {
                    return (ThermalSetting)message.Output3;
                }
                else
                {
                    return ThermalSetting.Error;
                }
            }
            catch (Exception)
            {
                return ThermalSetting.Error;
            }
        }

        /// <summary>
        /// Apply a new thermal setting to the system.
        /// </summary>
        /// <param name="thermalSetting">Thermal setting to apply</param>
        /// <returns>True if successful, false if not</returns>
        public static bool SetThermalSetting(ThermalSetting thermalSetting)
        {
            if (thermalSetting != ThermalSetting.Error)
            {
                try
                {
                    DellSmmBiosMessage message = new DellSmmBiosMessage
                    {
                        Class = ClassToken.Info,
                        Selector = SelectToken.ThermalMode,
                        Input1 = 1,
                        Input2 = (uint)thermalSetting
                    };

                    return ExecuteCommand(ref message);
                }
                catch (Exception)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static bool SetToken(Token token)
        {
            DellSmmBiosMessage message = new DellSmmBiosMessage
            {
                Class = ClassToken.TokenWrite,
                Selector = SelectToken.Standard
                // Input1 = Token location ???
                // Input2 = Token value
            };

            return ExecuteCommand(ref message);
        }

        /// <summary>
        /// Execute a command against the SMM BIOS via the ACPI/WMI interface.
        /// </summary>
        /// <param name="message">SMM BIOS message (a packaged-up command to be executed)</param>
        /// <returns>True if successful, false if not; note that exceptions on error conditions can come back from this method as well</returns>
        public static bool ExecuteCommand(ref DellSmmBiosMessage message)
        {
            byte[] bytes = StructToByteArray(message);
            byte[] buffer = new byte[BufferLength];
            Buffer.BlockCopy(bytes, 0, buffer, 0, bytes.Length);
            bool result = ExecuteCommand(ref buffer);
            message = ByteArrayToStruct(buffer);

            return result;
        }

        /// <summary>
        /// Execute a command against the SMM BIOS via the ACPI/WMI interface.
        /// </summary>
        /// <param name="buffer">Byte array buffer containing the command to be executed; results from the command will be filled into this array.</param>
        /// <returns>True if successful, false if not; note that exceptions on error conditions can come back from this method as well</returns>
        private static bool ExecuteCommand(ref byte[] buffer)
        {
            bool result = false;

            if (buffer.Length < MinimumBufferLength)
            {
                throw new Exception(string.Format("Buffer length is less than the minimum {0} bytes", MinimumBufferLength));
            }

            ManagementBaseObject instance = new ManagementClass(WmiScopeRoot, WmiClassNameBdat, null).CreateInstance();
            instance["Bytes"] = buffer;

            ManagementObjectSearcher searcher = new ManagementObjectSearcher(new ManagementScope(WmiScopeRoot), new SelectQuery(WmiClassNameBfn))
            {
                Options = new EnumerationOptions()
                {
                    EnsureLocatable = true
                }
            };

            foreach (ManagementObject managementObject in searcher.Get())
            {
                if (managementObject["InstanceName"].ToString().ToUpper().Equals(AcpiManagementInterfaceHardwareId))
                {
                    ManagementBaseObject methodParameters = managementObject.GetMethodParameters(WmiBfnMethodDobfn);
                    methodParameters["Data"] = instance;
                    ManagementBaseObject managementBaseObject = (ManagementBaseObject)managementObject.InvokeMethod(WmiBfnMethodDobfn, methodParameters, null).Properties["Data"].Value;
                    buffer = (byte[])managementBaseObject["Bytes"];
                    result = true;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Convert a SMM BIOS message struct to a byte array.
        /// </summary>
        /// <param name="message">SMM BIOS message struct</param>
        /// <returns>Byte array</returns>
        /// <seealso cref="https://stackoverflow.com/questions/3278827/how-to-convert-a-structure-to-a-byte-array-in-c"/>
        private static byte[] StructToByteArray(DellSmmBiosMessage message)
        {
            int size = Marshal.SizeOf(message);
            byte[] array = new byte[size];

            IntPtr pointer = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(message, pointer, true);
            Marshal.Copy(pointer, array, 0, size);
            Marshal.FreeHGlobal(pointer);
            return array;
        }

        /// <summary>
        /// Convert a byte array to a SMM BIOS message struct.
        /// </summary>
        /// <param name="array">Byte array</param>
        /// <returns>SMM BIOS message struct</returns>
        /// <seealso cref="https://stackoverflow.com/questions/3278827/how-to-convert-a-structure-to-a-byte-array-in-c"/>
        private static DellSmmBiosMessage ByteArrayToStruct(byte[] array)
        {
            DellSmmBiosMessage message = new DellSmmBiosMessage();

            int size = Marshal.SizeOf(message);
            IntPtr pointer = Marshal.AllocHGlobal(size);

            Marshal.Copy(array, 0, pointer, size);

            message = (DellSmmBiosMessage)Marshal.PtrToStructure(pointer, message.GetType());
            Marshal.FreeHGlobal(pointer);

            return message;
        }
    }
}
