extern "C"
{
	#include <windows.h>

	#define DllExport __declspec(dllexport)

	// Constants for controls.

	constexpr auto DELL_SMM_IO_DISABLE_FAN_CTL1 = 0x30A3;
	constexpr auto DELL_SMM_IO_ENABLE_FAN_CTL1 = 0x31A3;
	constexpr auto DELL_SMM_IO_DISABLE_FAN_CTL2 = 0x34A3;
	constexpr auto DELL_SMM_IO_ENABLE_FAN_CTL2 = 0x35A3;

	constexpr auto DELL_SMM_IO_SET_FAN_LV = 0x01A3;
	constexpr auto DELL_SMM_IO_GET_FAN_RPM = 0x02A3;

	constexpr auto DELL_SMM_IO_FAN_LV0 = 0;
	constexpr auto DELL_SMM_IO_FAN_LV1 = 1;
	constexpr auto DELL_SMM_IO_FAN_LV2 = 2;

	constexpr auto DELL_SMM_IO_FAN1 = 0;
	constexpr auto DELL_SMM_IO_FAN2 = 1;

	constexpr auto DELL_SMM_IO_NO_ARG = 0;

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

	// Public function headers.
	#include "DellFanLib-Public.h"

	// Private function headers.

	bool GetDriverPath();
	bool _stdcall InstallDriver(PWSTR pszDriverPath, bool IsDemandLoaded);
	bool _stdcall RemoveDriver();
	bool _stdcall StartDriver();
	bool _stdcall StopDriver();
	unsigned long _stdcall DellSmmIo(unsigned long command, unsigned long argument);
}
