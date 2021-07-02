using System.Collections.Generic;

namespace DellFanManagement.App.TemperatureReaders
{
    /// <summary>
    /// Handles reading the CPU core temperatures.
    /// </summary>
    /// <seealso cref="https://stackoverflow.com/questions/1195112/how-to-get-cpu-temperature"/>
    abstract class TemperatureReader
    {
        /// <summary>
        /// Read all of the available temperatures into a dictionary.
        /// </summary>
        /// <returns>Dictionary with temperatures, keyed by sensor name</returns>
        public abstract IReadOnlyDictionary<string, int> ReadTemperatures();
    }
}
