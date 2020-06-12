using System;
using System.Collections.Generic;
using System.Text;

namespace FileCabinetApp.Validators
{
    /// <summary>
    /// Класс обычной валидации роста.
    /// </summary>
    public class DefaultHeightValidator : IRecordValidator
    {
        private const short MinHeight = 120;
        private const short MaxHeight = 250;

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

            if (newRecord.Height < MinHeight || newRecord.Height > MaxHeight)
            {
                throw new ArgumentException($"Height must be between {MinHeight} and {MaxHeight}.");
            }
        }
    }
}
