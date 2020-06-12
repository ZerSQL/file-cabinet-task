using System;
using System.Collections.Generic;
using System.Text;

namespace FileCabinetApp.Validators
{
    /// <summary>
    /// Класс, производящий валидацию з/п.
    /// </summary>
    public class WageValidator : IRecordValidator
    {
        private decimal min;

        /// <summary>
        /// Initializes a new instance of the <see cref="WageValidator"/> class.
        /// </summary>
        /// <param name="min">Минимальная заработная плата.</param>
        public WageValidator(decimal min)
        {
            this.min = min;
        }

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

            if (newRecord.Wage < this.min)
            {
                throw new ArgumentException($"Wage must be more than {this.min}.");
            }
        }
    }
}
