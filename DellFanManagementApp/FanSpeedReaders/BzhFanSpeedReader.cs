using DellFanManagement.DellSmbiozBzhLib;

namespace DellFanManagement.App.FanSpeedReaders
{
    /// <summary>
    /// Read fan speeds using the BZH Dell SMM I/O driver.
    /// </summary>
    public class BzhFanSpeedReader : IFanSpeedReader
    {
        /// <summary>
        /// Get the current system fan speeds.
        /// </summary>
        /// <returns>Object containing speeds for two fans.  (null reading means fan not present, or error
        /// reading.)</returns>
        public FanSpeeds GetFanSpeeds()
        {
            return new FanSpeeds()
            {
                Fan1Rpm = DellSmbiosBzh.GetFanRpm(BzhFanIndex.Fan1),
                Fan2Rpm = DellSmbiosBzh.GetFanRpm(BzhFanIndex.Fan2)
            };
        }
    }
}
