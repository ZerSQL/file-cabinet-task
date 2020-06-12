using System;
using System.Collections.Generic;
using System.Text;

namespace FileCabinetApp.CommandHandlers
{
    /// <summary>
    /// Класс, представляющий обработчик команды удаления пустот.
    /// </summary>
    public class PurgeCommandHandler : ServiceCommandHandlerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PurgeCommandHandler"/> class.
        /// </summary>
        /// <param name="service">Используемый сервис.</param>
        public PurgeCommandHandler(IFileCabinetService service)
            : base(service)
        {
        }

        /// <summary>
        /// Обработчкик команды удаления пустот.
        /// </summary>
        /// <param name="request">Запрос.</param>
        public override void Handle(AppCommandRequest request)
        {
            if (request != null)
            {
                if (request.Command.Equals("Purge", comparisonType: StringComparison.InvariantCultureIgnoreCase))
                {
                    this.Purge(request.Parameters);
                }
                else
                {
                    this.nextHandler.Handle(request);
                }
            }
        }

        private void Purge(string parameters)
        {
            this.service.Purge();
        }
    }
}
