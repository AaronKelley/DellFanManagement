using DellFanManagement.Interop;
using System;
using System.Text;

namespace SmbiosTest
{
    class SmbiosTest
    {
        /// <summary>
        /// Run a process to check the SM BIOS interface.
        /// </summary>
        static void Main()
        {
            try
            {
                DellFanLib.Initialize();

                ulong result;

                result = DellFanLib.ExecuteCommand(SmbiosCommand.GetFanRpm, 0);
                Console.WriteLine(result);
            }
            catch (Exception exception)
            {
                Console.Error.WriteLine("{0}: {1}\n{2}", exception.GetType(), exception.Message, exception.StackTrace);
            }
        }

        /// <summary>
        /// Convert a ulong to a string, assuming that it is ASCII-encoded.
        /// </summary>
        /// <param name="value">ulong value to convert.</param>
        /// <returns>ASCII string respresentation.</returns>
        private static string UlongToString(ulong value)
        {
            return Encoding.ASCII.GetString(BitConverter.GetBytes(value));
        }
    }
}
