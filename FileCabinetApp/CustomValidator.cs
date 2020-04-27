using System;
using System.Collections.Generic;
using System.Text;

namespace FileCabinetApp
{
    /// <summary>
    /// Класс, представляющий измененный валидатор.
    /// </summary>
    public class CustomValidator : IRecordValidator
    {
        /// <summary>
        /// Метод, выполняющий измененную валидацию.
        /// </summary>
        /// <param name="newRecord">Проверяемая запись.</param>
        public void ValidateParameters(FileCabinetRecord newRecord)
        {
            if (newRecord == null)
            {
                throw new Exception();
            }

            if (newRecord.FirstName == null || newRecord.LastName == null)
            {
                throw new ArgumentNullException($"Name cannot be null. Please, try again. First name is {newRecord.FirstName}, last name is {newRecord.LastName}.");
            }

            if (newRecord.FirstName.Length < 2 || newRecord.FirstName.Length > 15 || newRecord.FirstName.Contains(' ', StringComparison.CurrentCulture) ||
                newRecord.LastName.Length < 2 || newRecord.LastName.Length > 15 || newRecord.LastName.Contains(' ', StringComparison.CurrentCulture) ||
                newRecord.DateOfBirth > DateTime.Now || newRecord.DateOfBirth < new DateTime(1900, 1, 1) ||
                newRecord.Wage < 150 ||
                newRecord.FavouriteNumeral < '0' || newRecord.FavouriteNumeral > '9' || newRecord.FavouriteNumeral == '4' ||
                newRecord.Height < 135 || newRecord.Height > 250)
            {
                throw new ArgumentException("Error input");
            }
        }
    }
}
