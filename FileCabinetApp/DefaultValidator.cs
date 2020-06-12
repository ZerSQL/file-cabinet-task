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
        /// <summary>
        /// Метод, выполняющий обычную валидацию.
        /// </summary>
        /// <param name="newRecord">Проверяемая запись.</param>
        public void ValidateParameters(FileCabinetRecord newRecord)
        {
            new DefaultFirstNameValidator().ValidateParameters(newRecord);
            new DefaultLastNameValidator().ValidateParameters(newRecord);
            new DefaultDateOfBirthValidator().ValidateParameters(newRecord);
            new DefaultWageValidator().ValidateParameters(newRecord);
            new DefaultFavouriteNumeralValidator().ValidateParameters(newRecord);
            new DefaultHeightValidator().ValidateParameters(newRecord);
        }
    }
}
