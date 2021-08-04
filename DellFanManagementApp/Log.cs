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
            Console.WriteLine(string.Format("{0}: {1}", DateTime.Now, message));
        }

        /// <summary>
        /// Write details about an exception to the log.
        /// </summary>
        /// <param name="exception">Exception to log</param>
        public static void Write(Exception exception)
        {
            Write(string.Format("{0}: {1}\n{2}", exception.GetType(), exception.Message, exception.StackTrace));
        }
    }
}
