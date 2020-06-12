using System;
using System.Collections.Generic;
using System.Text;

namespace FileCabinetApp.Validators
{
    /// <summary>
    /// Класс валидации фамилии.
    /// </summary>
    public class LastNameValidator : IRecordValidator
    {
        private int min;
        private int max;

        /// <summary>
        /// Initializes a new instance of the <see cref="LastNameValidator"/> class.
        /// </summary>
        /// <param name="min">Минимальная длина.</param>
        /// <param name="max">Максимальная длина.</param>
        public LastNameValidator(int min, int max)
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

            if (newRecord.LastName.Length < this.min || newRecord.LastName.Length > this.max || newRecord.LastName.Contains(' ', StringComparison.CurrentCulture))
            {
                throw new ArgumentException($"Firstname must contain more than {this.min} and less than {this.max} letters.");
            }
        }
    }
}
