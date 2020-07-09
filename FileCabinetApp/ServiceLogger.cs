using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;

namespace FileCabinetApp
{
    /// <summary>
    /// Класс для журналирования.
    /// </summary>
    public class ServiceLogger : IFileCabinetService
    {
        private const string Path = "D:\\log.txt";
        private IFileCabinetService fileCabinetService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceLogger"/> class.
        /// </summary>
        /// <param name="service">Сервис.</param>
        public ServiceLogger(IFileCabinetService service)
        {
            this.fileCabinetService = service;
        }

        /// <summary>
        /// Метод создания записи.
        /// </summary>
        /// <param name="newRecord">Запись.</param>
        /// <returns>Порядковый номер созданной записи.</returns>
        public int CreateRecord(FileCabinetRecord newRecord)
        {
            if (this.fileCabinetService != null && newRecord != null)
            {
                using (StreamWriter streamWriter = new StreamWriter(Path, true))
                {
                    streamWriter.WriteLine($"{DateTime.Now} - Calling Create() method with FirstName = {newRecord.FirstName}, LastName = {newRecord.LastName}, DateOfBirth = {newRecord.DateOfBirth.ToShortDateString()}.");
                    int t = this.fileCabinetService.CreateRecord(newRecord);
                    streamWriter.WriteLine($"{DateTime.Now} - Create() method returned {t}.");
                    return t;
                }
            }
            else
            {
                throw new ArgumentNullException($"Record is empty");
            }
        }

