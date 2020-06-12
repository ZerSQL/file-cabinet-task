using System;
using System.Collections.Generic;
using System.Text;

namespace FileCabinetApp.CommandHandlers
{
    /// <summary>
    /// Интерфейс для реализации стратегии.
    /// </summary>
    public interface IRecordPrinter
    {
        /// <summary>
        /// Метод для вывода записей.
        /// </summary>
        /// <param name="records">Записи.</param>
        void Print(IEnumerable<FileCabinetRecord> records);
    }
}
