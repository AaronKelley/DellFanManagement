using DellFanManagement.App.TemperatureReaders;
using DellFanManagement.Interop;
using DellFanManagement.SmmIo;
using System.Collections.Generic;
using System.Threading;

namespace DellFanManagement.App
{
    /// <summary>
    /// Represents, basically, the current state of the application.  Used for sharing data between the UI and
    /// background threads.
    /// </summary>
    class State
    {
        /// <summary>
        /// Object for reading CPU and GPU temperatures from the system.
        /// </summary>
        private readonly Dictionary<TemperatureComponent, TemperatureReader> _temperatureReaders;

        /// <summary>
        /// Semaphore for protecting access to state changes.
        /// </summary>
        private readonly Semaphore _semaphore;

        /// <summary>
        /// Only allow changes to the state when this value is true.
        /// </summary>
        private bool _changesAllowed;

        /// <summary>
        /// If there is an error message to display, it goes here.
        /// </summary>
        private string _error;

        /// <summary>
        /// Indicates which configuration mode the app is currently running under.
        /// </summary>
        private OperationMode _configuration;

        /// <summary>
        /// Whether or not the "background thread" is running.
        /// </summary>
        private bool _backgroundThreadRunning;

        /// <summary>
        /// Whether or not the "audio keep alive" thread is running.
        /// </summary>
        private bool _audioThreadRunning { get; set; }

        /// <summary>
        /// Whether or not the form hosting the main application has been closed.
        /// </summary>
        private bool _formClosed;

        /// <summary>
        /// Indicates whether or not EC fan control is enabled.
        /// </summary>
        private bool _ecFanControlEnabled { get; set; }

        /// <summary>
        /// Current level manually set for fan 1.
        /// </summary>
        private FanLevel? _fan1Level;

        /// <summary>
        /// Current level manually set for fan 2.
        /// </summary>
        private FanLevel? _fanLevel2;

        /// <summary>
        /// Status of the consistency mode system.
        /// </summary>
        private string _consistencyModeStatus;

        /// <summary>
        /// The currently selected audio device.
        /// </summary>
        private AudioDevice _selectedAudioDevice;

        /// <summary>
        /// If the selected audio device disappears and returns, we want to automatically select it again.
        /// </summary>
        private AudioDevice _bringBackAudioDevice;

        /// <summary>
        /// Number of times in a row that the thermal setting has failed to update.
        /// </summary>
        private int _consecutiveThermalSettingFailures;

        /// <summary>
        /// Number of times before trying to read the thermal setting again (exponential backoff).
        /// </summary>
        private int _thermalSettingReadBackoff;

        /// <summary>
        /// <summary>
        /// Constructor; initialize everything.
        /// </summary>
        public State()
        {
            _backgroundThreadRunning = false;
            _audioThreadRunning = false;
            _formClosed = false;
            _ecFanControlEnabled = true;
            _selectedAudioDevice = null;
            _error = null;

            _semaphore = new(1, 1);
            _changesAllowed = false;

            _consistencyModeStatus = " ";

            _consecutiveThermalSettingFailures = 0;
            _thermalSettingReadBackoff = 0;

            // Initialize temperature readers.
            _temperatureReaders = new();
            _temperatureReaders.Add(TemperatureComponent.CPU, new CpuTemperatureReader());
            if (NvidiaGpuTemperatureReader.IsNvapiSupported())
            {
                // Use NVAPI if it is available.
                _temperatureReaders.Add(TemperatureComponent.GPU, new NvidiaGpuTemperatureReader());
            }
            else
            {
                _temperatureReaders.Add(TemperatureComponent.GPU, new GenericGpuTemperatureReader());
            }

            Temperatures = new();

            Fan2Present = true;

            WaitOne();
            Update();
            Release();
        }

        /// <summary>
        /// Update the state.
        /// </summary>
        /// <param name="reader">A temperature reader</param>
        public void Update()
        {
            AccessCheck();

            UpdateFanRpms();
            UpdateTemperatures();
            UpdateThermalSetting();
            UpdateAudioDevices();
        }

        /// <summary>
        /// Update the fan RPMs.
        /// </summary>
        private void UpdateFanRpms()
        {
            // Update state: RPM.
            Fan1Rpm = DellFanLib.GetFanRpm(FanIndex.Fan1);
            Fan2Rpm = DellFanLib.GetFanRpm(FanIndex.Fan2);

            if (Fan1Rpm != uint.MaxValue && Fan2Rpm == uint.MaxValue)
            {
                Fan2Present = false;
            }
            else if (Fan2Rpm != uint.MaxValue)
            {
                Fan2Present = true;
            }
        }

        /// <summary>
        /// Update "thermal setting".
        /// </summary>
        public void UpdateThermalSetting()
        {
            // If a backoff has been set, decrement it.
            if (_thermalSettingReadBackoff > 0)
            {
                _thermalSettingReadBackoff--;
            }
            else
            {
                ThermalSetting = DellSmmIoLib.GetThermalSetting();

                if (ThermalSetting == ThermalSetting.Error)
                {
                    // Reading this setting on an unsupported system can cause high CPU usage from WMI.
                    // Exponential backoff before trying to read it again.
                    _consecutiveThermalSettingFailures++;
                    _thermalSettingReadBackoff = 1;
                    for (int index = 0; index < _consecutiveThermalSettingFailures; index++)
                    {
                        _thermalSettingReadBackoff *= 4;
                    }
                }
                else
                {
                    _consecutiveThermalSettingFailures = 0;
                }
            }
        }

