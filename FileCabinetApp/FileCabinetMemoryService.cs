using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace FileCabinetApp
{
    /// <summary>
    /// Сервис для работы со списком записей и словарями.
    /// </summary>
    public class FileCabinetMemoryService : IFileCabinetService
    {
        private readonly Dictionary<string, List<FileCabinetRecord>> firstNameDictionary = new Dictionary<string, List<FileCabinetRecord>>();
        private readonly Dictionary<string, List<FileCabinetRecord>> lastNameDictionary = new Dictionary<string, List<FileCabinetRecord>>();
        private readonly Dictionary<string, List<FileCabinetRecord>> dateOfBirthDictionary = new Dictionary<string, List<FileCabinetRecord>>();
        private readonly List<FileCabinetRecord> list = new List<FileCabinetRecord>();
        private IRecordValidator validator;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileCabinetMemoryService"/> class.
        /// </summary>
        /// <param name="validator">Тип валидатора.</param>
        public FileCabinetMemoryService(IRecordValidator validator)
        {
            this.validator = validator;
        }

        /// <summary>
        /// Функция добавления записи в словарь.
        /// </summary>
        /// <param name="dictionary">Сам словарь.</param>
        /// <param name="property">Имя свойства для проверки необходимости создания новой пары ключ-значение.</param>
        /// <param name="record">Сама запись.</param>
        public static void AddNoteAtDictionary(Dictionary<string, List<FileCabinetRecord>> dictionary, string property, FileCabinetRecord record)
        {
            if (dictionary == null)
            {
                throw new Exception();
            }

            if (dictionary.TryGetValue(property, out List<FileCabinetRecord> note))
            {
                note.Add(record);
            }
            else
            {
                dictionary.Add(property, new List<FileCabinetRecord>() { record });
            }
        }

        /// <summary>
        /// Удаляет записи из словарей.
        /// </summary>
        /// <param name="dictionary">Словарь.</param>
        /// <param name="record">Запись.</param>
        public static void RemoveAtDictionary(Dictionary<string, List<FileCabinetRecord>> dictionary, FileCabinetRecord record)
        {
            if (dictionary == null)
            {
                throw new Exception();
            }

            foreach (var note in dictionary)
            {
                FileCabinetRecord deletedRecord = note.Value.Find(p => p.Id == record.Id);
                note.Value.Remove(deletedRecord);
            }
        }

        /// <summary>
        /// Метод создания новой записи.
        /// </summary>
        /// <param name="newRecord">Объект, представляющий запись.</param>
        /// <returns>Порядковый номер записи.</returns>
        public int CreateRecord(FileCabinetRecord newRecord)
        {
            int newId = 0;
            if (newRecord == null)
            {
                throw new Exception();
            }

            this.validator.ValidateParameters(newRecord);

            if (newRecord.Id == 0)
            {
                if (this.list.Count == 0)
                {
                    newId = 1;
                }
                else
                {
                    newId = this.list.Last().Id + 1;
                }
            }
            else
            {
                newId = newRecord.Id;
            }

            var record = new FileCabinetRecord
            {
                Id = newId,
                FirstName = newRecord.FirstName,
                LastName = newRecord.LastName,
                DateOfBirth = newRecord.DateOfBirth,
                Wage = newRecord.Wage,
                FavouriteNumeral = newRecord.FavouriteNumeral,
                Height = newRecord.Height,
            };

            AddNoteAtDictionary(this.firstNameDictionary, newRecord.FirstName, record);
            AddNoteAtDictionary(this.lastNameDictionary, newRecord.LastName, record);
            AddNoteAtDictionary(this.dateOfBirthDictionary, newRecord.DateOfBirth.ToShortDateString(), record);

            this.list.Add(record);

            return record.Id;
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
            if (dictionary == null)
            {
                throw new Exception();
            }

            string key = string.Empty;

            foreach (var note in dictionary)
            {
                foreach (var listItems in note.Value)
                {
                    if (listItems.Id == id)
                    {
                        if (dictionary == this.firstNameDictionary)
                        {
                            key = listItems.FirstName;
                        }
                        else if (dictionary == this.lastNameDictionary)
                        {
                            key = listItems.LastName;
                        }
                        else if (dictionary == this.dateOfBirthDictionary)
                        {
                            key = listItems.DateOfBirth.ToShortDateString();
                        }
                    }
                }
            }

            if (dictionary.TryGetValue(property, out List<FileCabinetRecord> k))
            {
                k.Add(current);
            }
            else
            {
                dictionary.Add(property, new List<FileCabinetRecord>() { current });
            }

            FileCabinetRecord temp = dictionary[propName].Find(x => x.Id == id);

            dictionary[propName].Remove(temp);
        }

        /// <summary>
        /// Редактирование записи в списке.
        /// </summary>
        /// <param name="newRecord">Новые параметры записи.</param>
        public void EditRecord(FileCabinetRecord newRecord)
        {
            if (newRecord == null)
            {
                throw new Exception();
            }

            this.validator.ValidateParameters(newRecord);
            FileCabinetRecord current = this.list.Find(x => x.Id == newRecord.Id);
            if (current == null)
            {
                throw new ArgumentException($"No element with id = {newRecord.Id}");
            }

            string prevFirstName = current.FirstName;
            string prevLastName = current.LastName;
            string prevDoB = current.DateOfBirth.ToShortDateString();
            current.FirstName = newRecord.FirstName;
            current.LastName = newRecord.LastName;
            current.DateOfBirth = newRecord.DateOfBirth;
            current.Wage = newRecord.Wage;
            current.FavouriteNumeral = newRecord.FavouriteNumeral;
            current.Height = newRecord.Height;
            this.EditNoteAtDictionary(this.firstNameDictionary, newRecord.FirstName, newRecord.Id, current, prevFirstName);
            this.EditNoteAtDictionary(this.lastNameDictionary, newRecord.LastName, newRecord.Id, current, prevLastName);
            this.EditNoteAtDictionary(this.dateOfBirthDictionary, newRecord.DateOfBirth.ToShortDateString(), newRecord.Id, current, prevDoB);

            Console.WriteLine($"Record #{newRecord.Id} is updated.");
        }

        /// <summary>
        /// Получение списка записей.
        /// </summary>
        /// <returns>Список записей.</returns>
        public ReadOnlyCollection<FileCabinetRecord> GetRecords()
        {
            return this.list.AsReadOnly();
        }

        /// <summary>
        /// Поиск по имени.
        /// </summary>
        /// <param name="firstName">Искомое имя.</param>
        /// <returns>Массив найденных записей с именем firstName.</returns>
        public ReadOnlyCollection<FileCabinetRecord> FindByFirstName(string firstName)
        {
            if (this.firstNameDictionary.TryGetValue(firstName, out List<FileCabinetRecord> keyList))
            {
                return keyList.AsReadOnly();
            }
            else
            {
                Console.WriteLine("empty");
                return new ReadOnlyCollection<FileCabinetRecord>(new List<FileCabinetRecord>());
            }
        }

        /// <summary>
        /// Поиск по фамилии.
        /// </summary>
        /// <param name="lastName">Искомая фамилия.</param>
        /// <returns>Массив найденных записей с фамилией lastName.</returns>
        public ReadOnlyCollection<FileCabinetRecord> FindByLastName(string lastName)
        {
            if (this.lastNameDictionary.TryGetValue(lastName, out List<FileCabinetRecord> keyList))
            {
                return keyList.AsReadOnly();
            }
            else
            {
                Console.WriteLine("empty");
                return new ReadOnlyCollection<FileCabinetRecord>(new List<FileCabinetRecord>());
            }
        }

        /// <summary>
        /// Поиск по дате рождения.
        /// </summary>
        /// <param name="birthDate">Искомая дата рождения.</param>
        /// <returns>Массив найденных записей с искомой датой рождения.</returns>
        public ReadOnlyCollection<FileCabinetRecord> FindByBirthDate(string birthDate)
        {
            if (this.dateOfBirthDictionary.TryGetValue(birthDate, out List<FileCabinetRecord> keyList))
            {
                return keyList.AsReadOnly();
            }
            else
            {
                Console.WriteLine("empty");
                return new ReadOnlyCollection<FileCabinetRecord>(new List<FileCabinetRecord>());
            }
        }

        /// <summary>
        /// Получение числа записей.
        /// </summary>
        /// <returns>Число записей.</returns>
        public int GetStat()
        {
            return this.list.Count;
        }

        /// <summary>
        /// Создание копии состояния.
        /// </summary>
        /// <returns>Объект представляющий копию.</returns>
        public FileCabinetServiceSnapshot MakeSnapshot()
        {
            FileCabinetServiceSnapshot snapshot = new FileCabinetServiceSnapshot(this.list);
            return snapshot;
        }

        /// <summary>
        /// Метод производящий добавление в список и словари импортируемых записей.
        /// </summary>
        /// <param name="snap">Импортированные записи.</param>
        public void Restore(FileCabinetServiceSnapshot snap)
        {
            if (snap == null)
            {
                throw new Exception();
            }

            foreach (FileCabinetRecord record in snap.Records)
            {
                int t;
                if (this.list.Count == 0)
                {
                    t = 0;
                }
                else
                {
                    t = this.list.Last().Id;
                }

                if (record.Id <= this.list.Count || record.Id <= t)
                {
                    this.EditRecord(record);
                }
                else
                {
                    this.CreateRecord(record);
                }
            }
        }

        /// <summary>
        /// Удаляет записи из списка.
        /// </summary>
        /// <param name="number">Номер удаляемой записи.</param>
        public void Remove(int number)
        {
            foreach (var record in this.list)
            {
                if (record.Id == number)
                {
                    this.list.Remove(record);
                    Console.WriteLine($"Record #{number} is removed.");
                    RemoveAtDictionary(this.firstNameDictionary, record);
                    RemoveAtDictionary(this.lastNameDictionary, record);
                    RemoveAtDictionary(this.dateOfBirthDictionary, record);
                    return;
                }
            }

            Console.WriteLine("Record doesn't exists.");
        }
    }
}
