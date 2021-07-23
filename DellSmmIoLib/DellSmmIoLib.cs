using DellFanManagement.SmmIo.DellSmi;
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
                SmiObject message = new SmiObject
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
                    SmiObject message = new SmiObject
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

        public static uint GetToken(Token token)
        {
            SmiObject message = new SmiObject
            {
                Class = ClassToken.TokenRead,
                Selector = SelectToken.Standard,
                Input1 = (uint)token
            };

            ExecuteCommand(ref message);
            Console.WriteLine("{0}\t{1}\t{2}\t{3}", message.Output1, message.Output2, message.Output3, message.Output4);
            return message.Output2;
        }

        public static bool SetToken(Token token, uint value, SelectToken selector = SelectToken.Standard)
        {
            SmiObject message = new SmiObject
            {
                Class = ClassToken.TokenWrite,
                Selector = selector,
                Input1 = (uint)token,
                Input2 = value,
                //Input3 = securityKey
            };

            bool result = ExecuteCommand(ref message);
            Console.WriteLine("{0}\t{1}\t{2}\t{3}", message.Output1, message.Output2, message.Output3, message.Output4);
            return result;
        }

        /// <summary>
        /// Fetch the password encoding format from the BIOS.
        /// </summary>
        /// <param name="which">Which type of password to request the encoding format for.</param>
        /// <returns>Password encoding format, or null on error.</returns>
        public static SmiPasswordFormat? GetPasswordFormat(SmiPassword which)
        {
            PasswordProperties? properties = GetPasswordProperties(which);
            if (properties != null)
            {
                if ((properties?.Characteristics & 1) != 0)
                {
                    return SmiPasswordFormat.Ascii;
                }
                else
                {
                    return SmiPasswordFormat.Scancode;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Fetch password properties from the BIOS.
        /// </summary>
        /// <param name="which">Which type of password to request properties for.</param>
        /// <returns>Password properties structure, or null on error.</returns>
        public static PasswordProperties? GetPasswordProperties(SmiPassword which)
        {
            SmiObject message = new SmiObject
            {
                Class = (ClassToken)which,
                Selector = SelectToken.PasswordProperties
            };

            if (ExecuteCommand(ref message) && message.Output1 == 0)
            {
                return new PasswordProperties
                {
                    Installed = Utility.GetByte(0, message.Output2),
                    MaximumLength = Utility.GetByte(1, message.Output2),
                    MinimumLength = Utility.GetByte(2, message.Output2),
                    Characteristics = Utility.GetByte(3, message.Output2),
                    MinimumAlphabeticCharacters = Utility.GetByte(0, message.Output3),
                    MinimumNumericCharacters = Utility.GetByte(1, message.Output3),
                    MinimumSpecialCharacters = Utility.GetByte(2, message.Output3),
                    MaximumRepeatingCharacters = Utility.GetByte(3, message.Output3)
                };
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Execute a command against the SMM BIOS via the ACPI/WMI interface.
        /// </summary>
        /// <param name="message">SMM BIOS message (a packaged-up command to be executed)</param>
        /// <returns>True if successful, false if not; note that exceptions on error conditions can come back from this method as well</returns>
        public static bool ExecuteCommand(ref SmiObject message)
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
        private static byte[] StructToByteArray(SmiObject message)
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
        private static SmiObject ByteArrayToStruct(byte[] array)
        {
            SmiObject message = new SmiObject();

            int size = Marshal.SizeOf(message);
            IntPtr pointer = Marshal.AllocHGlobal(size);

            Marshal.Copy(array, 0, pointer, size);

            message = (SmiObject)Marshal.PtrToStructure(pointer, message.GetType());
            Marshal.FreeHGlobal(pointer);

            return message;
        }
    }
}
