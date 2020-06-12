using System;
using System.Collections.Generic;
using System.Text;

namespace FileCabinetApp.Validators
{
    /// <summary>
    /// Класс обычной валидации любимого числа.
    /// </summary>
    public class DefaultFavouriteNumeralValidator : IRecordValidator
    {
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

            if (newRecord.FavouriteNumeral < '0' || newRecord.FavouriteNumeral > '9')
            {
                throw new ArgumentException($"Numeral must be between '0' and '9'.");
            }
        }
    }
}
