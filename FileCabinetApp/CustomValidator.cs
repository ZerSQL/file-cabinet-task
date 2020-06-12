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
        /// <summary>
        /// Метод, выполняющий измененную валидацию.
        /// </summary>
        /// <param name="newRecord">Проверяемая запись.</param>
        public void ValidateParameters(FileCabinetRecord newRecord)
        {
            new CustomFirstNameValidator().ValidateParameters(newRecord);
            new CustomLastNameValidator().ValidateParameters(newRecord);
            new CustomDateOfBirthValidator().ValidateParameters(newRecord);
            new CustomWageValidator().ValidateParameters(newRecord);
            new CustomFavouriteNumeralValidator().ValidateParameters(newRecord);
            new CustomHeightValidator().ValidateParameters(newRecord);
        }
    }
}
