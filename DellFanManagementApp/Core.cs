using DellFanManagement.Interop;
using DellFanManagement.SmmIo;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace DellFanManagement.App
{
    class Core
    {
        /// <summary>
        /// How often to refresh the system state, in milliseconds.
        /// </summary>
        private static readonly int RefreshInterval = 1000;

        /// <summary>
        /// Shared object which contains the state of the application.
        /// </summary>
        private readonly State _state;

        /// <summary>
        /// Form object running the application.
        /// </summary>
        private readonly DellFanManagementGuiForm _form;

        /// <summary>
        /// Used to play back sounds in the application.
        /// </summary>
        private SoundPlayer _soundPlayer;

        /// <summary>
        /// Block state change requests while a state update is in progress.
        /// </summary>
        private readonly Semaphore _requestSemaphore;

        /// <summary>
        /// Thermal setting that has been requested by the user but not yet applied.
        /// </summary>
        private ThermalSetting? _requestedThermalSetting;

        /// <summary>
        /// Indicates whether or not the user has requested that EC fan control be enabled.
        /// </summary>
        private bool _ecFanControlRequested;

        /// <summary>
        /// User requested level for fan 1.
        /// </summary>
        private FanLevel? _fan1LevelRequested;

        /// <summary>
        /// User requested level for fan 2.
        /// </summary>
        private FanLevel? _fan2LevelRequested;

        /// <summary>
        /// Lower temperature threshold for keep alive.
        /// </summary>
        public int? LowerTemperatureThreshold { get; private set; }

        /// <summary>
        /// Upper temperature threshold for keep alive.
        /// </summary>
        public int? UpperTemperatureThreshold { get; private set; }

        /// <summary>
        /// Fan RPM threshold for keep alive.
        /// </summary>
        public ulong? RpmThreshold { get; private set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="state">Shared state object</param>
        /// <param name="form">Form object (hosting the application)</param>
        public Core(State state, DellFanManagementGuiForm form)
        {
            _state = state;
            _form = form;
            _soundPlayer = null;
            _requestSemaphore = new(1, 1);

            _requestedThermalSetting = null;
            _ecFanControlRequested = true;
            _fan1LevelRequested = null;
            _fan2LevelRequested = null;

            LowerTemperatureThreshold = null;
            UpperTemperatureThreshold = null;
            RpmThreshold = null;
        }

        /// <summary>
        /// Switch configuration to automatic mode.
        /// </summary>
        public void SetAutomaticMode()
        {
            _state.WaitOne();
            _state.Configuration = Configuration.Automatic;
            _state.KeepAliveStatus = string.Empty;
            _state.Release();
        }

        /// <summary>
        /// Switch configuration to manual mode.
        /// </summary>
        public void SetManualMode()
        {
            _state.WaitOne();
            _state.Configuration = Configuration.Manual;
            _ecFanControlRequested = _state.EcFanControlEnabled;
            _state.KeepAliveStatus = string.Empty;
            _state.Release();
        }

        /// <summary>
        /// Switch configuration to keep alive mode.
        /// </summary>
        public void SetKeepAliveMode()
        {
            _state.WaitOne();
            _state.Configuration = Configuration.KeepAlive;
            _state.Release();
        }

        /// <summary>
        /// Request that EC fan control be enabled or disabled.
        /// </summary>
        /// <param name="enabled">True to enable EC fan control, false to disable it</param>
        public void RequestEcFanControl(bool enabled)
        {
            _requestSemaphore.WaitOne();
            _ecFanControlRequested = enabled;
            _requestSemaphore.Release();
        }

        /// <summary>
        /// Requested a specific fan level for level 1.
        /// </summary>
        /// <param name="level">Fan level to set</param>
        public void RequestFan1Level(FanLevel? level)
        {
            _requestSemaphore.WaitOne();
            _fan1LevelRequested = level;
            _requestSemaphore.Release();
        }

        /// <summary>
        /// Requested a specific fan level for level 2.
        /// </summary>
        /// <param name="level">Fan level to set</param>
        public void RequestFan2Level(FanLevel? level)
        {
            _requestSemaphore.WaitOne();
            _fan2LevelRequested = level;
            _requestSemaphore.Release();
        }

        /// <summary>
        /// Request that the thermal setting be updated.
        /// </summary>
        /// <param name="requestedThermalSetting">Requested thermal setting</param>
        public void RequestThermalSetting(ThermalSetting requestedThermalSetting)
        {
            _requestSemaphore.WaitOne();
            _requestedThermalSetting = requestedThermalSetting;
            _requestSemaphore.Release();
        }

        /// <summary>
        /// Set the state audio device for the audio keep-alive thread.
        /// </summary>
        /// <param name="device">Selected audio device</param>
        public void RequestAudioDevice(AudioDevice device)
        {
            bool activeAudioDeviceChanged = false;

            _state.WaitOne();

            if (device != null && device != _state.SelectedAudioDevice && _state.AudioThreadRunning)
            {
                activeAudioDeviceChanged = true;
            }

            _state.SelectedAudioDevice = device;

            _state.Release();

            if (activeAudioDeviceChanged)
            {
                StopAudioThread();
            }
        }

        /// <summary>
        /// Start up the application "background thread", which monitors the system state.
        /// </summary>
        public void StartBackgroundThread()
        {
            new Thread(new ThreadStart(BackgroundThread)).Start();
        }

        /// <summary>
        /// The background thread runs in a loop.  It collects RPM and temperature data and handles the program's main
        /// background behavior.
        /// </summary>
        private void BackgroundThread()
        {
            _state.WaitOne();
            _state.BackgroundThreadRunning = true;
            _state.Release();

            try
            {
                // Initialize and turn on EC control.
                DellFanLib.Initialize();
                DellFanLib.EnableEcFanControl();

                Thread.Sleep(Core.RefreshInterval);

                while (_state.BackgroundThreadRunning)
                {
                    _state.WaitOne();

                    // Update state.
                    _state.Update();

                    _requestSemaphore.WaitOne();

                    // Take action based on configuration.
                    if (_state.Configuration == Configuration.Automatic)
                    {
                        if (!_state.EcFanControlEnabled)
                        {
                            _state.EcFanControlEnabled = true;
                            DellFanLib.EnableEcFanControl();
                        }
                    }
                    else if (_state.Configuration == Configuration.Manual)
                    {
                        // Check for EC control state changes that need to be applied.
                        if (_ecFanControlRequested && !_state.EcFanControlEnabled)
                        {
                            _state.EcFanControlEnabled = true;
                            DellFanLib.EnableEcFanControl();
                        }
                        else if (!_ecFanControlRequested && _state.EcFanControlEnabled)
                        {
                            _state.EcFanControlEnabled = false;
                            _state.Fan1Level = null;
                            _state.Fan2Level = null;
                            _fan1LevelRequested = null;
                            _fan2LevelRequested = null;
                            DellFanLib.DisableEcFanControl();
                        }

                        // Check for fan control state changes that need to be applied.
                        if (!_state.EcFanControlEnabled)
                        {
                            if (_state.Fan1Level != _fan1LevelRequested)
                            {
                                _state.Fan1Level = _fan1LevelRequested;
                                if (_fan1LevelRequested != null)
                                {
                                    DellFanLib.SetFanLevel(FanIndex.Fan1, (FanLevel)_fan1LevelRequested);
                                }
                            }

                            if (_state.Fan2Level != _fan2LevelRequested)
                            {
                                _state.Fan2Level = _fan2LevelRequested;
                                if (_fan2LevelRequested != null)
                                {
                                    DellFanLib.SetFanLevel(FanIndex.Fan2, (FanLevel)_fan2LevelRequested);
                                }
                            }
                        }
                    }
                    else if (_state.Configuration == Configuration.KeepAlive)
                    {
                        // Keep alive logic.
                        CheckKeepAlive();
                    }

                    if (_requestedThermalSetting != null)
                    {
                        if (_requestedThermalSetting != _state.ThermalSetting)
                        {
                            DellSmmIoLib.SetThermalSetting((ThermalSetting)_requestedThermalSetting);
                            _state.UpdateThermalSetting();
                        }

                        _requestedThermalSetting = null;
                    }

                    _requestSemaphore.Release();
                    _state.Release();

                    UpdateForm();

                    Thread.Sleep(Core.RefreshInterval);
                }
            }
            catch (Exception exception)
            {
                _state.Error = string.Format("{0}: {1}\n{2}", exception.GetType().ToString(), exception.Message, exception.StackTrace);
                Log.Write(_state.Error);
            }

            DellFanLib.Shutdown();

            _state.WaitOne();
            _state.BackgroundThreadRunning = false;
            _state.Release();

            UpdateForm();
        }

        /// <summary>
        /// Keep alive logic; lock the fans if temperature and RPM thresholds are met.
        /// </summary>
        private void CheckKeepAlive()
        {
            if (LowerTemperatureThreshold != null && UpperTemperatureThreshold != null && RpmThreshold != null)
            {
                bool thresholdsMet = true;
                ulong? rpmLowerThreshold = RpmThreshold - 400;

                foreach (KeyValuePair<string, int> temperature in _state.Temperatures)
                {
                    if (temperature.Value > (_state.EcFanControlEnabled ? LowerTemperatureThreshold : UpperTemperatureThreshold))
                    {
                        _state.KeepAliveStatus = "Waiting for CPU or GPU temp to fall";
                        thresholdsMet = false;
                    }
                }

                if (thresholdsMet)
                {
                    if (_state.Fan1Rpm > RpmThreshold || (_state.Fan2Present && _state.Fan2Rpm > RpmThreshold))
                    {
                        _state.KeepAliveStatus = "Waiting for fan speed to decrease";
                        thresholdsMet = false;
                    }
                    else if (_state.Fan1Rpm < rpmLowerThreshold || (_state.Fan2Present && _state.Fan2Rpm < rpmLowerThreshold))
                    {
                        _state.KeepAliveStatus = "Waiting for fan speed to increase";
                        thresholdsMet = false;
                    }
                }

                if (thresholdsMet)
                {
                    if (_state.EcFanControlEnabled)
                    {
                        _state.EcFanControlEnabled = false;
                        DellFanLib.DisableEcFanControl();
                    }
                    _state.KeepAliveStatus = "Fan speed is locked";
                }
                else if (!_state.EcFanControlEnabled && !thresholdsMet)
                {
                    _state.EcFanControlEnabled = true;
                    DellFanLib.EnableEcFanControl();
                }
            }
        }

        /// <summary>
        /// Start up the "audio thread", which streams silence to a particular audio device.
        /// </summary>
        public void StartAudioThread()
        {
            new Thread(new ThreadStart(AudioThread)).Start();
        }

        /// <summary>
        /// Request that the audio thread be terminated.
        /// </summary>
        public void StopAudioThread()
        {
            _soundPlayer?.RequestTermination();
        }

        /// <summary>
        /// The audio thread streams silence to a particular audio output device.
        /// </summary>
        private void AudioThread()
        {
            bool audioKeepAliveEnabled = false;
            AudioDevice selectedAudioDevice = null;

            try
            {
                _state.WaitOne();

                if (!_state.AudioThreadRunning)
                {
                    audioKeepAliveEnabled = true;
                    selectedAudioDevice = _state.SelectedAudioDevice;
                    _state.AudioThreadRunning = true;
                }
                else
                {
                    // Somehow, there was an attempt to start the audio thread when it was running already?
                }

                _state.Release();

                if (audioKeepAliveEnabled && selectedAudioDevice != null)
                {
                    _soundPlayer = new(selectedAudioDevice);
                    _soundPlayer.PlaySound(@"silence.wav", true);
                }
            }
            catch (Exception)
            {
                // Take no action, just allow the thread to terminate without error.
            }

            _soundPlayer = null;

            // Audio thread terminating.
            if (audioKeepAliveEnabled)
            {
                _state.WaitOne();
                _state.AudioThreadRunning = false;
                _state.Release();

                UpdateForm();
            }
        }

        /// <summary>
        /// Request that the GUI form update using current values from the state.
        /// </summary>
        private void UpdateForm()
        {
            MethodInvoker updateInvoker = new(_form.UpdateForm);

            if (!_state.FormClosed)
            {
                try
                {
                    _form.BeginInvoke(updateInvoker);
                }
                catch (Exception)
                {
                    // Take no action.
                    // (There could be an error if trying to update the form after it has been closed... let it slide.)
                }
            }
        }

        /// <summary>
        /// Write the keep alive configuration.
        /// </summary>
        /// <param name="lowerTemperatureThreshold">Lower temperature threshold</param>
        /// <param name="upperTemperatureThreshold">Upper temperature threshold</param>
        /// <param name="rpmThreshold">Fan speed threshold</param>
        public void WriteKeepAliveConfiguration(int lowerTemperatureThreshold, int upperTemperatureThreshold, int rpmThreshold)
        {
            LowerTemperatureThreshold = lowerTemperatureThreshold;
            UpperTemperatureThreshold = upperTemperatureThreshold;
            RpmThreshold = ulong.Parse(rpmThreshold.ToString());
        }
    }
}
