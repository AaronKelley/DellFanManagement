using NvAPIWrapper.GPU;
using NvAPIWrapper.Native.Exceptions;
using System;
using System.Collections.Generic;

namespace DellFanManagement.App.TemperatureReaders
{
    /// <summary>
    /// Handles reading GPU temperatures for NVIDIA GPUs using NVAPI, which has lower overhead than Libre Hardware
    /// Monitor.
    /// </summary>
    class NvidiaGpuTemperatureReader : TemperatureReader
    {
        /// <summary>
        /// Read all of the available temperatures into a dictionary.
        /// </summary>
        /// <returns>Dictionary with temperatures, keyed by sensor name</returns>
        public override IReadOnlyDictionary<string, int> ReadTemperatures()
        {
            Dictionary<string, int> temperatures = new();

            foreach (PhysicalGPU gpu in PhysicalGPU.GetPhysicalGPUs())
            {
                string name = gpu.FullName.Replace(" Laptop GPU", string.Empty);

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
                    else if (exception.Message == "NVAPI_NVIDIA_DEVICE_NOT_FOUND")
                    {
                        // NVIDIA device was present previously, but it disappeared?  (Driver update in progress?)
                        // Silently ignore.
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return temperatures;
        }

        /// <summary>
        /// Check to see if NVAPI can be used to read GPU temperatures.
        /// </summary>
        /// <returns>True if NVAPI is supported, otherwise false.</returns>
        public static bool IsNvapiSupported()
        {
            try
            {
                if (PhysicalGPU.GetPhysicalGPUs().Length > 0)
                {
                    return true;
                }
            }
            catch (Exception)
            {
                // Silently fail.
            }

            return false;
        }
    }
}
