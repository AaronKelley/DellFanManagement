using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DellFanManagement.App
{
    /// <summary>
    /// Handles storing the options selected in the program in the registry.
    /// </summary>
    class ConfigurationStore
    {
        private readonly Dictionary<string, object> _options;

        private readonly RegistryKey _registryKey;

        public ConfigurationStore()
        {
            _options = new();

            // Set up registry key.
            _registryKey = Registry.CurrentUser.OpenSubKey("Software", true).CreateSubKey("Dell Fan Management");

            foreach (string valueName in _registryKey.GetValueNames())
            {
                _options[valueName] = _registryKey.GetValue(valueName);
            }
        }

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
            if (!_options.Keys.Contains(option.Key))
            {
                return null;
            }
            else if (_options[option.Key] is string)
            {
                return (string)_options[option.Key];
            }
            else
            {
                throw new ConfigurationStoreException(string.Format("Requested string option \"{0}\" is not a string", option.Key));
            }
        }

        /// <summary>
        /// Get the value of a numeric option from the configuration store.
        /// </summary>
        /// <param name="option">Option to retrieve value of.</param>
        /// <returns>Option value (NULL if none set).</returns>
        public int? GetIntOption(ConfigurationOption option)
        {
            if (!_options.Keys.Contains(option.Key))
            {
                return null;
            }
            else if (_options[option.Key] is int)
            {
                return (int)_options[option.Key];
            }
            else
            {
                throw new ConfigurationStoreException(string.Format("Requested int option \"{0}\" is not an int", option.Key));
            }
        }
    }
}
