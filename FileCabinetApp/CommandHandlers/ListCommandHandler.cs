using System;
using System.Collections.Generic;
using System.Text;

namespace FileCabinetApp.CommandHandlers
{
    /// <summary>
    /// Класс, представляющий обработик команды вывода записей.
    /// </summary>
    public class ListCommandHandler : CommandHandlerBase
    {
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
                    List(request.Parameters);
                }
                else
                {
                    this.nextHandler.Handle(request);
                }
            }
        }

        private static void List(string parameters)
        {
            if (Program.fileCabinetService.GetRecords().Count > 0)
            {
                foreach (var t in Program.fileCabinetService.GetRecords())
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
