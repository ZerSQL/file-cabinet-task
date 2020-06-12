using System;
using System.Collections.Generic;
using System.Text;

namespace FileCabinetApp.CommandHandlers
{
    /// <summary>
    /// Класс, отображающий данные.
    /// </summary>
    public class DefaultRecordPrinter : IRecordPrinter
    {
        /// <summary>
        /// Метод, отображающий данные.
        /// </summary>
        /// <param name="records">Отображаемые записи.</param>
        public void Print(IEnumerable<FileCabinetRecord> records)
        {
            if (records != null)
            {
                foreach (var t in records)
                {
                    Console.WriteLine($"#{t.Id}, {t.FirstName}, {t.LastName}, {t.DateOfBirth.ToShortDateString()}, height: {t.Height}, wage: {t.Wage}, favourite numeral: {t.FavouriteNumeral}");
                }
            }
        }
    }
}
