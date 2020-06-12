using System;
using System.Collections.Generic;
using System.Text;

namespace FileCabinetApp.CommandHandlers
{
    /// <summary>
    /// Класс, представляющий обработчик команды редактирования.
    /// </summary>
    public class EditCommandHandler : ServiceCommandHandlerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EditCommandHandler"/> class.
        /// </summary>
        /// <param name="service">Используемый сервис.</param>
        public EditCommandHandler(IFileCabinetService service)
            : base(service)
        {
        }

        /// <summary>
        /// Обработчкик команды редактирования.
        /// </summary>
        /// <param name="request">Запрос.</param>
        public override void Handle(AppCommandRequest request)
        {
            if (request != null)
            {
                if (request.Command.Equals("Edit", comparisonType: StringComparison.InvariantCultureIgnoreCase))
                {
                    this.Edit(request.Parameters);
                }
                else
                {
                    this.nextHandler.Handle(request);
                }
            }
        }

        private void Edit(string parameters)
        {
            bool exact;
            int id;
            while (true)
            {
                Console.WriteLine("Input id of note to be changed or '0' to go to menu");
                exact = int.TryParse(Console.ReadLine(), out id);
                if (exact)
                {
                    if (this.service.GetStat() >= id)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"#{id} record is not found.");
                    }
                }
            }

            if (id == 0)
            {
                return;
            }

            Program.CreateOrEditCommands(out string firstName, out string lastName, out DateTime dateOfBirth, out decimal personalWage, out char favouriteNumeral, out short personalHeight);
            FileCabinetRecord record = new FileCabinetRecord() { Id = id, FirstName = firstName, LastName = lastName, DateOfBirth = dateOfBirth, Wage = personalWage, FavouriteNumeral = favouriteNumeral, Height = personalHeight };
            this.service.EditRecord(record);
        }
    }
}
