using System;
using System.Collections.Generic;
using System.Text;

namespace FileCabinetApp.Validators
{
    /// <summary>
    /// Класс измененной валидации имени.
    /// </summary>
    public class CustomFirstNameValidator : IRecordValidator
    {
        private const int MinLength = 2;
        private const int MaxLength = 15;

        /// <summary>
        /// Метод, производящий валидацию.
        /// </summary>
        /// <param name="newRecord">Запись, подвергаемая валидации.</param>
        public void ValidateParameters(FileCabinetRecord newRecord)
        {
            if (newRecord == null)
            {
                throw new ArgumentNullException($"Record is empty.");
            }

            if (newRecord.FirstName.Length < MinLength || newRecord.FirstName.Length > MaxLength || newRecord.FirstName.Contains(' ', StringComparison.CurrentCulture))
            {
                throw new ArgumentException($"Firstname must contain more than {MinLength} and less than {MaxLength} letters.");
            }
        }
    }
}
