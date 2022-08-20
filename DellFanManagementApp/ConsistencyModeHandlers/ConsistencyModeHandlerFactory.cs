using DellFanManagement.App.FanControllers;

namespace DellFanManagement.App.ConsistencyModeHandlers
{
    /// <summary>
    /// Select an appropriate consistency mode handler.
    /// </summary>
    public static class ConsistencyModeHandlerFactory
    {
        /// <summary>
        /// Select an appropriate consistency mode handler
        /// </summary>
        /// <param name="core">Core object.</param>
        /// <param name="state">State object.</param>
        /// <param name="fanController">The fan controller currently in use.</param>
        /// <returns>A consistency mode handler that can be used on this system.</returns>
        public static ConsistencyModeHandler GetConsistencyModeHandler(Core core, State state, FanController fanController)
        {
            ConsistencyModeHandler handler;
            if (fanController.IsAutomaticFanControlDisableSupported)
            {
                handler = new LegacyConsistencyModeHandler(core, state, fanController);
            }
            else
            {
                handler = new SimpleConsistencyModeHandler(core, state, fanController);
            }

            Log.Write(string.Format("Selected consistency mode handler: {0}", handler.GetType()));
            return handler;
        }
    }
}
