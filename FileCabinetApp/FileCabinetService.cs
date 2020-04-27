using System;
using System.Collections.Generic;
using System.Text;

namespace FileCabinetApp
{
    /// <summary>
    /// Сервис для работы со списком записей и словарями.
    /// </summary>
    public class FileCabinetService
    {
        private readonly Dictionary<string, List<FileCabinetRecord>> firstNameDictionary = new Dictionary<string, List<FileCabinetRecord>>();
        private readonly Dictionary<string, List<FileCabinetRecord>> lastNameDictionary = new Dictionary<string, List<FileCabinetRecord>>();
        private readonly Dictionary<string, List<FileCabinetRecord>> dateOfBirthDictionary = new Dictionary<string, List<FileCabinetRecord>>();
        private readonly List<FileCabinetRecord> list = new List<FileCabinetRecord>();

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
        /// Метод создания новой записи.
        /// </summary>
        /// <param name="newRecord">Объект, представляющий запись.</param>
        /// <returns>Порядковый номер записи.</returns>
        public int CreateRecord(FileCabinetRecord newRecord)
        {
            if (newRecord == null)
            {
                throw new Exception();
            }

            this.ValidateParameters(newRecord);
            var record = new FileCabinetRecord
            {
                Id = this.list.Count + 1,
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
        /// Виртуальный пустой метод валидации.
        /// </summary>
        /// <param name="newRecord">Новая запись.</param>
        public virtual void ValidateParameters(FileCabinetRecord newRecord)
        {
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

            this.ValidateParameters(newRecord);
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
        public FileCabinetRecord[] GetRecords()
        {
            return this.list.ToArray();
        }

        /// <summary>
        /// Поиск по имени.
        /// </summary>
        /// <param name="firstName">Искомое имя.</param>
        /// <returns>Массив найденных записей с именем firstName.</returns>
        public FileCabinetRecord[] FindByFirstName(string firstName)
        {
            if (this.firstNameDictionary.TryGetValue(firstName, out List<FileCabinetRecord> keyList))
            {
                return keyList.ToArray();
            }
            else
            {
                Console.WriteLine("empty");
                return Array.Empty<FileCabinetRecord>();
            }
        }

        /// <summary>
        /// Поиск по фамилии.
        /// </summary>
        /// <param name="lastName">Искомая фамилия.</param>
        /// <returns>Массив найденных записей с фамилией lastName.</returns>
        public FileCabinetRecord[] FindByLastName(string lastName)
        {
            if (this.lastNameDictionary.TryGetValue(lastName, out List<FileCabinetRecord> keyList))
            {
                return keyList.ToArray();
            }
            else
            {
                Console.WriteLine("empty");
                return Array.Empty<FileCabinetRecord>();
            }
        }

        /// <summary>
        /// Поиск по дате рождения.
        /// </summary>
        /// <param name="birthDate">Искомая дата рождения.</param>
        /// <returns>Массив найденных записей с искомой датой рождения.</returns>
        public FileCabinetRecord[] FindByBirthDate(string birthDate)
        {
            if (this.dateOfBirthDictionary.TryGetValue(birthDate, out List<FileCabinetRecord> keyList))
            {
                return keyList.ToArray();
            }
            else
            {
                Console.WriteLine("empty");
                return Array.Empty<FileCabinetRecord>();
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
    }
}
