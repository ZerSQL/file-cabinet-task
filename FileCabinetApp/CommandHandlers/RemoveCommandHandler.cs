using System;
using System.Collections.Generic;
using System.Text;

namespace FileCabinetApp.CommandHandlers
{
    /// <summary>
    /// Класс, представляющий команду удаления записи.
    /// </summary>
    public class RemoveCommandHandler : ServiceCommandHandlerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RemoveCommandHandler"/> class.
        /// </summary>
        /// <param name="service">Используемый сервис.</param>
        public RemoveCommandHandler(IFileCabinetService service)
            : base(service)
        {
        }

        /// <summary>
        /// Обработчкик команды удаления записи.
        /// </summary>
        /// <param name="request">Запрос.</param>
        public override void Handle(AppCommandRequest request)
        {
            if (request != null)
            {
                if (request.Command.Equals("Remove", comparisonType: StringComparison.InvariantCultureIgnoreCase))
                {
                    this.Remove(request.Parameters);
                }
                else
                {
                    this.nextHandler.Handle(request);
                }
            }
        }

        private void Remove(string parameters)
        {
            if (parameters.Split(' ').Length > 1)
            {
                Console.WriteLine("Error input.");
                return;
            }

            string[] values = parameters.Split(' ', 1);
            if (int.TryParse(values[0], out int number))
            {
                this.service.Remove(number);
            }
            else
            {
                Console.WriteLine("Error input");
            }
        }
    }
}
