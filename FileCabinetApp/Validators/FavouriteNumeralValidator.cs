using System;
using System.Collections.Generic;
using System.Text;

namespace FileCabinetApp.Validators
{
    /// <summary>
    /// Класс валидации любимого числа.
    /// </summary>
    public class FavouriteNumeralValidator : IRecordValidator
    {
        private char min;
        private char max;
        private char four;

        /// <summary>
        /// Initializes a new instance of the <see cref="FavouriteNumeralValidator"/> class.
        /// </summary>
        /// <param name="min">Минимальное число.</param>
        /// <param name="max">Максимальное число.</param>
        /// <param name="four">ЧеТыРе.</param>
        public FavouriteNumeralValidator(char min, char max, char four)
        {
            this.min = min;
            this.max = max;
            this.four = four;
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

            if (newRecord.FavouriteNumeral < this.min || newRecord.FavouriteNumeral > this.max || newRecord.FavouriteNumeral == this.four)
            {
                throw new ArgumentException($"Numeral must be between '{this.min}' and '{this.max}' and mustn't be '{this.four}'.");
            }
        }
    }
}
