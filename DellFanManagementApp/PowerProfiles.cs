using System;
using System.Runtime.InteropServices;

namespace DellFanManagement.App
{
    /// <summary>
    /// Manages interactions with the Windows power profiles system.
    /// </summary>
    /// <seealso cref="https://github.com/andy722/power-plan-switcher/blob/master/PlanSwitcher/PowerManager.cs"/>
    public static class PowerProfiles
    {
        /// <summary>
        /// Get the GUID for the currently active power profile.
        /// </summary>
        /// <returns>GUID for the currently active power profile (NULL if failure).</returns>
        public static Guid? GetActivePowerProfile()
        {
            Guid? activeProfile = null;

            if (PowerGetActiveScheme(IntPtr.Zero, out IntPtr pointer) == 0)
            {
                activeProfile = Marshal.PtrToStructure<Guid>(pointer);
                Marshal.FreeHGlobal(pointer);
            }

            return activeProfile;
        }

        /// <summary>
        /// Retrieves the active power scheme and returns a GUID that identifies the scheme.
        /// </summary>
        /// <param name="UserPowerKey">This parameter is reserved for future use and must be set to NULL.</param>
        /// <param name="ActivePolicyGuid">A pointer that receives a pointer to a GUID structure. Use the LocalFree
        /// function to free this memory.</param>
        /// <returns>Returns ERROR_SUCCESS (zero) if the call was successful, and a nonzero value if the call
        /// failed.</returns>
        /// <see cref="https://docs.microsoft.com/en-us/windows/win32/api/powersetting/nf-powersetting-powergetactivescheme"/>
        [DllImportAttribute("powrprof.dll", EntryPoint = "PowerGetActiveScheme")]
        private static extern uint PowerGetActiveScheme(IntPtr UserPowerKey, out IntPtr ActivePolicyGuid);
    }
}
