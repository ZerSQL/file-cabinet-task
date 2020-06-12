using System;
using System.Collections.Generic;
using System.Text;

namespace FileCabinetApp.Validators
{
    /// <summary>
    /// Класс обычной валидации фамилии.
    /// </summary>
    public class DefaultLastNameValidator : IRecordValidator
    {
        private const int MinLength = 2;
        private const int MaxLength = 60;

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

            if (newRecord.LastName.Length < MinLength || newRecord.LastName.Length > MaxLength || newRecord.LastName.Contains(' ', StringComparison.CurrentCulture))
            {
                throw new ArgumentException($"Lastname must contain more than {MinLength} and less than {MaxLength} letters.");
            }
        }
    }
}
