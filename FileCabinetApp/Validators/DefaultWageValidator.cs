using System;
using System.Collections.Generic;
using System.Text;

namespace FileCabinetApp.Validators
{
    /// <summary>
    /// Класс обычной валидации заработной платы.
    /// </summary>
    public class DefaultWageValidator : IRecordValidator
    {
        private const decimal MinWage = 300;

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
