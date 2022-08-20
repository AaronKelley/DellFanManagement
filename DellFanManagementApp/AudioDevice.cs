using System.Text.Json;

namespace DellFanManagement.App
{
    /// <summary>
    /// Represents an audio device.
    /// </summary>
    public class AudioDevice
    {
        /// <summary>
        /// Audio device ID.
        /// </summary>
        public readonly string DeviceId;

        /// <summary>
        /// Audio device name.
        /// </summary>
        public readonly string DeviceName;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="deviceId">Audio device ID</param>
        /// <param name="deviceName">Audio device name</param>
        public AudioDevice(string deviceId, string deviceName)
        {
            DeviceId = deviceId;
            DeviceName = deviceName;
        }

        /// <summary>
        /// Returns a string representation of the audio device (just the device name).
        /// </summary>
        /// <returns>Device name</returns>
        public override string ToString()
        {
            return DeviceName;
        }

        /// <summary>
        /// Determine whether or not two AudioDevice objects are identical.
        /// </summary>
        /// <param name="obj">Another AudioDevice object</param>
        /// <returns>True only if the two AudioDevice instances have the same ID and name</returns>
        public override bool Equals(object obj)
        {
            if (obj.GetType() != GetType())
            {
                return false;
            }

            AudioDevice otherDevice = (AudioDevice)obj;
            return otherDevice.DeviceId == DeviceId && otherDevice.DeviceName == DeviceName;
        }

        /// <summary>
        /// Get a hash code for the AudioDevice.
        /// </summary>
        /// <returns>Hash code derived from the device ID and name.</returns>
        public override int GetHashCode()
        {
            return JsonSerializer.Serialize(this).GetHashCode();
        }
    }
}
