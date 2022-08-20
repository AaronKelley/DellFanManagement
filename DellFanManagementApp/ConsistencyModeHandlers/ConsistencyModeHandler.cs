using DellFanManagement.App.FanControllers;

namespace DellFanManagement.App.ConsistencyModeHandlers
{
    /// <summary>
    /// Abstract class which allows for different consistency mode implementations.
    /// </summary>
    public abstract class ConsistencyModeHandler
    {
        /// <summary>
        /// Core object.
        /// </summary>
        protected readonly Core _core;

        /// <summary>
        /// State object.
        /// </summary>
        protected readonly State _state;

        /// <summary>
        /// Fan controller.
        /// </summary>
        protected readonly FanController _fanController;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="core">Core object.</param>
        /// <param name="state">State object.</param>
        /// <param name="fanController">The fan controller currently in use.</param>
        public ConsistencyModeHandler(Core core, State state, FanController fanController)
        {
            _core = core;
            _state = state;
            _fanController = fanController;
        }

        /// <summary>
        /// Logic for a particular consistency mode implementation.
        /// </summary>
        public abstract void RunConsistencyModeLogic();
    }
}
