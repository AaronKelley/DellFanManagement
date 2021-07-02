using LibreHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;

namespace DellFanManagement.App.TemperatureReaders
{
    /// <summary>
    /// Handles reading temperatures from Libre Hardware Monitor.
    /// </summary>
    abstract class LibreHardwareMonitorTemperatureReader : TemperatureReader, IDisposable
    {
        /// <summary>
        /// Libre Hardware Monitor computer object.
        /// </summary>
        protected Computer _computer;

        /// <summary>
        /// Read all of the available temperatures into a dictionary.
        /// </summary>
        /// <returns>Dictionary with temperatures, keyed by sensor name</returns>
        public override IReadOnlyDictionary<string, int> ReadTemperatures()
        {
            Dictionary<string, int> temperatures = new();

            // OpenHardwareMonitor values
            foreach (IHardware hardware in _computer.Hardware)
            {
                hardware.Update();

                foreach (ISensor sensor in hardware.Sensors)
                {
                    if (sensor.SensorType == SensorType.Temperature && sensor.Value.HasValue)
                    {
                        if (!sensor.Name.Contains("TjMax") && !sensor.Name.Contains("Average") && !sensor.Name.Contains("Max"))
                        {
                            int temperature = sensor.Value != null ? (int)Math.Round(sensor.Value.Value) : 0;
                            temperatures.Add(sensor.Name, temperature);
                        }
                    }
                }
            }

            return temperatures;
        }

        /// <summary>
        /// Clean up.
        /// </summary>
        public void Dispose()
        {
            try
            {
                _computer.Close();
            }
            catch (Exception)
            {
                // Ignore errors that come out of closing.
            }
        }
    }
}
