using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace DellFanManagement.App
{
    /// <summary>
    /// Used to detect if the process has administrative/elevated priviliges; borrowed from StackOverflow.
    /// </summary>
    /// <see cref="https://stackoverflow.com/a/17492949/4647297"/>
    public static class UacHelper
    {
        private const string uacRegistryKey = "Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System";
        private const string uacRegistryValue = "EnableLUA";

        private static readonly uint STANDARD_RIGHTS_READ = 0x00020000;
        private static readonly uint TOKEN_QUERY = 0x0008;
        private static readonly uint TOKEN_READ = (STANDARD_RIGHTS_READ | TOKEN_QUERY);

        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool OpenProcessToken(IntPtr ProcessHandle, UInt32 DesiredAccess, out IntPtr TokenHandle);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool CloseHandle(IntPtr hObject);

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool GetTokenInformation(IntPtr TokenHandle, TokenInformationClass TokenInformationClass, IntPtr TokenInformation, uint TokenInformationLength, out uint ReturnLength);

        private enum TokenInformationClass
        {
            TokenUser = 1,
            TokenGroups,
            TokenPrivileges,
            TokenOwner,
            TokenPrimaryGroup,
            TokenDefaultDacl,
            TokenSource,
            TokenType,
            TokenImpersonationLevel,
            TokenStatistics,
            TokenRestrictedSids,
            TokenSessionId,
            TokenGroupsAndPrivileges,
            TokenSessionReference,
            TokenSandBoxInert,
            TokenAuditPolicy,
            TokenOrigin,
            TokenElevationType,
            TokenLinkedToken,
            TokenElevation,
            TokenHasRestrictions,
            TokenAccessInformation,
            TokenVirtualizationAllowed,
            TokenVirtualizationEnabled,
            TokenIntegrityLevel,
            TokenUIAccess,
            TokenMandatoryPolicy,
            TokenLogonSid,
            MaxTokenInfoClass
        }

        private enum TokenElevationType
        {
            TokenElevationTypeDefault = 1,
            TokenElevationTypeFull,
            TokenElevationTypeLimited
        }

        private static bool IsUacEnabled()
        {
            using RegistryKey uacKey = Registry.LocalMachine.OpenSubKey(uacRegistryKey, false);
            bool result = uacKey.GetValue(uacRegistryValue).Equals(1);
            return result;
        }

        public static bool IsProcessElevated()
        {
            if (IsUacEnabled())
            {
                if (!OpenProcessToken(Process.GetCurrentProcess().Handle, TOKEN_READ, out IntPtr tokenHandle))
                {
                    throw new ApplicationException("Could not get process token.  Win32 Error Code: " + Marshal.GetLastWin32Error());
                }

                try
                {
                    TokenElevationType elevationResult = TokenElevationType.TokenElevationTypeDefault;

                    int elevationResultSize = Marshal.SizeOf(Enum.GetUnderlyingType(elevationResult.GetType()));
                    uint returnedSize = 0;

                    IntPtr elevationTypePtr = Marshal.AllocHGlobal(elevationResultSize);
                    try
                    {
                        bool success = GetTokenInformation(tokenHandle, TokenInformationClass.TokenElevationType, elevationTypePtr, (uint)elevationResultSize, out returnedSize);
                        if (success)
                        {
                            elevationResult = (TokenElevationType)Marshal.ReadInt32(elevationTypePtr);
                            bool isProcessAdmin = elevationResult == TokenElevationType.TokenElevationTypeFull;
                            return isProcessAdmin;
                        }
                        else
                        {
                            throw new ApplicationException("Unable to determine the current elevation.");
                        }
                    }
                    finally
                    {
                        if (elevationTypePtr != IntPtr.Zero)
                        {
                            Marshal.FreeHGlobal(elevationTypePtr);
                        }
                    }
                }
                finally
                {
                    if (tokenHandle != IntPtr.Zero)
                    {
                        CloseHandle(tokenHandle);
                    }
                }
            }
            else
            {
                WindowsIdentity identity = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new(identity);
                bool result = principal.IsInRole(WindowsBuiltInRole.Administrator) || principal.IsInRole(0x200); // 0x200 = Domain Administrator
                return result;
            }
        }
    }
}