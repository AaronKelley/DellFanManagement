using DellFanManagement.App.FanControllers;
using DellFanManagement.App.TemperatureReaders;
using System;
using System.Collections.Generic;

namespace DellFanManagement.App.ConsistencyModeHandlers
{
    /// <summary>
    /// Used when full fan control is possible to "lock" the fan speed when conditions are right.
    /// </summary>
    public class LegacyConsistencyModeHandler : ConsistencyModeHandler
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="core">Core object.</param>
        /// <param name="state">State object.</param>
        /// <param name="fanController">The fan controller currently in use.</param>
        public LegacyConsistencyModeHandler(Core core, State state, FanController fanController) : base(core, state, fanController)
        {
            // No action here.
        }

        /// <summary>
        /// Consistency mode logic; lock the fans if temperature and RPM thresholds are met.
        /// </summary>
        public override void RunConsistencyModeLogic()
        {
            if (_core.LowerTemperatureThreshold != null && _core.UpperTemperatureThreshold != null && _core.RpmThreshold != null)
            {
                bool thresholdsMet = true;
                ulong? rpmLowerThreshold = _core.RpmThreshold - 400;

                // Is the fan speed too low?
                if (_state.Fan1Rpm < rpmLowerThreshold || (_state.Fan2Present && _state.Fan2Rpm < rpmLowerThreshold))
                {
                    if (_state.Fan1Rpm == 0 && (!_state.Fan2Present || _state.Fan2Rpm == 0))
                    {
                        _state.ConsistencyModeStatus = string.Format("Waiting for embedded controller to activate the fan{0}", _state.Fan2Present ? "s" : string.Empty);
                    }
                    else
                    {
                        _state.ConsistencyModeStatus = "Waiting for embedded controller to raise the fan speed";
                    }

                    thresholdsMet = false;

                    if (!_state.EcFanControlEnabled)
                    {
                        Log.Write(string.Format("EC fan control disabled, but fan speed is too low: [{0}] [{1}]", _state.Fan1Rpm, _state.Fan2Present ? _state.Fan2Rpm : "N/A"));
                    }
                }

                // Is the CPU or GPU too hot?
                if (thresholdsMet)
                {
                    foreach (TemperatureComponent component in _state.Temperatures.Keys)
                    {
                        foreach (KeyValuePair<string, int> temperature in _state.Temperatures[component])
                        {
                            if (temperature.Value > (_state.EcFanControlEnabled ? _core.LowerTemperatureThreshold : _core.UpperTemperatureThreshold))
                            {
                                _state.ConsistencyModeStatus = string.Format("Waiting for {0} temperature to fall", component);
                                thresholdsMet = false;
                                break;
                            }
                        }

                        if (!thresholdsMet)
                        {
                            break;
                        }
                    }
                }

                // Is the fan speed too high?
                if (thresholdsMet && (_state.Fan1Rpm > _core.RpmThreshold || (_state.Fan2Present && _state.Fan2Rpm > _core.RpmThreshold)))
                {
                    if (_state.Fan1Rpm < Core.RpmSanityCheck && (!_state.Fan2Present || _state.Fan2Rpm < Core.RpmSanityCheck))
                    {
                        _state.ConsistencyModeStatus = "Waiting for embedded controller to reduce the fan speed";
                        thresholdsMet = false;

                        if (!_state.EcFanControlEnabled)
                        {
                            Log.Write(string.Format("EC fan control disabled, but fan speed is too high: [{0}] [{1}]", _state.Fan1Rpm, _state.Fan2Present ? _state.Fan2Rpm : "N/A"));
                        }
                    }
                    else
                    {
                        Log.Write(string.Format("Recorded fan speed above sanity check level: [{0}] [{1}]", _state.Fan1Rpm, _state.Fan2Present ? _state.Fan2Rpm : "N/A"));
                    }
                }

                if (thresholdsMet)
                {
                    if (_state.EcFanControlEnabled)
                    {
                        _state.EcFanControlEnabled = false;
                        _fanController.DisableAutomaticFanControl();
                        Log.Write("Disabled EC fan control – consistency mode – thresholds met");
                    }

                    if (!_state.ConsistencyModeStatus.StartsWith("Fan speed locked"))
                    {
                        _state.ConsistencyModeStatus = string.Format("Fan speed locked since {0}", DateTime.Now);
                    }

                    _core.TrayIconColor = TrayIconColor.Blue;
                }
                else if (!_state.EcFanControlEnabled && !thresholdsMet)
                {
                    _state.EcFanControlEnabled = true;
                    _fanController.EnableAutomaticFanControl();
                    Log.Write(string.Format("Enabled EC fan control – consistency mode – {0}", _state.ConsistencyModeStatus));

                    _core.TrayIconColor = TrayIconColor.Red;
                }
                else
                {
                    _core.TrayIconColor = TrayIconColor.Red;
                }
            }
        }
    }
}
