using System;
using System.Collections.Generic;
using System.Text;

namespace FileCabinetApp.Validators
{
    /// <summary>
    /// Класс обычной валидации даты рождения.
    /// </summary>
    public class DefaultDateOfBirthValidator : IRecordValidator
    {
        private static readonly DateTime MinDateTime = new DateTime(1950, 1, 1);

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

            if (newRecord.DateOfBirth > DateTime.Now || newRecord.DateOfBirth < MinDateTime)
            {
                throw new ArgumentException($"DateOfBirth must be less than current time and more than {MinDateTime.ToShortDateString()}.");
            }
        }
    }
}
