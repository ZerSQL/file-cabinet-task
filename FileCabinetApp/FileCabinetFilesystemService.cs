using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Text;

namespace FileCabinetApp
{
    /// <summary>
    /// Класс для создания файлов данных.
    /// </summary>
    public class FileCabinetFilesystemService : IFileCabinetService
    {
        private const int NameLength = 120;
        private const int Size = sizeof(short) + sizeof(int) + NameLength + NameLength + sizeof(int) + sizeof(int) + sizeof(int) + sizeof(decimal) + sizeof(short) + sizeof(char);
        private readonly FileStream fileStream;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileCabinetFilesystemService"/> class.
        /// </summary>
        /// <param name="fileStream">Filestream.</param>
        public FileCabinetFilesystemService(FileStream fileStream)
        {
            this.fileStream = fileStream;
        }

        /// <summary>
        /// Создание записи.
        /// </summary>
        /// <param name="newRecord">Запись.</param>
        /// <returns>Номер записи.</returns>
        public int CreateRecord(FileCabinetRecord newRecord)
        {
            using (FileStream fs = new FileStream(this.fileStream.Name, FileMode.Append))
            {
                if (newRecord == null)
                {
                    throw new Exception();
                }

                newRecord.Id = ((int)fs.Length / Size) + 1;
                var b1 = this.RecordToBytes(newRecord);
                fs.Write(b1);
                fs.Flush();

                return newRecord.Id + 1;
            }
        }