        /// <summary>
        /// Update temperatures.
        /// </summary>
        /// <param name="reader">A temperature reader</param>
        private void UpdateTemperatures()
        {
            foreach (TemperatureComponent component in _temperatureReaders.Keys)
            {
                Temperatures[component] = _temperatureReaders[component].ReadTemperatures();
            }
        }

        /// <summary>
        /// Update the audio device list.
        /// </summary>
        private void UpdateAudioDevices()
        {
            AudioDevices = Utility.GetAudioDevices();
        }

        /// <summary>
        /// Throw an exception if changes to the state are not presently allowed.
        /// </summary>
        private void AccessCheck()
        {
            if (!_changesAllowed)
            {
                throw new StateAccessException("Changes are not allowed until WaitOne is called.");
            }
        }

        /// <summary>
        /// Request access to make state changes (blocks until other threads are done).
        /// </summary>
        public void WaitOne()
        {
            _ = _semaphore.WaitOne();
            _changesAllowed = true;
        }

        /// <summary>
        /// Indicate that a thread is done making changes to the state.
        /// </summary>
        public void Release()
        {
            _changesAllowed = false;
            _semaphore.Release();
        }

        // ===========================================
        // Limited public propery declarations follow:
        // ===========================================

        /// <summary>
        /// If there is an error message to display, it goes here.
        /// </summary>
        public string Error
        {
            get { return _error; }
            set { AccessCheck(); _error = value; }
        }

        /// <summary>
        /// Indicates which configuration mode the app is currently running under.
        /// </summary>
        public OperationMode OperationMode
        {
            get { return _configuration; }
            set { AccessCheck(); _configuration = value; }
        }

        /// <summary>
        /// Whether or not the "background thread" is running.
        /// </summary>
        public bool BackgroundThreadRunning
        {
            get { return _backgroundThreadRunning; }
            set { AccessCheck(); _backgroundThreadRunning = value; }
        }

        /// <summary>
        /// Whether or not the "audio keep alive" thread is running.
        /// </summary>
        public bool AudioThreadRunning
        {
            get { return _audioThreadRunning; }
            set { AccessCheck(); _audioThreadRunning = value; }
        }

        /// <summary>
        /// Whether or not the form hosting the main application has been closed.
        /// </summary>
        public bool FormClosed
        {
            get { return _formClosed; }
            set { AccessCheck(); _formClosed = value; }
        }

        /// <summary>
        /// Indicates whether or not EC fan control is enabled.
        /// </summary>
        public bool EcFanControlEnabled
        {
            get { return _ecFanControlEnabled; }
            set { AccessCheck(); _ecFanControlEnabled = value; }
        }

        /// <summary>
        /// Current level manually set for fan 1.
        /// </summary>
        public FanLevel? Fan1Level
        {
            get { return _fan1Level; }
            set { AccessCheck(); _fan1Level = value; }
        }

        /// <summary>
        /// Current level manually set for fan 2.
        /// </summary>
        public FanLevel? Fan2Level
        {
            get { return _fanLevel2; }
            set { AccessCheck(); _fanLevel2 = value; }
        }

        /// <summary>
        /// Status of the consistency mode system.
        /// </summary>
        public string ConsistencyModeStatus
        {
            get { return _consistencyModeStatus; }
            set { AccessCheck(); _consistencyModeStatus = value; }
        }

        /// <summary>
        /// The currently selected audio device.
        /// </summary>
        public AudioDevice SelectedAudioDevice
        {
            get { return _selectedAudioDevice; }
            set { AccessCheck(); _selectedAudioDevice = value; }
        }

        /// <summary>
        /// If the selected audio device disappears and returns, we want to automatically select it again.
        /// </summary>
        public AudioDevice BringBackAudioDevice
        {
            get { return _bringBackAudioDevice; }
            set { AccessCheck(); _bringBackAudioDevice = value; }
        }

        /// <summary>
        /// RPM value for fan 1.
        /// </summary>
        public ulong Fan1Rpm { get; private set; }

        /// <summary>
        /// RPM value for fan 2.
        /// </summary>
        public ulong Fan2Rpm { get; private set; }

        /// <summary>
        /// Indicates whether or not a second fan is present in the system.
        /// </summary>
        public bool Fan2Present { get; private set; }

        /// <summary>
        /// Current temperatures.
        /// </summary>
        public Dictionary<TemperatureComponent, IReadOnlyDictionary<string, int>> Temperatures { get; private set; }

        /// <summary>
        /// Current "thermal setting".
        /// </summary>
        public ThermalSetting ThermalSetting { get; private set; }

        /// <summary>
        /// List of audio devices in the system (keyed by ID).
        /// </summary>
        public List<AudioDevice> AudioDevices { get; private set; }
    }
}
