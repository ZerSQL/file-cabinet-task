using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace FileCabinetApp
{
    /// <summary>
    /// Класс производящий считывание xml.
    /// </summary>
    public class FileCabinetRecordXmlReader
    {
        private StreamReader reader;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileCabinetRecordXmlReader"/> class.
        /// </summary>
        /// <param name="reader">Reader.</param>
        public FileCabinetRecordXmlReader(StreamReader reader)
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
            XmlSerializer formatter = new XmlSerializer(typeof(FileCabinetRecord[]), new XmlRootAttribute("FileCabinetRecords"));
            using (this.reader)
            {
                FileCabinetRecord[] recs;
                using (XmlReader xmlReader = XmlReader.Create(this.reader))
                {
                    recs = (FileCabinetRecord[])formatter.Deserialize(xmlReader);
                }

                records = recs.ToList();

                // recs = (FileCabinetRecord[])formatter.Deserialize(this.reader);
            }

            return records;
        }
    }
}
