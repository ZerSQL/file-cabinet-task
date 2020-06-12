using System;
using System.Collections.Generic;
using System.Text;

namespace FileCabinetApp.Validators
{
    /// <summary>
    /// Класс измененной валидации заработной платы.
    /// </summary>
    public class CustomWageValidator : IRecordValidator
    {
        private const decimal MinWage = 150;

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

            if (newRecord.Wage < MinWage)
            {
                throw new ArgumentException($"Wage must be more than {MinWage}.");
            }
        }
    }
}
