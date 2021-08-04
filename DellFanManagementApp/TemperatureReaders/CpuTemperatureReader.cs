using LibreHardwareMonitor.Hardware;

namespace DellFanManagement.App.TemperatureReaders
{
    /// <summary>
    /// Handles reading system CPU temperatures.
    /// </summary>
    class CpuTemperatureReader : LibreHardwareMonitorTemperatureReader
    {
        /// <summary>
        /// Constructor.  Initialize the computer object for reading the CPU temperature.
        /// </summary>
        public CpuTemperatureReader()
        {
            _computer = new Computer
            {
                IsCpuEnabled = true
            };

            _computer.Open();
        }
    }
}
