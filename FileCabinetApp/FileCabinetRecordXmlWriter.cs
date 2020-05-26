using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace FileCabinetApp
{
    /// <summary>
    /// Класс для выполнения экспорта списка в xml.
    /// </summary>
    public class FileCabinetRecordXmlWriter
    {
        private XmlWriter writer;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileCabinetRecordXmlWriter"/> class.
        /// </summary>
        /// <param name="textWriter">StreamWriter.</param>
        public FileCabinetRecordXmlWriter(XmlWriter textWriter)
        {
            this.writer = textWriter;
        }

        /// <summary>
        /// Метод выполняющий запись в xml.
        /// </summary>
        /// <param name="records">Массив записей.</param>
        public void Write(FileCabinetRecord[] records)
        {
            using (this.writer)
            {
                if (records != null)
                {
                    XmlSerializer formatter = new XmlSerializer(typeof(FileCabinetRecord[]), new XmlRootAttribute("FileCabinetRecords"));
                    formatter.Serialize(this.writer, records);
                }
            }
        }
    }
}
