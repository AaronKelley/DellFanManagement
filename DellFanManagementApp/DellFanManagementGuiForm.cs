using DellFanManagement.App.TemperatureReaders;
using DellFanManagement.Interop;
using DellFanManagement.SmmIo;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace DellFanManagement.App
{
    /// <summary>
    /// This class manages the Windows Forms application for the app.
    /// </summary>
    public partial class DellFanManagementGuiForm : Form
    {
        /// <summary>
        /// Shared object which contains the application state.
        /// </summary>
        private readonly State _state;

        /// <summary>
        /// The "Core" object does the actual system interations.
        /// </summary>
        private readonly Core _core;

        /// <summary>
        /// Handles storing the options selected in the program in the registry.
        /// </summary>
        private readonly ConfigurationStore _configurationStore;

        /// <summary>
        /// Pre-loaded icons to use in the system tray.
        /// </summary>
        private readonly Icon[] _trayIcons;

        /// <summary>
        /// Next tray icon animation to be displayed.
        /// </summary>
        private int _trayIconIndex;

        /// <summary>
        /// Indicates that the program is closing, so background threads should stop.
        /// </summary>
        private bool _formClosed;

        /// <summary>
        /// Indicates whether the initial setup code has finished or not.
        /// </summary>
        private readonly bool _initializationComplete;

        /// <summary>
        /// Constructor.  Get everything set up before the window is displayed.
        /// </summary>
        public DellFanManagementGuiForm()
        {
            _initializationComplete = false;

            InitializeComponent();

            // Initialize objects.
            _state = new State();
            _core = new Core(_state, this);
            _configurationStore = new();
            _formClosed = false;

            _trayIcons = new Icon[48];
            _trayIconIndex = 0;
            LoadTrayIcons();

            // Disclaimer.
            if (_configurationStore.GetIntOption(ConfigurationOption.DisclaimerShown) != 1)
            {
                MessageBox.Show("Note: This program is not created by or affiliated with Dell Inc. or Dell Technologies Inc., and while every has been made to make it safe to use, it does interact with the embedded controller and system BIOS using undocumented methods and may have adverse effects on your system.  Use at your own risk.  If you experience odd behavior, a full system shutdown should restore everything back to the original state.", "Dell Fan Management – Disclaimer");
                _configurationStore.SetOption(ConfigurationOption.DisclaimerShown, 1);
            }
            
            // Version number in the about box.
            aboutProductLabel.Text = string.Format("Dell Fan Management, version {0}", DellFanLib.Version);

            // Set event handlers.
            FormClosed += new FormClosedEventHandler(FormClosedEventHandler);

            // ...Thermal setting radio buttons...
            thermalSettingRadioButtonOptimized.CheckedChanged += new EventHandler(ThermalSettingChangedEventHandler);
            thermalSettingRadioButtonCool.CheckedChanged += new EventHandler(ThermalSettingChangedEventHandler);
            thermalSettingRadioButtonQuiet.CheckedChanged += new EventHandler(ThermalSettingChangedEventHandler);
            thermalSettingRadioButtonPerformance.CheckedChanged += new EventHandler(ThermalSettingChangedEventHandler);

            // ...EC fan control radio buttons...
            ecFanControlRadioButtonOn.CheckedChanged += new EventHandler(EcFanControlSettingChangedEventHandler);
            ecFanControlRadioButtonOff.CheckedChanged += new EventHandler(EcFanControlSettingChangedEventHandler);

            // ...Manual fan control radio buttons...
            manualFan1RadioButtonOff.CheckedChanged += new EventHandler(FanLevelChangedEventHandler);
            manualFan1RadioButtonMedium.CheckedChanged += new EventHandler(FanLevelChangedEventHandler);
            manualFan1RadioButtonHigh.CheckedChanged += new EventHandler(FanLevelChangedEventHandler);
            manualFan2RadioButtonOff.CheckedChanged += new EventHandler(FanLevelChangedEventHandler);
            manualFan2RadioButtonMedium.CheckedChanged += new EventHandler(FanLevelChangedEventHandler);
            manualFan2RadioButtonHigh.CheckedChanged += new EventHandler(FanLevelChangedEventHandler);

            // ...Restart background thread button...
            restartBackgroundThreadButton.Click += new EventHandler(ThermalSettingChangedEventHandler);
            
            // ...Operation mode radio buttons...
            operationModeRadioButtonAutomatic.CheckedChanged += new EventHandler(ConfigurationRadioButtonAutomaticEventHandler);
            operationModeRadioButtonManual.CheckedChanged += new EventHandler(ConfigurationRadioButtonManualEventHandler);
            operationModeRadioButtonConsistency.CheckedChanged += new EventHandler(ConfigurationRadioButtonConsistencyEventHandler);

            // ...Consistency mode section...
            consistencyModeLowerTemperatureThresholdTextBox.TextChanged += new EventHandler(ConsistencyModeTextBoxesChangedEventHandler);
            consistencyModeUpperTemperatureThresholdTextBox.TextChanged += new EventHandler(ConsistencyModeTextBoxesChangedEventHandler);
            consistencyModeRpmThresholdTextBox.TextChanged += new EventHandler(ConsistencyModeTextBoxesChangedEventHandler);
            consistencyModeApplyChangesButton.Click += new EventHandler(ConsistencyApplyChangesButtonClickedEventHandler);

            // ...Audio keep alive controls...
            audioKeepAliveComboBox.SelectedValueChanged += new EventHandler(AudioDeviceChangedEventHandler);
            audioKeepAliveCheckbox.CheckedChanged += new EventHandler(AudioKeepAliveCheckboxChangedEventHandler);

            // ...Tray icon checkboxes...
            trayIconCheckBox.CheckedChanged += new EventHandler(TrayIconCheckBoxChangedEventHandler);
            animatedCheckBox.CheckedChanged += new EventHandler(AnimatedCheckBoxChangedEventHandler);

            // Empty out pre-populated temperature label text fields.
            // (There are so many to allow support for lots of CPU cores, which many systems will not have.)
            temperatureLabel1.Text = string.Empty;
            temperatureLabel2.Text = string.Empty;
            temperatureLabel3.Text = string.Empty;
            temperatureLabel4.Text = string.Empty;
            temperatureLabel5.Text = string.Empty;
            temperatureLabel6.Text = string.Empty;
            temperatureLabel7.Text = string.Empty;
            temperatureLabel8.Text = string.Empty;
            temperatureLabel9.Text = string.Empty;
            temperatureLabel10.Text = string.Empty;
            temperatureLabel11.Text = string.Empty;
            temperatureLabel12.Text = string.Empty;
            temperatureLabel13.Text = string.Empty;
            temperatureLabel14.Text = string.Empty;
            temperatureLabel15.Text = string.Empty;
            temperatureLabel16.Text = string.Empty;
            temperatureLabel17.Text = string.Empty;
            temperatureLabel18.Text = string.Empty;

            // Apply configuration loaded from registry.
            ApplyConfiguration();

            // Save initial keep alive configuration.
            WriteConsistencyModeConfiguration();

            // Initial update of the tray icon (required for it to appear for display).
            UpdateTrayIcon(false);

            // Update form with default state values.
            UpdateForm();

            // Apply audio keep alive configuration from registry.
            ApplyAudioKeepAliveConfiguration();

            // Apply manual fan control configuration from registry.
            ApplyManualModeConfiguration();

            // Start threads to do background work.
            _core.StartBackgroundThread();
            StartTrayIconThread();

            _initializationComplete = true;
        }

        /// <summary>
        /// Apply configuration settings loaded from the registry.
        /// </summary>
        private void ApplyConfiguration()
        {
            // The tray icon is enabled by default; disable only if that has been explicitly set.
            if (_configurationStore.GetIntOption(ConfigurationOption.TrayIconEnabled) == 0)
            {
                trayIconCheckBox.Checked = false;
            }

            // Similar for tray icon animation.
            if (_configurationStore.GetIntOption(ConfigurationOption.TrayIconAnimationEnabled) == 0)
            {
                animatedCheckBox.Checked = false;
            }

            // Consistency mode settings.
            int? lowerTemperatureThreshold = _configurationStore.GetIntOption(ConfigurationOption.ConsistencyModeLowerTemperatureThreshold);
            int? upperTemperatureThreshold = _configurationStore.GetIntOption(ConfigurationOption.ConsistencyModeUpperTemperatureThreshold);

            if (lowerTemperatureThreshold != null && lowerTemperatureThreshold >= 0 && lowerTemperatureThreshold < 100 &&
                upperTemperatureThreshold != null && upperTemperatureThreshold >= 0 && upperTemperatureThreshold < 100 &&
                lowerTemperatureThreshold <= upperTemperatureThreshold)
            {
                consistencyModeLowerTemperatureThresholdTextBox.Text = lowerTemperatureThreshold.ToString();
                consistencyModeUpperTemperatureThresholdTextBox.Text = upperTemperatureThreshold.ToString();
            }

            int? rpmThreshold = _configurationStore.GetIntOption(ConfigurationOption.ConsistencyModeRpmThreshold);
            if (rpmThreshold != null && rpmThreshold > 0 && rpmThreshold < 10000)
            {
                consistencyModeRpmThresholdTextBox.Text = rpmThreshold.ToString();
            }

            // Read previous operation mode from configuration.
            if (Enum.TryParse(_configurationStore.GetStringOption(ConfigurationOption.OperationMode), out OperationMode operationMode))
            {
                switch (operationMode)
                {
                    case OperationMode.Automatic:
                        operationModeRadioButtonAutomatic.Checked = true;
                        break;
                    case OperationMode.Manual:
                        operationModeRadioButtonManual.Checked = true;
                        break;
                    case OperationMode.Consistency:
                        operationModeRadioButtonConsistency.Checked = true;
                        break;
                }
            }
            else
            {
                // Default to automatic mode.
                operationModeRadioButtonAutomatic.Checked = true;
            }
        }

        /// <summary>
        /// Apply audio keep alive configuration using values from the registry.
        /// </summary>
        private void ApplyAudioKeepAliveConfiguration()
        {
            if (_configurationStore.GetIntOption(ConfigurationOption.AudioKeepAliveEnabled) == 1)
            {
                // Audio keep alive should be enabled.  Let's see if the audio device is present.
                string storedAudioDeviceId = _configurationStore.GetStringOption(ConfigurationOption.AudioKeepAliveSelectedDevice);

                foreach (object deviceObject in audioKeepAliveComboBox.Items)
                {
                    AudioDevice device = (AudioDevice)deviceObject;
                    if (device.DeviceId == storedAudioDeviceId)
                    {
                        // Found it!
                        audioKeepAliveComboBox.SelectedItem = deviceObject;
                        audioKeepAliveCheckbox.Checked = true; // This will kick off the audio thread.
                        break;
                    }
                }

                // If we get down here and the checkbox is not checked, then the device was not in the list.
                if (!audioKeepAliveCheckbox.Checked)
                {
                    _configurationStore.SetOption(ConfigurationOption.AudioKeepAliveEnabled, 0);
                }
            }
        }

        /// <summary>
        /// Apply manual mode configuration from the registry.
        /// </summary>
        private void ApplyManualModeConfiguration()
        {
            if (operationModeRadioButtonManual.Checked)
            {
                // Apply saved manual mode configuration.
                if (_configurationStore.GetIntOption(ConfigurationOption.ManualModeEcFanControlEnabled) == 0)
                {
                    ecFanControlRadioButtonOff.Checked = true;

                    if (Enum.TryParse(_configurationStore.GetStringOption(ConfigurationOption.ManualModeFan1Level), out FanLevel fan1Level))
                    {
                        switch (fan1Level)
                        {
                            case FanLevel.Level0:
                                manualFan1RadioButtonOff.Checked = true;
                                break;
                            case FanLevel.Level1:
                                manualFan1RadioButtonMedium.Checked = true;
                                break;
                            case FanLevel.Level2:
                                manualFan1RadioButtonHigh.Checked = true;
                                break;
                        }
                    }

                    if (Enum.TryParse(_configurationStore.GetStringOption(ConfigurationOption.ManualModeFan2Level), out FanLevel fan2Level))
                    {
                        switch (fan2Level)
                        {
                            case FanLevel.Level0:
                                manualFan2RadioButtonOff.Checked = true;
                                break;
                            case FanLevel.Level1:
                                manualFan2RadioButtonMedium.Checked = true;
                                break;
                            case FanLevel.Level2:
                                manualFan2RadioButtonHigh.Checked = true;
                                break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Clear the manual operation mode configuration (when we switch to a different mode, it should be reset).
        /// </summary>
        private void ClearManualControlConfiguration()
        {
            _configurationStore.SetOption(ConfigurationOption.ManualModeEcFanControlEnabled, null);
            _configurationStore.SetOption(ConfigurationOption.ManualModeFan1Level, null);
            _configurationStore.SetOption(ConfigurationOption.ManualModeFan2Level, null);
        }

        /// <summary>
        /// Update the form based on the current state.
        /// </summary>
        public void UpdateForm()
        {
            // This method does not write to the state, but we should still make sure that the state is not changing
            // during the update.
            _state.WaitOne();

            AudioDevice bringBackAudioDevice = null;

            // Fan RPM.
            fan1RpmLabel.Text = string.Format("Fan 1 RPM: {0}", _state.Fan1Rpm);

            if (_state.Fan2Present)
            {
                fan2RpmLabel.Text = string.Format("Fan 2 RPM: {0}", _state.Fan2Rpm);
                fan2RpmLabel.Enabled = true;
                manualFan2GroupBox.Enabled = true;
            }
            else
            {
                fan2RpmLabel.Text = string.Format("Fan 2 not present");
                fan2RpmLabel.Enabled = false;

                if (manualFan2GroupBox.Enabled)
                {
                    manualFan2GroupBox.Enabled = false;
                    manualFan2RadioButtonOff.Checked = false;
                    manualFan2RadioButtonMedium.Checked = false;
                    manualFan2RadioButtonHigh.Checked = false;
                }
            }

            // Temperatures.
            int labelIndex = 0;
            foreach (TemperatureComponent component in _state.Temperatures.Keys)
            {
                foreach (string key in _state.Temperatures[component].Keys)
                {
                    string temperature = _state.Temperatures[component][key] != 0 ? _state.Temperatures[component][key].ToString() : "--";

                    string labelValue;
                    if (_state.MinimumTemperatures[component].ContainsKey(key) && _state.MaximumTemperatures[component].ContainsKey(key))
                    {
                        labelValue = string.Format("{0}: {1} ({2}-{3})", key, temperature, _state.MinimumTemperatures[component][key], _state.MaximumTemperatures[component][key]);
                    }
                    else
                    {
                        labelValue = string.Format("{0}: {1}", key, temperature);
                    }

                    switch (labelIndex)
                    {
                        case 0: temperatureLabel1.Text = labelValue; break;
                        case 1: temperatureLabel2.Text = labelValue; break;
                        case 2: temperatureLabel3.Text = labelValue; break;
                        case 3: temperatureLabel4.Text = labelValue; break;
                        case 4: temperatureLabel5.Text = labelValue; break;
                        case 5: temperatureLabel6.Text = labelValue; break;
                        case 6: temperatureLabel7.Text = labelValue; break;
                        case 7: temperatureLabel8.Text = labelValue; break;
                        case 8: temperatureLabel9.Text = labelValue; break;
                        case 9: temperatureLabel10.Text = labelValue; break;
                        case 10: temperatureLabel11.Text = labelValue; break;
                        case 11: temperatureLabel12.Text = labelValue; break;
                        case 12: temperatureLabel13.Text = labelValue; break;
                        case 13: temperatureLabel14.Text = labelValue; break;
                        case 14: temperatureLabel15.Text = labelValue; break;
                        case 15: temperatureLabel16.Text = labelValue; break;
                        case 16: temperatureLabel17.Text = labelValue; break;
                        case 17: temperatureLabel18.Text = labelValue; break;
                    }

                    labelIndex++;
                }
            }

            // EC fan control enabled?
            if (_state.OperationMode != OperationMode.Manual)
            {
                if (_state.EcFanControlEnabled && !ecFanControlRadioButtonOn.Checked)
                {
                    ecFanControlRadioButtonOn.Checked = true;
                }
                else if (!_state.EcFanControlEnabled && !ecFanControlRadioButtonOff.Checked)
                {
                    ecFanControlRadioButtonOff.Checked = true;
                }
            }

            // Consistency mode status.
            consistencyModeStatusLabel.Text = _state.ConsistencyModeStatus;

            // Thermal setting.
            switch (_state.ThermalSetting)
            {
                case ThermalSetting.Optimized:
                    SetThermalSettingAvaiability(true);
                    thermalSettingRadioButtonOptimized.Checked = true;
                    break;
                case ThermalSetting.Cool:
                    SetThermalSettingAvaiability(true);
                    thermalSettingRadioButtonCool.Checked = true;
                    break;
                case ThermalSetting.Quiet:
                    SetThermalSettingAvaiability(true);
                    thermalSettingRadioButtonQuiet.Checked = true;
                    break;
                case ThermalSetting.Performance:
                    SetThermalSettingAvaiability(true);
                    thermalSettingRadioButtonPerformance.Checked = true;
                    break;
                case ThermalSetting.Error:
                    SetThermalSettingAvaiability(false);
                    break;
            }

            // Restart background thread button.
            restartBackgroundThreadButton.Enabled = !_state.BackgroundThreadRunning;

            // Sync up audio devices list.
            List<AudioDevice> devicesToAdd = new();
            List<AudioDevice> devicesToRemove = new();
            
            // Items to add.
            foreach (AudioDevice audioDevice in _state.AudioDevices)
            {
                if (!audioKeepAliveComboBox.Items.Contains(audioDevice))
                {
                    devicesToAdd.Add(audioDevice);
                }
            }

            // Items to remove.
            foreach (AudioDevice audioDevice in audioKeepAliveComboBox.Items)
            {
                if (!_state.AudioDevices.Contains(audioDevice))
                {
                    devicesToRemove.Add(audioDevice);
                }
            }

            // Perform additions and removals.
            foreach (AudioDevice audioDevice in devicesToAdd)
            {
                audioKeepAliveComboBox.Items.Add(audioDevice);

                // ...If this happens to be the previously selected audio device that disappeared, set it back and start
                // the thread.
                if (audioDevice == _state.BringBackAudioDevice || audioDevice.DeviceId == _configurationStore.GetStringOption(ConfigurationOption.AudioKeepAliveBringBackDevice))
                {
                    bringBackAudioDevice = audioDevice;
                }
            }
            foreach (AudioDevice audioDevice in devicesToRemove)
            {
                audioKeepAliveComboBox.Items.Remove(audioDevice);
            }

            if (audioKeepAliveComboBox.SelectedItem == null)
            {
                audioKeepAliveCheckbox.Enabled = false;
            }

            if (audioKeepAliveCheckbox.Checked && !_state.AudioThreadRunning)
            {
                audioKeepAliveCheckbox.Checked = false;
            }

            // Tray icon hover text.
            if (_state.Fan2Present)
            {
                trayIcon.Text = string.Format("Dell Fan Management\n{0}\n{1}", fan1RpmLabel.Text, fan2RpmLabel.Text);
            }
            else
            {
                trayIcon.Text = string.Format("Dell Fan Management\n{0}", fan1RpmLabel.Text);
            }

            UpdateTrayIcon(false);

            // Error message.
            if (_state.Error != null)
            {
                MessageBox.Show(_state.Error, "Error in background thread", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _state.Error = null; // ...The one place where the state is actually updated.
            }

            _state.Release();

            if (bringBackAudioDevice != null)
            {
                audioKeepAliveComboBox.SelectedItem = bringBackAudioDevice;
                audioKeepAliveCheckbox.Checked = true;
            }
        }

        /// <summary>
        /// Called when "Automatic" configuration radio button is clicked.
        /// </summary>
        private void ConfigurationRadioButtonAutomaticEventHandler(Object sender, EventArgs e)
        {
            _core.SetAutomaticMode();
            _configurationStore.SetOption(ConfigurationOption.OperationMode, OperationMode.Automatic);
            ClearManualControlConfiguration();

            SetFanControlsAvailability(false);
            SetConsistencyModeControlsAvailability(false);
            SetEcFanControlsAvailability(false);

            UpdateTrayIcon(false);
        }

        /// <summary>
        /// Called when "Manual" configuration radio button is clicked.
        /// </summary>
        private void ConfigurationRadioButtonManualEventHandler(Object sender, EventArgs e)
        {
            ecFanControlRadioButtonOn.Checked = true;
            _core.SetManualMode();
            _configurationStore.SetOption(ConfigurationOption.OperationMode, OperationMode.Manual);

            SetFanControlsAvailability(false);
            SetConsistencyModeControlsAvailability(false);
            SetEcFanControlsAvailability(true);

            UpdateTrayIcon(false);
        }

        /// <summary>
        /// Called when "Consistency" configuration radio button is clicked.
        /// </summary>
        private void ConfigurationRadioButtonConsistencyEventHandler(Object sender, EventArgs e)
        {
            _core.SetConsistencyMode();
            _configurationStore.SetOption(ConfigurationOption.OperationMode, OperationMode.Consistency);
            ClearManualControlConfiguration();

            SetFanControlsAvailability(false);
            SetConsistencyModeControlsAvailability(true);
            SetEcFanControlsAvailability(false);

            UpdateTrayIcon(false);
        }

        /// <summary>
        /// Enable or disable the manual fan control controls.
        /// </summary>
        /// <param name="enabled">Indicates whether to enable or disable the controls</param>
        private void SetFanControlsAvailability(bool enabled)
        {
            manualGroupBox.Enabled = enabled;

            if (!enabled)
            {
                manualFan1RadioButtonOff.Checked = false;
                manualFan1RadioButtonMedium.Checked = false;
                manualFan1RadioButtonHigh.Checked = false;
                manualFan2RadioButtonOff.Checked = false;
                manualFan2RadioButtonMedium.Checked = false;
                manualFan2RadioButtonHigh.Checked = false;
            }
        }

        /// <summary>
        /// Enable or disbale the consistency mode configuration controls.
        /// </summary>
        /// <param name="enabled">Indicates whether to enable or disable the controls</param>
        private void SetConsistencyModeControlsAvailability(bool enabled)
        {
            consistencyModeGroupBox.Enabled = enabled;
        }

        /// <summary>
        /// Enable or disable the EC fan control on/off controls.
        /// </summary>
        /// <param name="enabled">Indicates whether to enable or disable the controls</param>
        private void SetEcFanControlsAvailability(bool enabled)
        {
            ecFanControlRadioButtonOn.Enabled = enabled;
            ecFanControlRadioButtonOff.Enabled = enabled;
        }

        /// <summary>
        /// Enable or disable the thermal setting controls.
        /// </summary>
        /// <param name="enabled">Indicates whether to enable or disable the controls</param>
        private void SetThermalSettingAvaiability(bool enabled)
        {
            thermalSettingGroupBox.Enabled = enabled;

            if (!enabled)
            {
                thermalSettingRadioButtonOptimized.Checked = false;
                thermalSettingRadioButtonCool.Checked = false;
                thermalSettingRadioButtonQuiet.Checked = false;
                thermalSettingRadioButtonPerformance.Checked = false;
            }
        }

        /// <summary>
        /// If the audio thread terminates, the checkbox should be unchecked to indicate as much.
        /// </summary>
        public void UncheckAudioKeepAlive()
        {
            audioKeepAliveCheckbox.Checked = false;
        }

        /// <summary>
        /// Called when the form is closed.
        /// </summary>
        private void FormClosedEventHandler(Object sender, FormClosedEventArgs e)
        {
            _formClosed = true;

            _state.WaitOne();
            _state.BackgroundThreadRunning = false; // Request termination of background thread.
            _core.StopAudioThread(); // Request termination of the audio thread.
            _state.FormClosed = true;
            _state.Release();
        }

        /// <summary>
        /// Called when the "Restart BG Thread" button is clicked.  Just starts the background thread.
        /// </summary>
        private void RestartBackgroundThreadButtonClickedEventHandler(Object sender, EventArgs e)
        {
            _core.StartBackgroundThread();
        }

        /// <summary>
        /// Called when any of the "thermal setting" radio buttons are clicked.
        /// </summary>
        private void ThermalSettingChangedEventHandler(Object sender, EventArgs e)
        {
            if (thermalSettingRadioButtonOptimized.Checked)
            {
                _core.RequestThermalSetting(ThermalSetting.Optimized);
            }
            else if (thermalSettingRadioButtonCool.Checked)
            {
                _core.RequestThermalSetting(ThermalSetting.Cool);
            }
            else if (thermalSettingRadioButtonQuiet.Checked)
            {
                _core.RequestThermalSetting(ThermalSetting.Quiet);
            }
            else if (thermalSettingRadioButtonPerformance.Checked)
            {
                _core.RequestThermalSetting(ThermalSetting.Performance);
            }
        }

        /// <summary>
        /// Called when the EC fan control on/off radio buttons are clicked.
        /// </summary>
        private void EcFanControlSettingChangedEventHandler(Object sender, EventArgs e)
        {
            if (ecFanControlRadioButtonOn.Checked)
            {
                _core.RequestEcFanControl(true);
                SetFanControlsAvailability(false);
                if (operationModeRadioButtonManual.Checked && _initializationComplete)
                {
                    _configurationStore.SetOption(ConfigurationOption.ManualModeEcFanControlEnabled, 1);
                    _configurationStore.SetOption(ConfigurationOption.ManualModeFan1Level, null);
                    _configurationStore.SetOption(ConfigurationOption.ManualModeFan2Level, null);
                }
            }
            else if (ecFanControlRadioButtonOff.Checked)
            {
                _core.RequestEcFanControl(false);
                if (operationModeRadioButtonManual.Checked)
                {
                    SetFanControlsAvailability(true);
                    _configurationStore.SetOption(ConfigurationOption.ManualModeEcFanControlEnabled, 0);
                }
            }
        }

        /// <summary>
        /// Called when one of the manual fan control level radio buttons is clicked.
        /// </summary>
        private void FanLevelChangedEventHandler(Object sender, EventArgs e)
        {
            // Fan 1.
            FanLevel? fan1LevelRequested = null;
            if (manualFan1RadioButtonOff.Checked)
            {
                fan1LevelRequested = FanLevel.Level0;
            }
            else if (manualFan1RadioButtonMedium.Checked)
            {
                fan1LevelRequested = FanLevel.Level1;
            }
            else if (manualFan1RadioButtonHigh.Checked)
            {
                fan1LevelRequested = FanLevel.Level2;
            }

            if (fan1LevelRequested != null)
            {
                _core.RequestFan1Level(fan1LevelRequested);
                _configurationStore.SetOption(ConfigurationOption.ManualModeFan1Level, fan1LevelRequested);
            }

            // Fan 2.
            FanLevel? fan2LevelRequested = null;
            if (manualFan2RadioButtonOff.Checked)
            {
                fan2LevelRequested = FanLevel.Level0;
            }
            else if (manualFan2RadioButtonMedium.Checked)
            {
                fan2LevelRequested = FanLevel.Level1;
            }
            else if (manualFan2RadioButtonHigh.Checked)
            {
                fan2LevelRequested = FanLevel.Level2;
            }

            if (fan2LevelRequested != null)
            {
                _core.RequestFan2Level(fan2LevelRequested);
                _configurationStore.SetOption(ConfigurationOption.ManualModeFan2Level, fan2LevelRequested);
            }
        }

        /// <summary>
        /// Called when the audio device drop-down selection is changed.
        /// </summary>
        private void AudioDeviceChangedEventHandler(Object sender, EventArgs e)
        {
            _core.RequestAudioDevice((AudioDevice)audioKeepAliveComboBox.SelectedItem);

            if (audioKeepAliveComboBox.SelectedItem != null)
            {
                audioKeepAliveCheckbox.Enabled = true;
                _configurationStore.SetOption(ConfigurationOption.AudioKeepAliveBringBackDevice, null);
            }
            else
            {
                audioKeepAliveCheckbox.Enabled = false;
            }
        }

        /// <summary>
        /// Called when the "audio keep alive" checkbox is checked or unchecked.
        /// </summary>
        private void AudioKeepAliveCheckboxChangedEventHandler(Object sender, EventArgs e)
        {
            if (audioKeepAliveCheckbox.Checked)
            {
                _core.StartAudioThread();
                _configurationStore.SetOption(ConfigurationOption.AudioKeepAliveEnabled, 1);
                _configurationStore.SetOption(ConfigurationOption.AudioKeepAliveSelectedDevice, ((AudioDevice)audioKeepAliveComboBox.SelectedItem).DeviceId);
                _configurationStore.SetOption(ConfigurationOption.AudioKeepAliveBringBackDevice, null);
            }
            else
            {
                _core.StopAudioThread();

                if (!_formClosed)
                {
                    _configurationStore.SetOption(ConfigurationOption.AudioKeepAliveEnabled, 0);
                    _configurationStore.SetOption(ConfigurationOption.AudioKeepAliveSelectedDevice, null);

                    if (_state.BringBackAudioDevice != null)
                    {
                        _configurationStore.SetOption(ConfigurationOption.AudioKeepAliveBringBackDevice, _state.BringBackAudioDevice.DeviceId);
                    }
                }
            }
        }

        /// <summary>
        /// Called when the consistency mode configuration text boxes are modified.
        /// </summary>
        private void ConsistencyModeTextBoxesChangedEventHandler(Object sender, EventArgs e)
        {
            // Enforce digits only in these text boxes.
            if (Regex.IsMatch(consistencyModeLowerTemperatureThresholdTextBox.Text, "[^0-9]"))
            {
                consistencyModeLowerTemperatureThresholdTextBox.Text = Regex.Replace(consistencyModeLowerTemperatureThresholdTextBox.Text, "[^0-9]", "");
            }

            if (Regex.IsMatch(consistencyModeUpperTemperatureThresholdTextBox.Text, "[^0-9]"))
            {
                consistencyModeUpperTemperatureThresholdTextBox.Text = Regex.Replace(consistencyModeUpperTemperatureThresholdTextBox.Text, "[^0-9]", "");
            }

            if (Regex.IsMatch(consistencyModeRpmThresholdTextBox.Text, "[^0-9]"))
            {
                consistencyModeRpmThresholdTextBox.Text = Regex.Replace(consistencyModeRpmThresholdTextBox.Text, "[^0-9]", "");
            }

            CheckConsistencyModeOptionsConsistency();
        }

        /// <summary>
        /// Called when the consistency mode "Apply changes" button is clicked.
        /// </summary>
        private void ConsistencyApplyChangesButtonClickedEventHandler(Object sender, EventArgs e)
        {
            WriteConsistencyModeConfiguration();
        }

        /// <summary>
        /// Called when the "tray icon" checkbox is clicked.
        /// </summary>
        private void TrayIconCheckBoxChangedEventHandler(Object sender, EventArgs e)
        {
            UpdateTrayIcon(false);
            animatedCheckBox.Enabled = trayIconCheckBox.Checked;

            _configurationStore.SetOption(ConfigurationOption.TrayIconEnabled, trayIconCheckBox.Checked ? 1 : 0);
        }

        /// <summary>
        /// Called when the "animated" checkbox is clicked.
        /// </summary>
        private void AnimatedCheckBoxChangedEventHandler(Object sender, EventArgs e)
        {
            UpdateTrayIcon(false);

            _configurationStore.SetOption(ConfigurationOption.TrayIconAnimationEnabled, animatedCheckBox.Checked ? 1 : 0);
        }

        /// <summary>
        /// Check to see if the GUI consistency mode options text boxes match the currently stored configuration, and
        /// enable or disable the "apply changes" button accordingly.
        /// </summary>
        private void CheckConsistencyModeOptionsConsistency()
        {
            bool result = false;

            if (consistencyModeLowerTemperatureThresholdTextBox.Text != _core.LowerTemperatureThreshold.ToString() ||
                consistencyModeUpperTemperatureThresholdTextBox.Text != _core.UpperTemperatureThreshold.ToString() ||
                consistencyModeRpmThresholdTextBox.Text != _core.RpmThreshold.ToString())
            {
                // Configuration doesn't match.  Check for flip-flop.
                bool success = int.TryParse(consistencyModeLowerTemperatureThresholdTextBox.Text, out int lowerTemperatureThreshold);
                if (success)
                {
                    success = int.TryParse(consistencyModeUpperTemperatureThresholdTextBox.Text, out int upperTemperatureThreshold);
                    if (success)
                    {
                        if (upperTemperatureThreshold >= lowerTemperatureThreshold)
                        {
                            // Looks good, we can enable the button.
                            result = true;
                        }
                    }
                }
            }

            consistencyModeApplyChangesButton.Enabled = result;
        }

        /// <summary>
        /// Take the consistency mode configuration and save it to the core.
        /// </summary>
        private void WriteConsistencyModeConfiguration()
        {
            bool success = int.TryParse(consistencyModeLowerTemperatureThresholdTextBox.Text, out int lowerTemperatureThreshold);
            if (success)
            {
                success = int.TryParse(consistencyModeUpperTemperatureThresholdTextBox.Text, out int upperTemperatureThreshold);
                if (success)
                {
                    success = int.TryParse(consistencyModeRpmThresholdTextBox.Text, out int rpmThreshold);
                    if (success)
                    {
                        _core.WriteConsistencyModeConfiguration(lowerTemperatureThreshold, upperTemperatureThreshold, rpmThreshold);

                        _configurationStore.SetOption(ConfigurationOption.ConsistencyModeLowerTemperatureThreshold, lowerTemperatureThreshold);
                        _configurationStore.SetOption(ConfigurationOption.ConsistencyModeUpperTemperatureThreshold, upperTemperatureThreshold);
                        _configurationStore.SetOption(ConfigurationOption.ConsistencyModeRpmThreshold, rpmThreshold);

                        CheckConsistencyModeOptionsConsistency();
                    }
                }
            }
        }

        /// <summary>
        /// Load system tray icons.
        /// </summary>
        private void LoadTrayIcons()
        {
            int globalIndex = 0;

            foreach (string color in new string[] { "Grey", "Blue", "Red" })
            {
                for (int index = 1; index <= 16; index++)
                {
                    _trayIcons[globalIndex++] = new Icon(string.Format(@"Resources\Fan-{0}-{1}.ico", color, index));
                }
            }
        }

        /// <summary>
        /// Update the system tray icon.
        /// </summary>
        /// <param name="advance">Whether or not to advance a frame</param>
        private void UpdateTrayIcon(bool advance)
        {
            // Actually, hide tray icon if it is not enabled.
            trayIcon.Visible = trayIconCheckBox.Checked;

            if (trayIconCheckBox.Checked)
            {
                int offset;

                if (_state.OperationMode != OperationMode.Consistency)
                {
                    offset = 0;
                }
                else
                {
                    if (!_state.EcFanControlEnabled)
                    {
                        offset = 16;
                    }
                    else
                    {
                        offset = 32;
                    }
                }

                if (animatedCheckBox.Checked)
                {
                    if (advance)
                    {
                        _trayIconIndex = (_trayIconIndex + 1) % (_trayIcons.Length / 3);
                    }
                }
                else
                {
                    _trayIconIndex = 0;
                }

                Icon newIcon = _trayIcons[_trayIconIndex + offset];
                if (trayIcon.Icon != newIcon)
                {
                    trayIcon.Icon = newIcon;
                }
            }
        }

        /// <summary>
        /// Update the system tray icon (advance one frame).
        /// </summary>
        private void UpdateTrayIcon()
        {
            UpdateTrayIcon(true);
        }

        /// <summary>
        /// Kicks off the thread that handles the tray icon animation.
        /// </summary>
        private void StartTrayIconThread()
        {
            new Thread(new ThreadStart(TrayIconThread)).Start();
        }

        /// <summary>
        /// Update the tray icon, changing speed with the fan RPM.
        /// </summary>
        private void TrayIconThread()
        {
            try
            {
                MethodInvoker updateInvoker = new(UpdateTrayIcon);

                while (!_formClosed)
                {
                    int waitTime = 1000; // One second.

                    if (trayIconCheckBox.Checked && animatedCheckBox.Checked)
                    {
                        // Grab state information that we need.
                        ulong averageRpm;
                        if (_state.Fan2Present)
                        {
                            averageRpm = (_state.Fan1Rpm + _state.Fan2Rpm) / 2;
                        }
                        else
                        {
                            averageRpm = _state.Fan1Rpm;
                        }

                        if (averageRpm > 250 && averageRpm < 10000)
                        {
                            try
                            {
                                BeginInvoke(updateInvoker);
                            }
                            catch (Exception)
                            {
                                // If the window handle is not here (not open yet, or closing), there could be an error.
                                // Silently ignore.
                            }

                            // Higher RPM = lower wait time = faster animation.
                            waitTime = 250000 / (int)averageRpm;
                        }
                    }

                    Thread.Sleep(Math.Min(waitTime, 1000));
                }
            }
            catch (Exception exception)
            {
                Log.Write(exception);
            }
        }
    }
}
