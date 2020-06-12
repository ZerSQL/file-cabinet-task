using System;
using System.Collections.Generic;
using System.Text;

namespace FileCabinetApp.CommandHandlers
{
    /// <summary>
    /// Класс, представляющий команду вывода количества записей.
    /// </summary>
    public class StatCommandHandler : ServiceCommandHandlerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StatCommandHandler"/> class.
        /// </summary>
        /// <param name="service">Используемый сервис.</param>
        public StatCommandHandler(IFileCabinetService service)
            : base(service)
        {
        }

        /// <summary>
        /// Обработчкик команды вывода количества записей.
        /// </summary>
        /// <param name="request">Запрос.</param>
        public override void Handle(AppCommandRequest request)
        {
            if (request != null)
            {
                if (request.Command.Equals("Stat", comparisonType: StringComparison.InvariantCultureIgnoreCase))
                {
                    this.Stat(request.Parameters);
                }
                else
                {
                    this.nextHandler.Handle(request);
                }
            }
        }

        private void Stat(string parameters)
        {
            var recordsCount = this.service.GetStat();
            Console.WriteLine($"{recordsCount} record(s).");
        }
    }
}
