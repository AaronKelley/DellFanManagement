using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DellFanManagement.SmmIo
{
    /// <summary>
    /// General helper methods.
    /// </summary>
    static class Utility
    {
        /// <summary>
        /// Extract a byte value from a uint.
        /// </summary>
        /// <param name="index">Which byte to grab.</param>
        /// <param name="from">uint value to grab the byte from.</param>
        /// <returns>Extracted byte.</returns>
        public static byte GetByte(int index, uint from)
        {
            return (byte)((from & (0xFF << (index * 8))) >> (index * 8));
        }
    }
}
