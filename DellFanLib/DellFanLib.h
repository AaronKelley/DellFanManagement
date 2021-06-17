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
	/// Get the speed of a specific fan.
	/// </summary>
	/// <param name="fanIndex">Which fan to get the speed of (0 or 1)</param>
	/// <returns>Speed in RPM; uint-max means failure</returns>
	DllExport unsigned long GetFanRpm(unsigned long fanIndex);

	/// <summary>
	/// Disable EC fan control.
	/// </summary>
	/// <param name="alternate">If true, attempt alternate method to disable EC fan control</param>
	/// <returns>ulong-max means failure</returns>
	DllExport unsigned long DisableEcFanControl(bool alternate = false);

	/// <summary>
	/// Enable EC fan control.
	/// </summary>
	/// <param name="alternate">If ture, attempt alternate method to enable EC fan control</param>
	/// <returns>ulong-max means failure</returns>
	DllExport unsigned long EnableEcFanControl(bool alternate = false);

	/// <summary>
	/// Set the fan level of one of the fans.
	/// </summary>
	/// <param name="fanIndex">Which fan to set the level of</param>
	/// <param name="fanLevel">Which fan level to set (0 = off, 1 = medium, 2 = high)</param>
	/// <returns>ulong-max means failure</returns>
	DllExport unsigned long SetFanLevel(unsigned long fanIndex, unsigned long fanLevel);
}
