extern "C"
{
	#define DllExport __declspec(dllexport)

	// Public function headers.

	/// <summary>
	/// Execute an arbitrary command against the SMBIOS.
	/// </summary>
	/// <param name="command">Command identifier.</param>
	/// <param name="argument">Command argument/parameter.</param>
	/// <returns>Command result *ulong-max means failure).</returns>
	DllExport unsigned long DellSmmIo(HANDLE driverHandle, unsigned long command, unsigned long argument);
}