        /// <summary>
        /// Редактиование данных в словаре.
        /// </summary>
        /// <param name="dictionary">Словарь.</param>
        /// <param name="prop">Свойство для проверки необходимости создания нового значения ключ-значение.</param>
        /// <param name="id">Номер редактируемой записи.</param>
        /// <param name="current">Запись до редактирования.</param>
        /// <param name="propName">Название свойства для поиска.</param>
        public void EditNoteAtDictionary(Dictionary<string, List<FileCabinetRecord>> dictionary, string prop, int id, FileCabinetRecord current, string propName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Редактирование записи в списке.
        /// </summary>
        /// <param name="newRecord">Новые параметры записи.</param>
        public void EditRecord(FileCabinetRecord newRecord)
        {
            if (this.fileCabinetService != null && newRecord != null)
            {
                using (StreamWriter streamWriter = new StreamWriter(Path, true))
                {
                    streamWriter.WriteLine($"{DateTime.Now} - Calling Edit() method with Id = {newRecord.Id}, FirstName = {newRecord.FirstName}, LastName = {newRecord.LastName}, DateOfBirth = {newRecord.DateOfBirth.ToShortDateString()}.");
                    this.fileCabinetService.EditRecord(newRecord);
                }
            }
            else
            {
                throw new ArgumentNullException($"Record is empty");
            }
        }

        /// <summary>
        /// Поиск по дате рождения.
        /// </summary>
        /// <param name="birthDate">Искомая дата рождения.</param>
        /// <returns>Массив найденных записей с искомой датой рождения.</returns>
        public IEnumerable<FileCabinetRecord> FindByBirthDate(string birthDate)
        {
            if (this.fileCabinetService != null)
            {
                using (StreamWriter streamWriter = new StreamWriter(Path, true))
                {
                    streamWriter.WriteLine($"{DateTime.Now} - Calling FindByBirthDate() method with birthDate = {birthDate}.");
                    var list = this.fileCabinetService.FindByBirthDate(birthDate);
                    streamWriter.WriteLine($"{DateTime.Now} - FindByBirthDate() method returned list.");
                    return list;
                }
            }
            else
            {
                throw new ArgumentNullException($"Record is empty");
            }
        }

        /// <summary>
        /// Поиск по имени.
        /// </summary>
        /// <param name="firstName">Искомое имя.</param>
        /// <returns>Массив найденных записей с именем firstName.</returns>
        public IEnumerable<FileCabinetRecord> FindByFirstName(string firstName)
        {
            if (this.fileCabinetService != null)
            {
                using (StreamWriter streamWriter = new StreamWriter(Path, true))
                {
                    streamWriter.WriteLine($"{DateTime.Now} - Calling FindByFirstName() method with firstName = {firstName}.");
                    var list = this.fileCabinetService.FindByFirstName(firstName);
                    streamWriter.WriteLine($"{DateTime.Now} - FindByFirstName() method returned list.");
                    return list;
                }
            }
            else
            {
                throw new ArgumentNullException($"Record is empty");
            }
        }

        /// <summary>
        /// Поиск по фамилии.
        /// </summary>
        /// <param name="lastName">Искомая фамилия.</param>
        /// <returns>Массив найденных записей с фамилией lastName.</returns>
        public IEnumerable<FileCabinetRecord> FindByLastName(string lastName)
        {
            if (this.fileCabinetService != null)
            {
                using (StreamWriter streamWriter = new StreamWriter(Path, true))
                {
                    streamWriter.WriteLine($"{DateTime.Now} - Calling FindByLastName() method with lastName = {lastName}.");
                    var list = this.fileCabinetService.FindByLastName(lastName);
                    streamWriter.WriteLine($"{DateTime.Now} - FindByLastName() method returned list.");
                    return list;
                }
            }
            else
            {
                throw new ArgumentNullException($"Record is empty");
            }
        }

        /// <summary>
        /// Получение списка записей.
        /// </summary>
        /// <returns>Список записей.</returns>
        public IEnumerable<FileCabinetRecord> GetRecords()
        {
            if (this.fileCabinetService != null)
            {
                using (StreamWriter streamWriter = new StreamWriter(Path, true))
                {
                    streamWriter.WriteLine($"{DateTime.Now} - Calling GetRecords() method.");
                    var list = this.fileCabinetService.GetRecords();
                    streamWriter.WriteLine($"{DateTime.Now} - GetRecords() method returned list.");
                    return list;
                }
            }
            else
            {
                throw new ArgumentNullException($"Record is empty");
            }
        }

        /// <summary>
        /// Получение числа записей.
        /// </summary>
        /// <returns>Число записей.</returns>
        public int GetStat()
        {
            if (this.fileCabinetService != null)
            {
                using (StreamWriter streamWriter = new StreamWriter(Path, true))
                {
                    streamWriter.WriteLine($"{DateTime.Now} - Calling GetStat() method.");
                    int count = this.fileCabinetService.GetStat();
                    streamWriter.WriteLine($"{DateTime.Now} - GetRecords() method returned count.");
                    return count;
                }
            }
            else
            {
                throw new ArgumentNullException($"Record is empty");
            }
        }

        /// <summary>
        /// Создает копию.
        /// </summary>
        /// <returns>Объект содержащий копию и метод записи.</returns>
        public FileCabinetServiceSnapshot MakeSnapshot()
        {
            if (this.fileCabinetService != null)
            {
                using (StreamWriter streamWriter = new StreamWriter(Path, true))
                {
                    streamWriter.WriteLine($"{DateTime.Now} - Calling MakeSnapshot() method.");
                    var snap = this.fileCabinetService.MakeSnapshot();
                    streamWriter.WriteLine($"{DateTime.Now} - MakeSnapshot() method returned snap.");
                    return snap;
                }
            }
            else
            {
                throw new ArgumentNullException($"Record is empty");
            }
        }

        /// <summary>
        /// Удаляет записи, помеченные битом IsDeleted.
        /// </summary>
        public void Purge()
        {
            if (this.fileCabinetService != null)
            {
                using (StreamWriter streamWriter = new StreamWriter(Path, true))
                {
                    streamWriter.WriteLine($"{DateTime.Now} - Calling Purge() method.");
                    this.fileCabinetService.Purge();
                }
            }
            else
            {
                throw new ArgumentNullException($"Record is empty");
            }
        }

        /// <summary>
        /// Удаляет записи из списка.
        /// </summary>
        /// <param name="number">Номер удаляемой записи.</param>
        public void Remove(int number)
        {
            if (this.fileCabinetService != null)
            {
                using (StreamWriter streamWriter = new StreamWriter(Path, true))
                {
                    streamWriter.WriteLine($"{DateTime.Now} - Calling Remove() method with Number = {number}.");
                    this.fileCabinetService.Remove(number);
                }
            }
            else
            {
                throw new ArgumentNullException($"Record is empty");
            }
        }

        /// <summary>
        /// Замена и добавление импортированных записей.
        /// </summary>
        /// <param name="snap">Snap.</param>
        public void Restore(FileCabinetServiceSnapshot snap)
        {
            if (this.fileCabinetService != null)
            {
                using (StreamWriter streamWriter = new StreamWriter(Path, true))
                {
                    streamWriter.WriteLine($"{DateTime.Now} - Calling Restore() method.");
                    this.fileCabinetService.Restore(snap);
                }
            }
            else
            {
                throw new ArgumentNullException($"Record is empty");
            }
        }
    }
}
