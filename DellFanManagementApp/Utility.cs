using IrrKlang;
using System.Collections.Generic;

namespace DellFanManagement.App
{
    /// <summary>
    /// A collection of various methods used in multiple places throughout the application.
    /// </summary>
    static class Utility
    {
        /// <summary>
        /// Get a list of audio devices in the system.
        /// </summary>
        /// <returns>Audio device list</returns>
        public static List<AudioDevice> GetAudioDevices()
        {
            List<AudioDevice> audioDevices = new();

            ISoundDeviceList soundDeviceList = new(SoundDeviceListType.PlaybackDevice);

            for (int index = 0; index < soundDeviceList.DeviceCount; index++)
            {
                string deviceId = soundDeviceList.getDeviceID(index);
                if (deviceId != string.Empty)
                {
                    audioDevices.Add(new AudioDevice(deviceId, soundDeviceList.getDeviceDescription(index)));
                }
            }

            return audioDevices;
        }
    }
}