        /// <summary>
        /// Редактиование данных в словаре.
        /// </summary>
        /// <param name="dictionary">Словарь.</param>
        /// <param name="property">Свойство для проверки необходимости создания нового значения ключ-значение.</param>
        /// <param name="id">Номер редактируемой записи.</param>
        /// <param name="current">Запись до редактирования.</param>
        /// <param name="propName">Название свойства для поиска.</param>
        public void EditNoteAtDictionary(Dictionary<string, List<FileCabinetRecord>> dictionary, string property, int id, FileCabinetRecord current, string propName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Редактирование записи в списке.
        /// </summary>
        /// <param name="newRecord">Новые параметры записи.</param>
        public void EditRecord(FileCabinetRecord newRecord)
        {
            using (FileStream fs = new FileStream(this.fileStream.Name, FileMode.Open, FileAccess.ReadWrite))
            {
                if (newRecord == null)
                {
                    throw new Exception();
                }

                FileCabinetRecord u1 = null;
                var recordBuffer = new byte[Size];
                for (int i = 0; i < fs.Length / 278; i++)
                {
                    fs.Read(recordBuffer, 0, Size);
                    u1 = this.BytesToRecord(recordBuffer);
                    if (u1.Id == newRecord.Id)
                    {
                        u1 = newRecord;
                        break;
                    }
                }

                byte[] recordBytes = this.RecordToBytes(u1);
                fs.Seek(-Size, SeekOrigin.Current);
                fs.Write(recordBytes);
            }
        }

        /// <summary>
        /// Поиск по дате рождения.
        /// </summary>
        /// <param name="birthDate">Искомая дата рождения.</param>
        /// <returns>Массив найденных записей с искомой датой рождения.</returns>
        public ReadOnlyCollection<FileCabinetRecord> FindByBirthDate(string birthDate)
        {
            using (FileStream fs = new FileStream(this.fileStream.Name, FileMode.Open, FileAccess.Read))
            {
                List<FileCabinetRecord> records = new List<FileCabinetRecord>();
                var recordBuffer = new byte[Size];
                for (int i = 0; i < fs.Length / 278; i++)
                {
                    fs.Read(recordBuffer, 0, Size);
                    var u1 = this.BytesToRecord(recordBuffer);
                    if (u1.DateOfBirth.ToShortDateString() == birthDate)
                    {
                        records.Add(u1);
                    }
                }

                return new ReadOnlyCollection<FileCabinetRecord>(records);
            }
        }

        /// <summary>
        /// Поиск по имени.
        /// </summary>
        /// <param name="firstName">Искомое имя.</param>
        /// <returns>Массив найденных записей с именем firstName.</returns>
        public ReadOnlyCollection<FileCabinetRecord> FindByFirstName(string firstName)
        {
            using (FileStream fs = new FileStream(this.fileStream.Name, FileMode.Open, FileAccess.Read))
            {
                List<FileCabinetRecord> records = new List<FileCabinetRecord>();
                var recordBuffer = new byte[Size];
                for (int i = 0; i < fs.Length / 278; i++)
                {
                    fs.Read(recordBuffer, 0, Size);
                    var u1 = this.BytesToRecord(recordBuffer);
                    if (u1.FirstName.Equals(firstName, StringComparison.InvariantCultureIgnoreCase))
                    {
                        records.Add(u1);
                    }
                }

                return new ReadOnlyCollection<FileCabinetRecord>(records);
            }
        }

        /// <summary>
        /// Поиск по фамилии.
        /// </summary>
        /// <param name="lastName">Искомая фамилия.</param>
        /// <returns>Массив найденных записей с фамилией lastName.</returns>
        public ReadOnlyCollection<FileCabinetRecord> FindByLastName(string lastName)
        {
            using (FileStream fs = new FileStream(this.fileStream.Name, FileMode.Open, FileAccess.Read))
            {
                List<FileCabinetRecord> records = new List<FileCabinetRecord>();
                var recordBuffer = new byte[Size];
                for (int i = 0; i < fs.Length / 278; i++)
                {
                    fs.Read(recordBuffer, 0, Size);
                    var u1 = this.BytesToRecord(recordBuffer);
                    if (u1.LastName.Equals(lastName, StringComparison.InvariantCultureIgnoreCase))
                    {
                        records.Add(u1);
                    }
                }

                return new ReadOnlyCollection<FileCabinetRecord>(records);
            }
        }

        /// <summary>
        /// Получение списка записей.
        /// </summary>
        /// <returns>Список записей.</returns>
        public ReadOnlyCollection<FileCabinetRecord> GetRecords()
        {
            using (FileStream fs = new FileStream(this.fileStream.Name, FileMode.Open, FileAccess.Read))
            {
                List<FileCabinetRecord> records = new List<FileCabinetRecord>();
                var recordBuffer = new byte[Size];
                for (int i = 0; i < fs.Length / 278; i++)
                {
                    fs.Read(recordBuffer, 0, Size);
                    var u1 = this.BytesToRecord(recordBuffer);
                    records.Add(u1);
                }

                return new ReadOnlyCollection<FileCabinetRecord>(records);
            }
        }

        /// <summary>
        /// Получение числа записей.
        /// </summary>
        /// <returns>Число записей.</returns>
        public int GetStat()
        {
            using (FileStream fs = new FileStream(this.fileStream.Name, FileMode.Open, FileAccess.Read))
            {
                return (int)fs.Length / Size;
            }
        }

        /// <summary>
        /// Метод производящий добавление в список импортируемых записей.
        /// </summary>
        /// <param name="snap">Импортированные записи.</param>
        public void Restore(FileCabinetServiceSnapshot snap)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Создание копии состояния.
        /// </summary>
        /// <returns>Объект представляющий копию.</returns>
        public FileCabinetServiceSnapshot MakeSnapshot()
        {
            throw new NotImplementedException();
        }

        private byte[] RecordToBytes(FileCabinetRecord newRecord)
        {
            short reserved = 0;
            if (newRecord == null)
            {
                throw new ArgumentNullException(nameof(newRecord));
            }

            byte[] bytes = new byte[Size];
            using (var memoryStream = new MemoryStream(bytes))
            using (var binaryWriter = new BinaryWriter(memoryStream))
            {
                binaryWriter.Write(reserved);
                binaryWriter.Write(newRecord.Id);

                var firstNameBytes = Encoding.ASCII.GetBytes(newRecord.FirstName);
                var lastNameBytes = Encoding.ASCII.GetBytes(newRecord.LastName);
                var nameBuffer = new byte[NameLength];
                Array.Copy(firstNameBytes, nameBuffer, firstNameBytes.Length);
                binaryWriter.Write(nameBuffer, 0, nameBuffer.Length);
                Array.Copy(lastNameBytes, nameBuffer, lastNameBytes.Length);
                binaryWriter.Write(nameBuffer, 0, nameBuffer.Length);
                binaryWriter.Write(newRecord.DateOfBirth.Year);
                binaryWriter.Write(newRecord.DateOfBirth.Month);
                binaryWriter.Write(newRecord.DateOfBirth.Day);
                binaryWriter.Write(newRecord.Wage);
                binaryWriter.Write(newRecord.Height);
                binaryWriter.Write(newRecord.FavouriteNumeral);
            }

            return bytes;
        }

        private FileCabinetRecord BytesToRecord(byte[] bytes)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException(nameof(bytes));
            }

            var record = new FileCabinetRecord();
            using (var memoryStream = new MemoryStream(bytes))
            using (var binaryReader = new BinaryReader(memoryStream))
            {
                binaryReader.ReadInt16();
                record.Id = binaryReader.ReadInt32();
                var nameBuffer = binaryReader.ReadBytes(NameLength);
                record.FirstName = Encoding.ASCII.GetString(nameBuffer, 0, nameBuffer.Length).TrimEnd('\0');
                nameBuffer = binaryReader.ReadBytes(NameLength);
                record.LastName = Encoding.ASCII.GetString(nameBuffer, 0, nameBuffer.Length).TrimEnd('\0');
                int year = binaryReader.ReadInt32();
                int month = binaryReader.ReadInt32();
                int day = binaryReader.ReadInt32();
                DateTime dateTime = new DateTime(year, month, day);
                record.DateOfBirth = dateTime;
                record.Wage = binaryReader.ReadDecimal();
                record.Height = binaryReader.ReadInt16();
                record.FavouriteNumeral = binaryReader.ReadChar();
            }

            return record;
        }
    }
}
