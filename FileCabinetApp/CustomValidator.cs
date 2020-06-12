using System;
using System.Collections.Generic;
using System.Text;
using FileCabinetApp.Validators;

namespace FileCabinetApp
{
    /// <summary>
    /// Класс, представляющий измененный валидатор.
    /// </summary>
    public class CustomValidator : IRecordValidator
    {
        private const int MinLength = 2;
        private const int MaxLength = 15;
        private const short MinHeight = 135;
        private const short MaxHeight = 250;
        private const char MinNum = '0';
        private const char MaxNum = '9';
        private const char Four = '4';
        private const decimal MinWage = 150;
        private static readonly DateTime From = new DateTime(1900, 1, 1);
        private static readonly DateTime To = DateTime.Now;

        /// <summary>
        /// Метод, выполняющий измененную валидацию.
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
