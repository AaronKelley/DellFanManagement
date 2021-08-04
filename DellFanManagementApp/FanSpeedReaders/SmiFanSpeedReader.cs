using DellFanManagement.DellSmbiosSmiLib;
using DellFanManagement.DellSmbiosSmiLib.DellSmi;
using System;

namespace DellFanManagement.App.FanSpeedReaders
{
    /// <summary>
    /// Read fan speeds using the Dell WMI/SMI interface.
    /// </summary>
    public class SmiFanSpeedReader : IFanSpeedReader
    {
        /// <summary>
        /// Get the current system fan speeds.
        /// </summary>
        /// <returns>Object containing speeds for two fans.  (null reading means fan not present, or error
        /// reading.)</returns>
        public FanSpeeds GetFanSpeeds()
        {
            FanSpeeds fanSpeedReading = new();

            try
            {
                fanSpeedReading.Fan1Rpm = DellSmbiosSmi.GetTokenCurrentValue(Token.Fan1Rpm);
                fanSpeedReading.Fan2Rpm = DellSmbiosSmi.GetTokenCurrentValue(Token.Fan2Rpm);
            }
            catch (Exception)
            {
                // Take no action.
            }

            return fanSpeedReading;
        }
    }
}
