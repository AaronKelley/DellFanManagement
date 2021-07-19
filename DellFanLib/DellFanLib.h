extern "C"
{
	#define DllExport __declspec(dllexport)

	// Public function headers.

	/// <summary>
	/// Load the EC I/O driver and set things up.
	/// </summary>
	/// <returns>False if the driver failed to load</returns>
	DllExport bool _stdcall Initialize();

	/// <summary>
	/// Unload the EC I/O driver.
	/// </summary>
	DllExport void _stdcall Shutdown();

	/// <summary>
	/// Execute an arbitrary command against the SMBIOS.
	/// </summary>
	/// <param name="command">Command identifier.</param>
	/// <param name="argument">Command argument/parameter.</param>
	/// <returns>Command result *ulong-max means failure).</returns>
	DllExport unsigned long DellSmmIo(unsigned long command, unsigned long argument);
}
