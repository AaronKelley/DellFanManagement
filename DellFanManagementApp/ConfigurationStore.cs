using DellFanManagement.DellSmbiosSmiLib;
using Microsoft.Win32;
using System;
using System.Collections.Generic;

namespace DellFanManagement.App
{
    /// <summary>
    /// Handles storing the options selected in the program in the registry.
    /// </summary>
    class ConfigurationStore
    {
        /// <summary>
        /// A reflection of configuration option values that were present in the registry when the application started,
        /// or that have been set to the registry by the application since then.
        /// </summary>
        private readonly Dictionary<string, object> _options;

        /// <summary>
        /// Prefix for thermal setting override registry values.
        /// </summary>
        private static readonly string ThermalSettingOverridePrefix = "ThermalSetting-";

        /// <summary>
        /// Prefix for power mode override registry values.
        /// </summary>
        private static readonly string PowerModeOverridePrefix = "PowerMode-";

        /// <summary>
        /// Prefix for thermal setting override registry values.
        /// </summary>
        private static readonly string NvPstateOverridePrefix = "NVPState-";

        /// <summary>
        /// List of thermal setting overrides for Windows power profiles.
        /// </summary>
        private readonly Dictionary<Guid, ThermalSetting> _thermalSettingOverrides;

        /// <summary>
        /// List of power mode overrides for Windows power profiles.
        /// </summary>
        private readonly Dictionary<Guid, Guid> _powerModeOverrides;

        /// <summary>
        /// List of NVIDIA GPU P-state overrides for Windows power profiles.
        /// </summary>
        private readonly Dictionary<Guid, int> _nvPstateOverrides;

        /// <summary>
        /// A handle for the registry key that contains the configuration option values.
        /// </summary>
        private readonly RegistryKey _registryKey;

        /// <summary>
        /// Constructor.  Reads current configuration option values from the registry.
        /// </summary>
        public ConfigurationStore()
        {
            _options = new();
            _thermalSettingOverrides = new();
            _powerModeOverrides = new();
            _nvPstateOverrides = new();

            // Set up registry key.
            _registryKey = Registry.CurrentUser.OpenSubKey("Software", true).CreateSubKey("Dell Fan Management");

            foreach (string valueName in _registryKey.GetValueNames())
            {
                if (valueName.StartsWith(ThermalSettingOverridePrefix) || valueName.StartsWith(NvPstateOverridePrefix) || valueName.StartsWith(PowerModeOverridePrefix))
                {
                    string powerProfileString = valueName[(valueName.IndexOf("-") + 1)..];

                    if (Guid.TryParse(powerProfileString, out Guid powerProfile))
                    {
                        if (valueName.StartsWith(ThermalSettingOverridePrefix))
                        {
                            // Thermal setting override.
                            string thermalSettingName = _registryKey.GetValue(valueName).ToString();
                            if (Enum.TryParse(typeof(ThermalSetting), thermalSettingName, out object thermalSetting))
                            {
                                Log.Write(string.Format("Thermal setting override: {0} => {1}", powerProfile, thermalSetting));
                                _thermalSettingOverrides[powerProfile] = (ThermalSetting)thermalSetting;
                            }
                        }
                        else if (valueName.StartsWith(PowerModeOverridePrefix))
                        {
                            // Power mode override.
                            string powerModeGuidString = _registryKey.GetValue(valueName).ToString();
                            if (Guid.TryParse(powerModeGuidString, out Guid powerModeGuid))
                            {
                                Log.Write(string.Format("Power mode override: {0} => {1}", powerProfile, powerModeGuid));
                                _powerModeOverrides[powerProfile] = powerModeGuid;
                            }
                        }
                        else
                        {
                            // NVIDIA P-state override.
                            int pState = int.Parse(_registryKey.GetValue(valueName).ToString());
                            Log.Write(string.Format("NVIDIA P-state override: {0} => {1}", powerProfile, pState));
                            _nvPstateOverrides[powerProfile] = pState;
                        }
                    }
                }
                else
                {
                    _options[valueName] = _registryKey.GetValue(valueName);
                }
            }
        }

        /// <summary>
        /// Stores a configuration option value to the registry.
        /// </summary>
        /// <param name="option">Which specific configuration option to store.</param>
        /// <param name="optionValue">Value of the configuration option to store.</param>
        public void SetOption(ConfigurationOption option, object optionValue)
        {
            _options[option.Key] = optionValue;

            // Write to registry.
            if (optionValue == null)
            {
                // Acutally, null means delete from registry.
                _registryKey.DeleteValue(option.Key, false);
            }
            else if (option.Type == ConfigurationOptionType.Integer && optionValue is int)
            {
                _registryKey.SetValue(option.Key, optionValue, RegistryValueKind.DWord);
            }
            else if (option.Type == ConfigurationOptionType.String)
            {
                _registryKey.SetValue(option.Key, optionValue.ToString(), RegistryValueKind.String);
            }
            else
            {
                throw new ConfigurationStoreException(string.Format("Expected value of type {0} for option key \"{1}\", but received {2} instead", option.Type, option.Key, optionValue.GetType()));
            }
        }

        /// <summary>
        /// Get the value of a string option from the configuration store.
        /// </summary>
        /// <param name="option">Option to retrieve value of.</param>
        /// <returns>Option value (NULL if none set).</returns>
        public string GetStringOption(ConfigurationOption option)
        {
            if (!_options.ContainsKey(option.Key))
            {
                return null;
            }
            else if (_options[option.Key] is string @string)
            {
                return @string;
            }
            else if (_options[option.Key] != null)
            {
                throw new ConfigurationStoreException(string.Format("Requested string option \"{0}\" is not a string", option.Key));
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Get the value of a numeric option from the configuration store.
        /// </summary>
        /// <param name="option">Option to retrieve value of.</param>
        /// <returns>Option value (NULL if none set).</returns>
        public int? GetIntOption(ConfigurationOption option)
        {
            if (!_options.ContainsKey(option.Key))
            {
                return null;
            }
            else if (_options[option.Key] is int @int)
            {
                return @int;
            }
            else if (_options[option.Key] != null)
            {
                throw new ConfigurationStoreException(string.Format("Requested int option \"{0}\" is not an int", option.Key));
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Get the thermal setting override for a given power profile (if one exists).
        /// </summary>
        /// <param name="powerProfile">Windows power profile GUID.</param>
        /// <returns>Thermal setting override for the given power profile; NULL if none.</returns>
        public ThermalSetting? GetThermalSettingOverride(Guid powerProfile)
        {
            if (_thermalSettingOverrides.ContainsKey(powerProfile))
            {
                return _thermalSettingOverrides[powerProfile];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Get the power mode override for a given power profile (if one exists).
        /// </summary>
        /// <param name="powerProfile">Windows power profile GUID.</param>
        /// <returns>Power mode override GUID for the given power profile; NULL if none.</returns>
        public Guid? GetPowerModeOverride(Guid powerProfile)
        {
            if (_powerModeOverrides.ContainsKey(powerProfile))
            {
                return _powerModeOverrides[powerProfile];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Get the NVIDIA P-state override for a given power profile (if one exists).
        /// </summary>
        /// <param name="powerProfile">Windows power profile GUID.</param>
        /// <returns>NVIDIA P-state override for the given power profile; NULL if none.</returns>
        public int? GetNvPstateOverride(Guid powerProfile)
        {
            if (_nvPstateOverrides.ContainsKey(powerProfile))
            {
                return _nvPstateOverrides[powerProfile];
            }
            else
            {
                return null;
            }
        }
    }
}
