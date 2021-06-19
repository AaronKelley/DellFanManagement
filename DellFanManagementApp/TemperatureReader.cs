using LibreHardwareMonitor.Hardware;
using NvAPIWrapper.GPU;
using NvAPIWrapper.Native.Exceptions;
using System;
using System.Collections.Generic;

namespace DellFanManagement.App
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
            bool nvidiaGpuPresent = false;

            if (PhysicalGPU.GetPhysicalGPUs().Length > 0)
            {
                nvidiaGpuPresent = true;
            }

            _computer = new Computer
            {
                IsCpuEnabled = true,
                IsGpuEnabled = !nvidiaGpuPresent
            };

            _computer.Open();
        }

        /// <summary>
        /// Read all of the available temperatures into a dictionary.
        /// </summary>
        /// <returns>Dictionary with temperatures, keyed by sensor name</returns>
        public IReadOnlyDictionary<string, int> ReadTemperatures()
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
                            temperatures.Add(sensor.Name, int.Parse(sensor.Value.Value.ToString()));
                        }
                    }
                }
            }

            // NVAPI values
            if (!_computer.IsGpuEnabled)
            {
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
                            throw;
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
