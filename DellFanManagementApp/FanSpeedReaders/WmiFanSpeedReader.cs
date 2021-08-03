using System;
using System.Management;

namespace DellFanManagement.App.FanSpeedReaders
{
    class WmiFanSpeedReader : IFanSpeedReader
    {
        private static readonly ManagementScope Scope = new("root/dcim/sysman");
        private readonly ManagementObjectSearcher FanSensor1Searcher;
        private readonly ManagementObjectSearcher FanSensor2Searcher;

        public WmiFanSpeedReader()
        {
            FanSensor1Searcher = null;
            FanSensor2Searcher = null;

            ManagementObjectSearcher searcher = new(Scope, new SelectQuery("DCIM_NumericSensor"))
            {
                Options = new EnumerationOptions()
                {
                    EnsureLocatable = true
                }
            };

            try
            {
                foreach (ManagementObject managementObject in searcher.Get())
                {
                    if (managementObject.GetPropertyValue("ElementName").ToString().ToLower().StartsWith("fan"))
                    {
                        // Found a fan sensor.
                        if (FanSensor1Searcher == null)
                        {
                            Log.Write("Found WMI fan sensor 1");
                            FanSensor1Searcher = new(Scope, new SelectQuery(string.Format("Select * FROM DCIM_NumericSensor WHERE DeviceID = '{0}'", managementObject.GetPropertyValue("DeviceID"))));
                        }
                        else
                        {
                            Log.Write("Found WMI fan sensor 2");
                            FanSensor2Searcher = new(Scope, new SelectQuery(string.Format("Select * FROM DCIM_NumericSensor WHERE DeviceID = '{0}'", managementObject.GetPropertyValue("DeviceID"))));
                            break;
                        }
                    }
                }
            }
            catch (Exception)
            {
                // Take no action.
            }
        }

        public FanSpeeds GetFanSpeeds()
        {
            uint? rpm1 = null;
            uint? rpm2 = null;

            if (FanSensor1Searcher != null)
            {
                foreach (ManagementObject fanSensor in FanSensor1Searcher.Get())
                {
                    if (uint.TryParse(fanSensor.GetPropertyValue("CurrentReading")?.ToString(), out uint value))
                    {
                        rpm1 = value;
                    }
                    break;
                }
            }

            if (FanSensor2Searcher != null)
            {
                foreach (ManagementObject fanSensor in FanSensor2Searcher.Get())
                {
                    if (uint.TryParse(fanSensor.GetPropertyValue("CurrentReading")?.ToString(), out uint value))
                    {
                        rpm2 = value;
                    }
                    break;
                }
            }

            return new FanSpeeds()
            {
                Fan1Rpm = rpm1,
                Fan2Rpm = rpm2
            };
        }
    }
}
