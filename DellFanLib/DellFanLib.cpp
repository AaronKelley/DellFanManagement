#include "DellFanLib.h"
#include <winioctl.h>


// State.
HANDLE driverHandle = INVALID_HANDLE_VALUE;
bool IsInitialized = false;
SmmBiosPackage smmBiosPackage;
wchar_t driverPath[32768];


bool _stdcall Initialize()
{
    bool result;
    driverHandle = CreateFile(
            L"\\\\.\\BZHDELLSMMIO",
            GENERIC_READ | GENERIC_WRITE,
            0,
            NULL,
            OPEN_EXISTING,
            FILE_ATTRIBUTE_NORMAL,
            NULL
        );

    // If the driver is not running, install it
    if (driverHandle == INVALID_HANDLE_VALUE)
    {
        GetDriverPath();
        result = InstallDriver(driverPath, true);
        if (!result)
        {
            return false;
        }

        result = StartDriver();

        if (!result)
        {
            return false;
        }

        driverHandle = CreateFile(
                L"\\\\.\\BZHDELLSMMIO",
                GENERIC_READ | GENERIC_WRITE,
                FILE_SHARE_READ | FILE_SHARE_WRITE,
                NULL,
                OPEN_EXISTING,
                FILE_ATTRIBUTE_NORMAL,
                NULL
            );

        if (driverHandle == INVALID_HANDLE_VALUE)
        {
            return false;
        }
    }
    IsInitialized = true;
    return true;
}

DllExport void _stdcall Shutdown()
{
    if (driverHandle != INVALID_HANDLE_VALUE)
    {
        CloseHandle(driverHandle);
    }
    RemoveDriver();
    IsInitialized = false;
}

unsigned long GetFanRpm(unsigned long fanIndex)
{
    return DellSmmIo(DELL_SMM_IO_GET_FAN_RPM, fanIndex);
}

unsigned long DisableEcFanControl(bool alternate)
{
    return DellSmmIo(alternate ? DELL_SMM_IO_DISABLE_FAN_CTL2 : DELL_SMM_IO_DISABLE_FAN_CTL1, DELL_SMM_IO_NO_ARG);
}

unsigned long EnableEcFanControl(bool alternate)
{
    return DellSmmIo(alternate ? DELL_SMM_IO_ENABLE_FAN_CTL2 : DELL_SMM_IO_ENABLE_FAN_CTL1, DELL_SMM_IO_NO_ARG);
}

unsigned long SetFanLevel(unsigned long fanIndex, unsigned long fanLevel)
{
    ULONG argument = (fanLevel << 8) | fanIndex;
    return DellSmmIo(DELL_SMM_IO_SET_FAN_LV, argument);
}

bool GetDriverPath()
{
    PWSTR slash;

    if (!GetModuleFileName(GetModuleHandle(NULL), driverPath, sizeof(driverPath)))
    {
        return false;
    }

    slash = wcsrchr(driverPath, '\\');

    if (slash)
    {
        slash[1] = 0;
    }
    else
    {
        return false;
    }

    wcscat_s(driverPath, L"bzh_dell_smm_io_x64.sys");

    return true;
}

bool _stdcall InstallDriver(PWSTR pszDriverPath, bool IsDemandLoaded)
{
    SC_HANDLE serviceManagerHandle;
    SC_HANDLE serviceHandle;

    // Remove any previous instance of the driver
    RemoveDriver();

    serviceManagerHandle = OpenSCManager(NULL, NULL, SC_MANAGER_ALL_ACCESS);

    if (serviceManagerHandle)
    {
        // Install the driver

        serviceHandle = CreateService(
                serviceManagerHandle,
                L"BZHDELLSMMIO",
                L"BZHDELLSMMIO",
                SERVICE_ALL_ACCESS,
                SERVICE_KERNEL_DRIVER,
                (IsDemandLoaded == true) ? SERVICE_DEMAND_START : SERVICE_SYSTEM_START,
                SERVICE_ERROR_NORMAL,
                pszDriverPath,
                NULL,
                NULL,
                NULL,
                NULL,
                NULL
            );

        CloseServiceHandle(serviceManagerHandle);

        if (serviceHandle == NULL)
            return false;
    }
    else
        return false;

    CloseServiceHandle(serviceHandle);

    return true;
}

