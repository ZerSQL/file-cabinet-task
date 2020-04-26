using System;
using System.Collections.Generic;
using System.Text;

namespace FileCabinetApp
{
    /// <summary>
    /// Обычная валидация.
    /// </summary>
    public class FileCabinetDefaultService : FileCabinetService
    {
        /// <summary>
        /// Метод выполняющий валидацию.
        /// </summary>
        /// <param name="newRecord">Проверяемая запись.</param>
        public override void ValidateParameters(FileCabinetRecord newRecord)
        {
            if (newRecord == null)
            {
                throw new Exception();
            }

            if (newRecord.FirstName == null || newRecord.LastName == null)
            {
                throw new ArgumentNullException($"Name cannot be null. Please, try again. First name is {newRecord.FirstName}, last name is {newRecord.LastName}.");
            }

            if (newRecord.FirstName.Length < 2 || newRecord.FirstName.Length > 60 || newRecord.FirstName.Contains(' ', StringComparison.CurrentCulture) ||
                newRecord.LastName.Length < 2 || newRecord.LastName.Length > 60 || newRecord.LastName.Contains(' ', StringComparison.CurrentCulture) ||
                newRecord.DateOfBirth > DateTime.Now || newRecord.DateOfBirth < new DateTime(1950, 1, 1) ||
                newRecord.Wage < 300 ||
                newRecord.FavouriteNumeral < '0' || newRecord.FavouriteNumeral > '9')
            {
                throw new ArgumentException("Error input");
            }
        }
    }
}
