namespace DellFanManagement.App
{
    /// <summary>
    /// Just some string constants used when dealing with the ConfigurationStore class.
    /// </summary>
    class ConfigurationOption
    {
        /// <summary>
        /// Record whether or not the disclaimer message has been shown.
        /// </summary>
        public static readonly ConfigurationOption DisclaimerShown = new(ConfigurationOptionType.Integer, "DisclaimerShown");

        /// <summary>
        /// Store the "operation mode".
        /// </summary>
        public static readonly ConfigurationOption OperationMode = new(ConfigurationOptionType.String, "OperationMode");

        /// <summary>
        /// Store the state of the "Tray icon" checkbox.
        /// </summary>
        public static readonly ConfigurationOption TrayIconEnabled = new(ConfigurationOptionType.Integer, "TrayIconEnabled");

        /// <summary>
        /// Store the state of the tray icon "Animated" checkbox.
        /// </summary>
        public static readonly ConfigurationOption TrayIconAnimationEnabled = new(ConfigurationOptionType.Integer, "TrayIconAnimationEnabled");

        /// <summary>
        /// Store whether or not EC fan control is turned on in manual mode.
        /// </summary>
        public static readonly ConfigurationOption ManualModeEcFanControlEnabled = new(ConfigurationOptionType.Integer, "ManualModeEcFanControlEnabled");

        /// <summary>
        /// Store the saved level for fan 1 in manual mode.
        /// </summary>
        public static readonly ConfigurationOption ManualModeFan1Level = new(ConfigurationOptionType.String, "ManualModeFan1Level");

        /// <summary>
        /// Store the saved level for fan 2 in manual mode.
        /// </summary>
        public static readonly ConfigurationOption ManualModeFan2Level = new(ConfigurationOptionType.String, "ManualModeFan2Level");

        /// <summary>
        /// Lower temperature threshold for consistency mode.
        /// </summary>
        public static readonly ConfigurationOption ConsistencyModeLowerTemperatureThreshold = new(ConfigurationOptionType.Integer, "ConsistencyModeLowerTemperatureThreshold");

        /// <summary>
        /// Upper temperature threshold for consistency mode.
        /// </summary>
        public static readonly ConfigurationOption ConsistencyModeUpperTemperatureThreshold = new(ConfigurationOptionType.Integer, "ConsistencyModeUpperTemperatureThreshold");

        /// <summary>
        /// RPM threshold for consistency mode.
        /// </summary>
        public static readonly ConfigurationOption ConsistencyModeRpmThreshold = new(ConfigurationOptionType.Integer, "ConsistencyModeRpmThreshold");

        /// <summary>
        /// Store the state of the "Keep this audio device active..." checkbox.
        /// </summary>
        public static readonly ConfigurationOption AudioKeepAliveEnabled = new(ConfigurationOptionType.Integer, "AudioKeepAliveEnabled");

        /// <summary>
        /// Which device is selected from the drop-down for audio keep alive.
        /// </summary>
        public static readonly ConfigurationOption AudioKeepAliveSelectedDevice = new(ConfigurationOptionType.String, "AudioKeepAliveSelectedDevice");

        /// <summary>
        /// If the selected audio device disappears and returns, we want to automatically select it again.
        /// </summary>
        public static readonly ConfigurationOption AudioKeepAliveBringBackDevice = new(ConfigurationOptionType.String, "AudioKeepAliveBringBackDevice");

        /// <summary>
        /// Indicates whether this configuration option is for a "number" or a "string".
        /// </summary>
        public ConfigurationOptionType Type { get; private set; }

        /// <summary>
        /// Key, or basically the name of this configuration option.
        /// </summary>
        public string Key { get; private set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="type">What type of data is going to be stored.</param>
        /// <param name="key">Name of this configuration option.</param>
        private ConfigurationOption(ConfigurationOptionType type, string key)
        {
            Type = type;
            Key = key;
        }
    }
}