bool _stdcall RemoveDriver()
{
    SC_HANDLE serviceManagerHandle;
    SC_HANDLE serviceHandle;
    LPQUERY_SERVICE_CONFIG serviceConfiguration;
    DWORD bytesNeeded;
    DWORD bufferSize;
    bool result;

    StopDriver();

    serviceManagerHandle = OpenSCManager(NULL, NULL, SC_MANAGER_ALL_ACCESS);

    if (!serviceManagerHandle)
    {
        return false;
    }

    serviceHandle = OpenService(serviceManagerHandle, L"BZHDELLSMMIO", SERVICE_ALL_ACCESS);
    CloseServiceHandle(serviceManagerHandle);

    if (!serviceHandle)
    {
        return false;
    }

    result = QueryServiceConfig(serviceHandle, NULL, 0, &bytesNeeded);

    if (GetLastError() == ERROR_INSUFFICIENT_BUFFER)
    {
        bufferSize = bytesNeeded;
        serviceConfiguration = (LPQUERY_SERVICE_CONFIG)malloc(bufferSize);
        result = QueryServiceConfig(serviceHandle, serviceConfiguration, bufferSize, &bytesNeeded);

        if (!result)
        {
            free(serviceConfiguration);
            CloseServiceHandle(serviceHandle);
            return result;
        }

        // If service is set to load automatically, don't delete it!
        if (serviceConfiguration->dwStartType == SERVICE_DEMAND_START)
        {
            result = DeleteService(serviceHandle);
        }
    }

    CloseServiceHandle(serviceHandle);

    return result;
}

bool _stdcall StartDriver()
{
    SC_HANDLE serviceManagerHandle;
    SC_HANDLE serviceHandle;
    bool result;

    serviceManagerHandle = OpenSCManager(NULL, NULL, SC_MANAGER_ALL_ACCESS);

    if (serviceManagerHandle)
    {
        serviceHandle = OpenService(serviceManagerHandle, L"BZHDELLSMMIO", SERVICE_ALL_ACCESS);

        CloseServiceHandle(serviceManagerHandle);

        if (serviceHandle)
        {
            result = StartService(serviceHandle, 0, NULL) || GetLastError() == ERROR_SERVICE_ALREADY_RUNNING;

            CloseServiceHandle(serviceHandle);
        }
        else
        {
            return false;
        }
    }
    else
    {
        return false;
    }

    return result;
}

bool _stdcall StopDriver()
{
    SC_HANDLE serviceManagerHandle;
    SC_HANDLE serviceHandle;
    SERVICE_STATUS serviceStatus;
    bool result;

    serviceManagerHandle = OpenSCManager(NULL, NULL, SC_MANAGER_ALL_ACCESS);

    if (serviceManagerHandle)
    {
        serviceHandle = OpenService(serviceManagerHandle, L"BZHDELLSMMIO", SERVICE_ALL_ACCESS);

        CloseServiceHandle(serviceManagerHandle);

        if (serviceHandle)
        {
            result = ControlService(serviceHandle, SERVICE_CONTROL_STOP, &serviceStatus);

            CloseServiceHandle(serviceHandle);
        }
        else
            return false;
    }
    else
        return false;

    return result;
}

unsigned long _stdcall DellSmmIo(unsigned long command, unsigned long argument)
{
    if (!IsInitialized)
    {
        return ULONG_MAX;
    }

    smmBiosPackage.command = command;
    smmBiosPackage.data = argument;
    smmBiosPackage.stat1 = 0;
    smmBiosPackage.stat2 = 0;

    unsigned long resultSize = 0;

    bool status = DeviceIoControl(
            driverHandle,
            IOCTL_BZH_DELL_SMM_RWREG,
            &smmBiosPackage,
            sizeof(SmmBiosPackage),
            &smmBiosPackage,
            sizeof(SmmBiosPackage),
            &resultSize,
            NULL
        );

    if (status == false)
    {
        return ULONG_MAX;
    }
    else
    {
        return smmBiosPackage.command;
    }
}
