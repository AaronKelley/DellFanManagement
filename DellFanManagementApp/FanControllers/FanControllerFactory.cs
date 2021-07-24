using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DellFanManagement.App.FanControllers
{
    /// <summary>
    /// Determine system capabilities and select an appropriate fan speed controller to use.
    /// </summary>
    class FanControllerFactory
    {
        /// <summary>
        /// Selects a fan speed reader and returns it.
        /// </summary>
        /// <returns>Fan speed reader appropriate for the system.</returns>
        public static FanController GetFanFanController()
        {
            // Right now, we just have one choice.
            return new BzhFanController();
        }
    }
}
