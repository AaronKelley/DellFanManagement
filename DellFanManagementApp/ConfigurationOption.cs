using System;

namespace DellFanManagement.App
{
    /// <summary>
    /// Just some string constants used when dealing with the ConfigurationStore class.
    /// </summary>
    class ConfigurationOption
    {
        /// <summary>
        /// Store the state of the "Tray icon" checkbox.
        /// </summary>
        public static readonly ConfigurationOption TrayIconEnabled = new(ConfigurationOptionType.Integer, "TrayIconEnabled");

        /// <summary>
        /// Store the state of the tray icon "Animated" checkbox.
        /// </summary>
        public static readonly ConfigurationOption TrayIconAnimationEnabled = new(ConfigurationOptionType.Integer, "TrayIconAnimationEnabled");

        public ConfigurationOptionType Type { get; private set; }
        public string Key { get; private set; }

        private ConfigurationOption(ConfigurationOptionType type, string key)
        {
            Type = type;
            Key = key;
        }
    }
}
