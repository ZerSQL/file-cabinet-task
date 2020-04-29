using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileCabinetApp
{
    /// <summary>
    /// Класс для выполнения экспорта списка в csv.
    /// </summary>
    public class FileCabinetRecordCsvWriter
    {
        private StreamWriter writer;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileCabinetRecordCsvWriter"/> class.
        /// </summary>
        /// <param name="textWriter">StreamWriter.</param>
        public FileCabinetRecordCsvWriter(StreamWriter textWriter)
        {
            this.writer = textWriter;
        }

        /// <summary>
        /// Метод выполняющий запись в csv.
        /// </summary>
        /// <param name="records">Массив записей.</param>
        public void Write(FileCabinetRecord[] records)
        {
            using (this.writer)
            {
                this.writer.WriteLine("Id, First Name, Last Name, Date of Birth, Wage, Height, Favourite numeral");
                if (records != null)
                {
                    foreach (FileCabinetRecord record in records)
                    {
                        this.writer.Write($"{record.Id},{record.FirstName},{record.LastName},{record.DateOfBirth.ToShortDateString()},{record.Wage},{record.Height},{record.FavouriteNumeral}");
                        this.writer.WriteLine();
                    }
                }
            }
        }
    }
}
