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
        private Action<IEnumerable<FileCabinetRecord>> printer;

        /// <summary>
        /// Initializes a new instance of the <see cref="FindCommandHandler"/> class.
        /// </summary>
        /// <param name="service">Используемый сервсис.</param>
        /// <param name="printer">Используемый метод отображения.</param>
        public FindCommandHandler(IFileCabinetService service, Action<IEnumerable<FileCabinetRecord>> printer)
            : base(service)
        {
            this.printer = printer;
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
                this.printer(this.service.FindByFirstName(propertyAndValue[1]));
            }
            else if (propertyAndValue[0].ToLower(CultureInfo.CurrentCulture) == "lastname")
            {
                this.printer(this.service.FindByLastName(propertyAndValue[1]));
            }
            else if (propertyAndValue[0].ToLower(CultureInfo.CurrentCulture) == "dateofbirth")
            {
                this.printer(this.service.FindByBirthDate(propertyAndValue[1]));
            }
            else
            {
                Console.WriteLine("'Find' command working in 'find *name of property* *value*' format. For birthdate use dd.MM.yyyy format.Please,try again.");
            }
        }
    }
}
