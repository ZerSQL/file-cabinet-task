using System;
using System.Collections.Generic;
using System.Text;
using FileCabinetApp.Validators;

namespace FileCabinetApp
{
    /// <summary>
    /// Класс, представляющий обычный валидатор.
    /// </summary>
    public class DefaultValidator : IRecordValidator
    {
        private const int MinLength = 2;
        private const int MaxLength = 60;
        private const short MinHeight = 120;
        private const short MaxHeight = 250;
        private const char MinNum = '0';
        private const char MaxNum = '9';
        private const char Four = ' ';
        private const decimal MinWage = 300;
        private static readonly DateTime From = new DateTime(1950, 1, 1);
        private static readonly DateTime To = DateTime.Now;

        /// <summary>
        /// Метод, выполняющий обычную валидацию.
        /// </summary>
        /// <param name="newRecord">Проверяемая запись.</param>
        public void ValidateParameters(FileCabinetRecord newRecord)
        {
            new FirstNameValidator(MinLength, MaxLength).ValidateParameters(newRecord);
            new LastNameValidator(MinLength, MaxLength).ValidateParameters(newRecord);
            new DateOfBirthValidator(From, To).ValidateParameters(newRecord);
            new WageValidator(MinWage).ValidateParameters(newRecord);
            new FavouriteNumeralValidator(MinNum, MaxNum, Four).ValidateParameters(newRecord);
            new HeightValidator(MinHeight, MaxHeight).ValidateParameters(newRecord);
        }
    }
}
