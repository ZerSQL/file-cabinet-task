using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;

namespace FileCabinetApp
{
    /// <summary>
    /// Класс выводящий время, затраченное на выполнение метода.
    /// </summary>
    public class ServiceMeter : IFileCabinetService
    {
        private IFileCabinetService service;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceMeter"/> class.
        /// </summary>
        /// <param name="service">Используемый сервис.</param>
        public ServiceMeter(IFileCabinetService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Метод создания записи.
        /// </summary>
        /// <param name="newRecord">Запись.</param>
        /// <returns>Порядковый номер созданной записи.</returns>
        public int CreateRecord(FileCabinetRecord newRecord)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            int t = this.service.CreateRecord(newRecord);
            watch.Stop();
            Console.WriteLine($"CreateRecord method execution duration is {watch.ElapsedTicks} ticks.");
            watch.Reset();
            return t;
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
            Stopwatch watch = new Stopwatch();
            watch.Start();
            this.service.EditRecord(newRecord);
            watch.Stop();
            Console.WriteLine($"EditRecord method execution duration is {watch.ElapsedTicks} ticks.");
            watch.Reset();
        }

        /// <summary>
        /// Поиск по дате рождения.
        /// </summary>
        /// <param name="birthDate">Искомая дата рождения.</param>
        /// <returns>Массив найденных записей с искомой датой рождения.</returns>
        public IEnumerable<FileCabinetRecord> FindByBirthDate(string birthDate)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            IEnumerable<FileCabinetRecord> collection = this.service.FindByBirthDate(birthDate);
            watch.Stop();
            Console.WriteLine($"FindByBirthDate method execution duration is {watch.ElapsedTicks} ticks.");
            watch.Reset();
            return collection;
        }

        /// <summary>
        /// Поиск по имени.
        /// </summary>
        /// <param name="firstName">Искомое имя.</param>
        /// <returns>Массив найденных записей с именем firstName.</returns>
        public IEnumerable<FileCabinetRecord> FindByFirstName(string firstName)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            IEnumerable<FileCabinetRecord> collection = this.service.FindByFirstName(firstName);
            watch.Stop();
            Console.WriteLine($"FindByFirstName method execution duration is {watch.ElapsedTicks} ticks.");
            watch.Reset();
            return collection;
        }

        /// <summary>
        /// Поиск по фамилии.
        /// </summary>
        /// <param name="lastName">Искомая фамилия.</param>
        /// <returns>Массив найденных записей с фамилией lastName.</returns>
        public IEnumerable<FileCabinetRecord> FindByLastName(string lastName)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            IEnumerable<FileCabinetRecord> collection = this.service.FindByLastName(lastName);
            watch.Stop();
            Console.WriteLine($"FindByLastName method execution duration is {watch.ElapsedTicks} ticks.");
            watch.Reset();
            return collection;
        }

        /// <summary>
        /// Получение списка записей.
        /// </summary>
        /// <returns>Список записей.</returns>
        public IEnumerable<FileCabinetRecord> GetRecords()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            IEnumerable<FileCabinetRecord> collection = this.service.GetRecords();
            watch.Stop();
            Console.WriteLine($"GetRecords method execution duration is {watch.ElapsedTicks} ticks.");
            watch.Reset();
            return collection;
        }

        /// <summary>
        /// Получение числа записей.
        /// </summary>
        /// <returns>Число записей.</returns>
        public int GetStat()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            int count = this.service.GetStat();
            watch.Stop();
            Console.WriteLine($"Stat method execution duration is {watch.ElapsedTicks} ticks.");
            watch.Reset();
            return count;
        }

        /// <summary>
        /// Создает копию.
        /// </summary>
        /// <returns>Объект содержащий копию и метод записи.</returns>
        public FileCabinetServiceSnapshot MakeSnapshot()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            FileCabinetServiceSnapshot snap = this.service.MakeSnapshot();
            watch.Stop();
            Console.WriteLine($"MakeSnapshot method execution duration is {watch.ElapsedTicks} ticks.");
            watch.Reset();
            return snap;
        }

        /// <summary>
        /// Удаляет записи, помеченные битом IsDeleted.
        /// </summary>
        public void Purge()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            this.service.Purge();
            watch.Stop();
            Console.WriteLine($"Purge method execution duration is {watch.ElapsedTicks} ticks.");
            watch.Reset();
        }

        /// <summary>
        /// Удаляет записи из списка.
        /// </summary>
        /// <param name="number">Номер удаляемой записи.</param>
        public void Remove(int number)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            this.service.Remove(number);
            watch.Stop();
            Console.WriteLine($"Remove method execution duration is {watch.ElapsedTicks} ticks.");
            watch.Reset();
        }

        /// <summary>
        /// Замена и добавление импортированных записей.
        /// </summary>
        /// <param name="snap">Snap.</param>
        public void Restore(FileCabinetServiceSnapshot snap)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            this.service.Restore(snap);
            watch.Stop();
            Console.WriteLine($"Restore method execution duration is {watch.ElapsedTicks} ticks.");
            watch.Reset();
        }
    }
}
