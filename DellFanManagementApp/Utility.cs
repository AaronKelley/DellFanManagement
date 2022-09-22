using IrrKlang;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

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

        /// <summary>
        /// Attempt to set the NVIDIA GPU P-state.
        /// </summary>
        /// <param name="inspectorPath">Path to NVIDIA Inspector, or an application that can manipulate the NVIDIA GPU
        /// P-state.</param>
        /// <param name="pState">P-state to set.  (16 = automatic)</param>
        /// <returns>True if the attempt to invoke NVIDIA Inspector was successful.</returns>
        public static bool SetNvidiaGpuPstate(string inspectorPath, int pState)
        {
            Process process = new();
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = false;
            process.StartInfo.FileName = inspectorPath;
            process.StartInfo.Arguments = string.Format("-forcepstate:0,{0}", pState);
            return process.Start();
        }

        /// <summary>
        /// Set the current power mode.
        /// </summary>
        /// <param name="OverlaySchemeGuid">Power mode to set.</param>
        /// <returns>Zero on success, non-zero on failure.</returns>
        [DllImportAttribute("powrprof.dll", EntryPoint = "PowerSetActiveOverlayScheme")]
        public static extern uint PowerSetActiveOverlayScheme(Guid OverlaySchemeGuid);
    }
}
