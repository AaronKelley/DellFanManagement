using System.Runtime.InteropServices;

namespace DellFanManagement.SmmIo
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct DellSmmBiosMessage
    {
        public ushort Class;
        public ushort Selector;
        public uint Parameter1;
        public uint Parameter2;
        public uint Parameter3;
        public uint Parameter4;
        public uint Result1;
        public uint Result2;
        public uint Result3;
        public uint Result4;
        public uint ArgumentAttribute;
        public uint BufferLength;
    }
}
