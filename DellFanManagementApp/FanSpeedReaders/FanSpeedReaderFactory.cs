using System.Collections.Generic;

namespace DellFanManagement.App.FanSpeedReaders
{
    /// <summary>
    /// Determine system capabilities and select an appropriate fan speed reader object to use.
    /// </summary>
    public static class FanSpeedReaderFactory
    {
        /// <summary>
        /// Selects a fan speed reader and returns it.
        /// </summary>
        /// <returns>Fan speed reader appropriate for the system; null if one cannot be found.</returns>
        public static IFanSpeedReader GetFanSpeedReader()
        {
            // Fan speed reader options (prioritized):
            List<IFanSpeedReader> readers = new()
            {
                new SmiFanSpeedReader(),
                new BzhFanSpeedReader(),
                new WmiFanSpeedReader()
            };

            IFanSpeedReader selectedReader = null;
            int bestFanCount = 0;

            foreach (IFanSpeedReader reader in readers)
            {
                // Get a reading.
                FanSpeeds fanSpeedReading = reader.GetFanSpeeds();
                int fanCount = 0;

                if (fanSpeedReading.Fan1Rpm != null)
                {
                    fanCount++;
                }
                if (fanSpeedReading.Fan2Rpm != null)
                {
                    fanCount++;
                }

                // Decide if we should use this one.
                if (fanCount > bestFanCount)
                {
                    bestFanCount = fanCount;
                    selectedReader = reader;
                }
            }

            Log.Write(string.Format("Selected fan speed reader: {0}", selectedReader?.GetType()));
            return selectedReader;
        }
    }
}
