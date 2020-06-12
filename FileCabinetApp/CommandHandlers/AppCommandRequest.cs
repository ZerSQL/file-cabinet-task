using System;
using System.Collections.Generic;
using System.Text;

namespace FileCabinetApp.CommandHandlers
{
    /// <summary>
    /// Запрос.
    /// </summary>
    public class AppCommandRequest
    {
        /// <summary>
        /// Gets or sets command.
        /// </summary>
        /// <value>
        /// Искомая команда.
        /// </value>
        public string Command { get; set; }

        /// <summary>
        /// Gets or sets parameters.
        /// </summary>
        /// <value>
        /// Параметры.
        /// </value>
        public string Parameters { get; set; }
    }
}
