using System;
using System.Collections.Generic;
using System.Text;

namespace FileCabinetApp.Validators
{
    /// <summary>
    /// Класс валидации роста.
    /// </summary>
    public class HeightValidator : IRecordValidator
    {
        private short min;
        private short max;

        /// <summary>
        /// Initializes a new instance of the <see cref="HeightValidator"/> class.
        /// </summary>
        /// <param name="min">Минимальный рост.</param>
        /// <param name="max">Максимальный рост.</param>
        public HeightValidator(short min, short max)
        {
            this.min = min;
            this.max = max;
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

            if (newRecord.Height < this.min || newRecord.Height > this.max)
            {
                throw new ArgumentException($"Height must be between {this.min} and {this.max}.");
            }
        }
    }
}
