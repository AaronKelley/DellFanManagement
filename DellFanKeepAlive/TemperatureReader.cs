using NvAPIWrapper.GPU;
using NvAPIWrapper.Native.Exceptions;
using OpenHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;

namespace DellFanManagement.KeepAlive
{
    /// <summary>
    /// Handles reading the CPU core temperatures.
    /// </summary>
    /// <seealso cref="https://stackoverflow.com/questions/1195112/how-to-get-cpu-temperature"/>
    class TemperatureReader : IDisposable
    {
        private readonly Computer _computer;

        /// <summary>
        /// Constructor.  Initialize the computer object for reading the CPU temperature.
        /// </summary>
        public TemperatureReader()
        {
            _computer = new Computer
            {
                CPUEnabled = true,
                //GPUEnabled = true
            };

            _computer.Open();
        }

        /// <summary>
        /// Read all of the available temperatures into a dictionary.
        /// </summary>
        /// <returns>Dictionary with temperatures, keyed by sensor name</returns>
        public IReadOnlyDictionary<string, int> GetCpuCoreTemperatures()
        {
            Dictionary<string, int> temperatures = new Dictionary<string, int>();

            // OpenHardwareMonitor values
            foreach (IHardware hardware in _computer.Hardware)
            {
                hardware.Update();

                foreach (ISensor sensor in hardware.Sensors)
                {
                    if (sensor.SensorType == SensorType.Temperature && sensor.Value.HasValue)
                    {
                        temperatures.Add(sensor.Name, int.Parse(sensor.Value.Value.ToString()));
                    }
                }
            }

            // NVAPI values
            foreach (PhysicalGPU gpu in PhysicalGPU.GetPhysicalGPUs())
            {
                string name = gpu.FullName;

                try
                {
                    foreach (GPUThermalSensor sensor in gpu.ThermalInformation.ThermalSensors)
                    {
                        temperatures.Add(name, sensor.CurrentTemperature);
                    }
                }
                catch (NVIDIAApiException exception)
                {
                    if (exception.Message == "NVAPI_GPU_NOT_POWERED")
                    {
                        // GPU is currently powered off.
                        temperatures.Add(name, 0);
                    }
                    else
                    {
                        throw exception;
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
