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
        /// <summary>
        /// Initializes a new instance of the <see cref="ListCommandHandler"/> class.
        /// </summary>
        /// <param name="service">Используемый сервис.</param>
        public ListCommandHandler(IFileCabinetService service)
            : base(service)
        {
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
                foreach (var t in this.service.GetRecords())
                {
                    Console.WriteLine($"#{t.Id}, {t.FirstName}, {t.LastName}, {t.DateOfBirth.ToShortDateString()}, height: {t.Height}, wage: {t.Wage}, favourite numeral: {t.FavouriteNumeral}");
                }
            }
            else
            {
                Console.WriteLine("No any notes");
            }
        }
    }
}
