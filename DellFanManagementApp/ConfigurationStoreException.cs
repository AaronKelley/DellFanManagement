using System;

namespace DellFanManagement.App
{
    /// <summary>
    /// This exception type is thrown when an error occurs in the ConfigurationStore.
    /// </summary>
    class ConfigurationStoreException : Exception
    {
        /// <summary>
        /// Constructor; pass the message along to the parent object.
        /// </summary>
        /// <param name="message">A message describing the exception</param>
        public ConfigurationStoreException(string message) : base(message)
        {
            // Nothing to do.
        }
    }
}
