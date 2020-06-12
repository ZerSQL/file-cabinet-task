using System;
using System.Collections.Generic;
using System.Text;

namespace FileCabinetApp.Validators
{
    /// <summary>
    /// Класс валидации имени.
    /// </summary>
    public class FirstNameValidator : IRecordValidator
    {
        private int min;
        private int max;

        /// <summary>
        /// Initializes a new instance of the <see cref="FirstNameValidator"/> class.
        /// </summary>
        /// <param name="min">Минимальная длина.</param>
        /// <param name="max">Максимальная длина.</param>
        public FirstNameValidator(int min, int max)
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

            if (newRecord.FirstName.Length < this.min || newRecord.FirstName.Length > this.max || newRecord.FirstName.Contains(' ', StringComparison.CurrentCulture))
            {
                throw new ArgumentException($"Firstname must contain more than {this.min} and less than {this.max} letters.");
            }
        }
    }
}
