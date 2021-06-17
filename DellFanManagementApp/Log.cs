using System;

namespace DellFanManagement.App
{
    static class Log
    {
        /// <summary>
        /// Write a message to the log.
        /// </summary>
        /// <param name="message">Message to write</param>
        public static void Write(string message)
        {
            // TODO: Support Windows event log.
            Console.WriteLine(message);
        }
    }
}
