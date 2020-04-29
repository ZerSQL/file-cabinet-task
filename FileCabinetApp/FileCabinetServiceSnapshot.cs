using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileCabinetApp
{
    /// <summary>
    /// Класс для сохранения текущего списка для последующего экспорта.
    /// </summary>
    public class FileCabinetServiceSnapshot
    {
        private FileCabinetRecord[] records;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileCabinetServiceSnapshot"/> class.
        /// </summary>
        /// <param name="list">Список записей.</param>
        public FileCabinetServiceSnapshot(List<FileCabinetRecord> list)
        {
            if (list == null)
            {
                this.records = Array.Empty<FileCabinetRecord>();
            }
            else
            {
                this.records = list.ToArray();
            }
        }

        /// <summary>
        /// Метод записи в csv.
        /// </summary>
        /// <param name="streamWriter">StreamWriter.</param>
        public void SaveToCsv(StreamWriter streamWriter)
        {
            FileCabinetRecordCsvWriter fileCabinetRecordCsvWriter = new FileCabinetRecordCsvWriter(streamWriter);
            fileCabinetRecordCsvWriter.Write(this.records);
        }
    }
}
