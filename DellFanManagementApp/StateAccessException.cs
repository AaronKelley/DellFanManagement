using System;

namespace DellFanManagement.App
{
    /// <summary>
    /// This exception type is thrown when an attempt is made to modify the state without first calling WaitOne.
    /// </summary>
    class StateAccessException : Exception
    {
        /// <summary>
        /// Constructor; pass the message along to the parent object.
        /// </summary>
        /// <param name="message">A message describing the exception</param>
        public StateAccessException(string message) : base(message)
        {
            // Nothing to do.
        }
    }
}
