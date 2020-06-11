using System;
using System.Collections.Generic;
using System.Text;

namespace FileCabinetApp.CommandHandlers
{
    /// <summary>
    /// Обработка команды создания.
    /// </summary>
    public class CreateCommandHandler : CommandHandlerBase
    {
        /// <summary>
        /// Обработчкик команды создания.
        /// </summary>
        /// <param name="request">Запрос.</param>
        public override void Handle(AppCommandRequest request)
        {
            if (request != null)
            {
                if (request.Command.Equals("Create", comparisonType: StringComparison.InvariantCultureIgnoreCase))
                {
                    Create(request.Parameters);
                }
                else
                {
                    this.nextHandler.Handle(request);
                }
            }
        }

        private static void Create(string parameters)
        {
            Program.CreateOrEditCommands(out string firstName, out string lastName, out DateTime dateOfBirth, out decimal personalWage, out char favouriteNumeral, out short personalHeight);
            FileCabinetRecord newRecord = new FileCabinetRecord() { FirstName = firstName, LastName = lastName, DateOfBirth = dateOfBirth, Wage = personalWage, FavouriteNumeral = favouriteNumeral, Height = personalHeight };
            Program.fileCabinetService.CreateRecord(newRecord);
        }
    }
}
