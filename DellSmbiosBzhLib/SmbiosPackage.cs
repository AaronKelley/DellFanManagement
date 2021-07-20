using System.Runtime.InteropServices;

namespace DellFanManagement.DellSmbiozBzhLib
{
    /// <summary>
    /// A packaged structure used to communicate with the SMBIOS.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SmbiosPackage
    {
        /// <summary>
        /// Identifier of the command to execute.
        /// </summary>
        public uint Command;

        /// <summary>
        /// An optional parameter for the command.
        /// </summary>
        public uint Data;

        /// <summary>
        /// Output field 1.
        /// </summary>
        public uint Output1;

        /// <summary>
        /// Output field 2.
        /// </summary>
        public uint Output2;
    }
}
