using System;
using System.Collections.Generic;
using System.Text;

namespace FileCabinetApp
{
    /// <summary>
    /// Интерфейс для валидации записей.
    /// </summary>
    public interface IRecordValidator
    {
        /// <summary>
        /// Метод, выполняющий обычную валидацию.
        /// </summary>
        /// /// <param name="newRecord">Проверяемая запись.</param>
        void ValidateParameters(FileCabinetRecord newRecord);
    }
}
