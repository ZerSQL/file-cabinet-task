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
        private const int Size = sizeof(short) + sizeof(int) + 120 + 120 + sizeof(int) + sizeof(int) + sizeof(int) + sizeof(decimal) + sizeof(short) + sizeof(char);
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
            throw new NotImplementedException();
        }

        /// <summary>
        /// Поиск по дате рождения.
        /// </summary>
        /// <param name="birthDate">Искомая дата рождения.</param>
        /// <returns>Массив найденных записей с искомой датой рождения.</returns>
        public ReadOnlyCollection<FileCabinetRecord> FindByBirthDate(string birthDate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Поиск по имени.
        /// </summary>
        /// <param name="firstName">Искомое имя.</param>
        /// <returns>Массив найденных записей с именем firstName.</returns>
        public ReadOnlyCollection<FileCabinetRecord> FindByFirstName(string firstName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Поиск по фамилии.
        /// </summary>
        /// <param name="lastName">Искомая фамилия.</param>
        /// <returns>Массив найденных записей с фамилией lastName.</returns>
        public ReadOnlyCollection<FileCabinetRecord> FindByLastName(string lastName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Получение списка записей.
        /// </summary>
        /// <returns>Список записей.</returns>
        public ReadOnlyCollection<FileCabinetRecord> GetRecords()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Получение числа записей.
        /// </summary>
        /// <returns>Число записей.</returns>
        public int GetStat()
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
            char reserved = ' ';
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
                var nameBuffer = new byte[120];
                nameBuffer = firstNameBytes;
                binaryWriter.Write(nameBuffer, 0, nameBuffer.Length);
                nameBuffer = lastNameBytes;
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
    }
}
