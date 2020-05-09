using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Xml;

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
        /// Gets notes from csv.
        /// </summary>
        /// <value>
        /// Notes.
        /// </value>
        public ReadOnlyCollection<FileCabinetRecord> Records => Array.AsReadOnly<FileCabinetRecord>(this.records);

        /// <summary>
        /// Метод записи в csv.
        /// </summary>
        /// <param name="streamWriter">StreamWriter.</param>
        public void SaveToCsv(StreamWriter streamWriter)
        {
            FileCabinetRecordCsvWriter fileCabinetRecordCsvWriter = new FileCabinetRecordCsvWriter(streamWriter);
            fileCabinetRecordCsvWriter.Write(this.records);
        }

        /// <summary>
        /// Метод записи в xml.
        /// </summary>
        /// <param name="xmlWriter">StreamWriter.</param>
        public void SaveToXml(XmlWriter xmlWriter)
        {
            FileCabinetRecordXmlWriter fileCabinetRecordXmlWriter = new FileCabinetRecordXmlWriter(xmlWriter);
            fileCabinetRecordXmlWriter.Write(this.records);
        }

        /// <summary>
        /// Метод выгрузки из csv.
        /// </summary>
        /// <param name="fs">Filestream.</param>
        public void LoadFromCsv(FileStream fs)
        {
            StreamReader reader = new StreamReader(fs);
            FileCabinetRecordCsvReader csvReader = new FileCabinetRecordCsvReader(reader);
            List<FileCabinetRecord> list = new List<FileCabinetRecord>(csvReader.ReadAll());
            this.records = list.ToArray();
        }
    }
}
