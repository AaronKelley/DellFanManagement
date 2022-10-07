using NvAPIWrapper.GPU;
using NvAPIWrapper.Native.Exceptions;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace DellFanManagement.App.TemperatureReaders
{
    /// <summary>
    /// Handles reading GPU temperatures for NVIDIA GPUs using NVAPI, which has lower overhead than Libre Hardware
    /// Monitor.
    /// </summary>
    class NvidiaGpuTemperatureReader : TemperatureReader
    {
        /// <summary>
        /// Keep track of previously read NVIDIA GPU names in the event that one cannot be pulled.
        /// </summary>
        private static readonly Dictionary<uint, string> gpuNames = new();

        /// <summary>
        /// Read all of the available temperatures into a dictionary.
        /// </summary>
        /// <returns>Dictionary with temperatures, keyed by sensor name</returns>
        public override IReadOnlyDictionary<string, int> ReadTemperatures()
        {
            Dictionary<string, int> temperatures = new();

            string name = null;

            try
            {
                foreach (PhysicalGPU gpu in PhysicalGPU.GetPhysicalGPUs())
                {
                    name = gpuNames.ContainsKey(gpu.GPUId) ? gpuNames[gpu.GPUId] : "Unknown GPU";
                    name = gpu.FullName.Replace("Laptop GPU", string.Empty).Replace("NVIDIA", string.Empty).Trim();
                    gpuNames[gpu.GPUId] = name;

                    foreach (GPUThermalSensor sensor in gpu.ThermalInformation.ThermalSensors)
                    {
                        temperatures.Add(name, sensor.CurrentTemperature);
                    }
                }
            }
            catch (NVIDIAApiException exception)
            {
                if (exception.Message == "NVAPI_GPU_NOT_POWERED")
                {
                    if (name != null)
                    {
                        // GPU is currently powered off.
                        temperatures.Add(name, 0);
                    }
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
