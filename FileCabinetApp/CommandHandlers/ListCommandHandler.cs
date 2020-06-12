using System;
using System.Collections.Generic;
using System.Text;

namespace FileCabinetApp.CommandHandlers
{
    /// <summary>
    /// Класс, представляющий обработик команды вывода записей.
    /// </summary>
    public class ListCommandHandler : ServiceCommandHandlerBase
    {
        private IRecordPrinter printer;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListCommandHandler"/> class.
        /// </summary>
        /// <param name="service">Используемый сервис.</param>
        /// <param name="printer">Используемый метод отображения.</param>
        public ListCommandHandler(IFileCabinetService service, IRecordPrinter printer)
            : base(service)
        {
            this.printer = printer;
        }

        /// <summary>
        /// Обработчкик команды вывода записей.
        /// </summary>
        /// <param name="request">Запрос.</param>
        public override void Handle(AppCommandRequest request)
        {
            if (request != null)
            {
                if (request.Command.Equals("List", comparisonType: StringComparison.InvariantCultureIgnoreCase))
                {
                    this.List(request.Parameters);
                }
                else
                {
                    this.nextHandler.Handle(request);
                }
            }
        }

        private void List(string parameters)
        {
            if (this.service.GetRecords().Count > 0)
            {
                this.printer.Print(this.service.GetRecords());
            }
            else
            {
                Console.WriteLine("No any notes");
            }
        }
    }
}
