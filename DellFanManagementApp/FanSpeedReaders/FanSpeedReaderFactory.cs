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
            // Two options.
            IFanSpeedReader bzhReader = new BzhFanSpeedReader();
            IFanSpeedReader wmiReader = new WmiFanSpeedReader();

            // Get a reading from each.
            FanSpeeds bzhReading = bzhReader.GetFanSpeeds();
            FanSpeeds wmiReading = wmiReader.GetFanSpeeds();

            // If both have a value for fan 2, prefer BZH.
            if (bzhReading.Fan2Rpm != null && wmiReading.Fan2Rpm != null)
            {
                Log.Write("Using BZH fan speed reader.");
                return bzhReader;
            }

            // If WMI has a value for fan 2 but BZH does not, prefer WMI.
            if (bzhReading.Fan2Rpm == null && wmiReading.Fan2Rpm != null)
            {
                Log.Write("Using WMI fan speed reader.");
                return wmiReader;
            }

            // If both have a reading for fan 1, prefer BZH.
            if (bzhReading.Fan1Rpm != null && wmiReading.Fan1Rpm != null)
            {
                Log.Write("Using BZH fan speed reader.");
                return bzhReader;
            }

            // If WMI has a value for fan 1 but BZH does not, prefer WMI.
            if (bzhReading.Fan1Rpm == null && wmiReading.Fan1Rpm != null)
            {
                Log.Write("Using WMI fan speed reader.");
                return wmiReader;
            }

            // If BZH has a value for fan 1 but WMI does not, prefer BZH.
            if (bzhReading.Fan1Rpm != null && wmiReading.Fan1Rpm == null)
            {
                Log.Write("Using BZH fan speed reader.");
                return bzhReader;
            }

            // No adequate reader.
            Log.Write("Couldn't find a fan speed reader to use.");
            return null;
        }
    }
}
