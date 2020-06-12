using System;
using System.Collections.Generic;
using System.Text;

namespace FileCabinetApp.CommandHandlers
{
    /// <summary>
    /// Суперкласс с сервисом.
    /// </summary>
    public class ServiceCommandHandlerBase : CommandHandlerBase
    {
        /// <summary>
        /// Используемый сервис.
        /// </summary>
        protected IFileCabinetService service;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceCommandHandlerBase"/> class.
        /// </summary>
        /// <param name="service">Используемый сервис.</param>
        public ServiceCommandHandlerBase(IFileCabinetService service)
        {
            this.service = service;
        }
    }
}
