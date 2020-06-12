using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace FileCabinetApp.CommandHandlers
{
    /// <summary>
    /// Класс, представляющий обработчик команды поиска.
    /// </summary>
    public class FindCommandHandler : ServiceCommandHandlerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FindCommandHandler"/> class.
        /// </summary>
        /// <param name="service">Используемый сервсис.</param>
        public FindCommandHandler(IFileCabinetService service)
            : base(service)
        {
        }

        /// <summary>
        /// Обработчкик команды поиска.
        /// </summary>
        /// <param name="request">Запрос.</param>
        public override void Handle(AppCommandRequest request)
        {
            if (request != null)
            {
                if (request.Command.Equals("Find", comparisonType: StringComparison.InvariantCultureIgnoreCase))
                {
                    this.Find(request.Parameters);
                }
                else
                {
                    this.nextHandler.Handle(request);
                }
            }
        }

        private void Find(string parameters)
        {
            string[] propertyAndValue = parameters.Replace("\"", string.Empty, StringComparison.CurrentCultureIgnoreCase).Split(' ', 2);
            if (propertyAndValue.Length != 2)
            {
                Console.WriteLine("'Find' command working in 'find *name of property* *value*' format. For birthdate use dd.MM.yyyy format.Please,try again.");
                return;
            }

            if (propertyAndValue[0].ToLower(CultureInfo.CurrentCulture) == "firstname")
            {
                foreach (var note in this.service.FindByFirstName(propertyAndValue[1]))
                {
                    Console.WriteLine($"#{note.Id}, {note.FirstName}, {note.LastName}, {note.DateOfBirth.ToShortDateString()}");
                }
            }
            else if (propertyAndValue[0].ToLower(CultureInfo.CurrentCulture) == "lastname")
            {
                foreach (var note in this.service.FindByLastName(propertyAndValue[1]))
                {
                    Console.WriteLine($"#{note.Id}, {note.FirstName}, {note.LastName}, {note.DateOfBirth.ToShortDateString()}");
                }
            }
            else if (propertyAndValue[0].ToLower(CultureInfo.CurrentCulture) == "dateofbirth")
            {
                foreach (var note in this.service.FindByBirthDate(propertyAndValue[1]))
                {
                    Console.WriteLine($"#{note.Id}, {note.FirstName}, {note.LastName}, {note.DateOfBirth.ToShortDateString()}");
                }
            }
            else
            {
                Console.WriteLine("'Find' command working in 'find *name of property* *value*' format. For birthdate use dd.MM.yyyy format.Please,try again.");
            }
        }
    }
}
