using System;

namespace DellFanManagement.App
{
    /// <summary>
    /// Just some string constants used when dealing with the ConfigurationStore class.
    /// </summary>
    class ConfigurationOption
    {
        public static readonly ConfigurationOption TrayIconEnabled = new(ConfigurationOptionType.Integer, "TrayIconEnabled");

        public ConfigurationOptionType Type { get; private set; }
        public string Key { get; private set; }

        private ConfigurationOption(ConfigurationOptionType type, string key)
        {
            Type = type;
            Key = key;
        }
    }
}
