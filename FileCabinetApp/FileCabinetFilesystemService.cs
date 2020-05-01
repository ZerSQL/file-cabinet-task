using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FileCabinetApp
{
    /// <summary>
    /// Класс для создания файлов данных.
    /// </summary>
    public class FileCabinetFilesystemService : IFileCabinetService
    {
        /// <summary>
        /// Создание записи.
        /// </summary>
        /// <param name="newRecord">Запись.</param>
        /// <returns>Номер записи.</returns>
        public int CreateRecord(FileCabinetRecord newRecord)
        {
            throw new NotImplementedException();
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
    }
}
