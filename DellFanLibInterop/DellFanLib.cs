using System.Runtime.InteropServices;

namespace DellFanManagement.Interop
{
    public static class DellFanLib
    {
        /// <summary>
        /// Version number for the entire package.
        /// </summary>
        public static readonly string Version = "DEV";

        /// <summary>
        /// Load the EC I/O driver and set things up.
        /// </summary>
        /// <returns>False if the driver failed to load</returns>
        [DllImport(@"DellFanLib.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Initialize();

        /// <summary>
        /// Unload the EC I/O driver.
        /// </summary>
        [DllImport(@"DellFanLib.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Shutdown();

        /// <summary>
        /// Get the speed of a specific fan.
        /// </summary>
        /// <param name="fanIndex">Which fan to get the speed of</param>
        /// <returns>Speed in RPM; uint-max means failure</returns>
        [DllImport(@"DellFanLib.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern ulong GetFanRpm(FanIndex fanIndex);

        /// <summary>
        /// Disable EC fan control.
        /// </summary>
        /// <param name="alternate">If true, attempt alternate method to disable EC fan control</param>
        /// <returns>ulong-max means failure</returns>
        [DllImport(@"DellFanLib.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern ulong DisableEcFanControl(bool alternate = false);

        /// <summary>
        /// Enable EC fan control.
        /// </summary>
        /// <param name="alternate">If ture, attempt alternate method to enable EC fan control</param>
        /// <returns>ulong-max means failure</returns>
        [DllImport(@"DellFanLib.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern ulong EnableEcFanControl(bool alternate = false);

        /// <summary>
        /// Set the fan level of one of the fans.
        /// </summary>
        /// <param name="fanIndex">Which fan to set the level of</param>
        /// <param name="fanLevel">Which fan level to set</param>
        /// <returns>ulong-max means failure</returns>
        [DllImport(@"DellFanLib.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern ulong SetFanLevel(FanIndex fanIndex, FanLevel fanLevel);
    }

    /// <summary>
    /// Used for specifying which fan to query or set the level of.
    /// </summary>
    public enum FanIndex : ulong
    {
        Fan1 = 0,
        Fan2 = 1
    }

    /// <summary>
    /// Used to specify which fan level to set.
    /// </summary>
    public enum FanLevel : ulong
    {
        Level0 = 0,
        Level1 = 1,
        Level2 = 2
    }
}
