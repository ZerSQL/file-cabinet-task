using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

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
                    this.writer.WriteStartElement("records");

                    foreach (FileCabinetRecord record in records)
                    {
                        this.writer.WriteStartElement("record");
                        this.writer.WriteAttributeString("id", $"{record.Id}");
                        this.writer.WriteElementString($"firstname", $"{record.FirstName}");
                        this.writer.WriteElementString($"lastname", $"{record.LastName}");
                        this.writer.WriteElementString($"dateOfBirth", $"{record.DateOfBirth.ToShortDateString()}");
                        this.writer.WriteElementString($"wage", $"{record.Wage}");
                        this.writer.WriteElementString($"height", $"{record.Height}");
                        this.writer.WriteElementString($"fav.numeral", $"{record.FavouriteNumeral}");
                        this.writer.WriteEndElement();
                    }
                }
            }
        }
    }
}
