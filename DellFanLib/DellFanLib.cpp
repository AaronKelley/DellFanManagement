#include <windows.h>
#include <winioctl.h>
#include "DellFanLib.h"

// Other constants.

constexpr auto FILE_DEVICE_BZH_DELL_SMM = 0xB424;
constexpr auto BZH_DELL_SMM_IOCTL_KEY = 0xB42;
#define IOCTL_BZH_DELL_SMM_RWREG CTL_CODE(FILE_DEVICE_BZH_DELL_SMM, BZH_DELL_SMM_IOCTL_KEY, METHOD_BUFFERED, FILE_ANY_ACCESS)

// Structures.

typedef struct {
    unsigned long command;
    unsigned long data;
    unsigned long stat1;
    unsigned long stat2;
} SmmBiosPackage;

// Private function headers.

// State.
SmmBiosPackage smmBiosPackage;


unsigned long _stdcall DellSmmIo(HANDLE driverHandle, unsigned long command, unsigned long argument)
{
    /*
    if (!IsInitialized)
    {
        return ULONG_MAX;
    }
    */

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
