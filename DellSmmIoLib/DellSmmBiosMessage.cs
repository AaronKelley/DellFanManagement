using System.Runtime.InteropServices;

namespace DellFanManagement.SmmIo
{
    /// <summary>
    /// Structure used to interact with the BIOS.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct DellSmmBiosMessage
    {
        public ClassToken Class;

        public SelectToken Selector;

        /// <summary>
        /// First input parameter.
        /// </summary>
        public uint Input1;

        /// <summary>
        /// Second input parameter.
        /// </summary>
        public uint Input2;

        /// <summary>
        /// Third input parameter.
        /// </summary>
        public uint Input3;

        /// <summary>
        /// Fourth input parameter.
        /// </summary>
        public uint Input4;

        /// <summary>
        /// First output value.
        /// </summary>
        public uint Output1;

        /// <summary>
        /// Second output value.
        /// </summary>
        public uint Output2;

        /// <summary>
        /// Third output value.
        /// </summary>
        public uint Output3;

        /// <summary>
        /// Fourth output value.
        /// </summary>
        public uint Output4;
    }
}
