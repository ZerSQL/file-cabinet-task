using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace FileCabinetApp
{
    /// <summary>
    /// Класс производящий считывание Csv.
    /// </summary>
    public class FileCabinetRecordCsvReader
    {
        private StreamReader reader;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileCabinetRecordCsvReader"/> class.
        /// </summary>
        /// <param name="reader">Reader.</param>
        public FileCabinetRecordCsvReader(StreamReader reader)
        {
            this.reader = reader;
        }

        /// <summary>
        /// Получение всех импортируемых записей.
        /// </summary>
        /// <returns>Список импортированных записей.</returns>
        public IList<FileCabinetRecord> ReadAll()
        {
            List<FileCabinetRecord> records = new List<FileCabinetRecord>();
            using (this.reader)
            {
                var line = this.reader.ReadLine();
                while (this.reader.EndOfStream != true)
                {
                    FileCabinetRecord record = new FileCabinetRecord();
                    line = this.reader.ReadLine();
                    var cells = line.Split(',');
                    record.Id = int.Parse(cells[0], CultureInfo.InvariantCulture);
                    record.FirstName = cells[1];
                    record.LastName = cells[2];
                    record.DateOfBirth = DateTime.Parse(cells[3], CultureInfo.CurrentCulture);
                    record.Wage = decimal.Parse(cells[4], CultureInfo.InvariantCulture);
                    record.Height = short.Parse(cells[5], CultureInfo.InvariantCulture);
                    record.FavouriteNumeral = char.Parse(cells[6]);
                    records.Add(record);
                }
            }

            return records;
        }
    }
}
