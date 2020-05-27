using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FileCabinetApp
{
    /// <summary>
    /// Интерфейс представляющий сервис.
    /// </summary>
    internal interface IFileCabinetService
    {
        /// <summary>
        /// Метод создания записи.
        /// </summary>
        /// <param name="newRecord">Запись.</param>
        /// <returns>Порядковый номер созданной записи.</returns>
        int CreateRecord(FileCabinetRecord newRecord);

        /// <summary>
        /// Редактиование данных в словаре.
        /// </summary>
        /// <param name="dictionary">Словарь.</param>
        /// <param name="property">Свойство для проверки необходимости создания нового значения ключ-значение.</param>
        /// <param name="id">Номер редактируемой записи.</param>
        /// <param name="current">Запись до редактирования.</param>
        /// <param name="propName">Название свойства для поиска.</param>
        void EditNoteAtDictionary(Dictionary<string, List<FileCabinetRecord>> dictionary, string property, int id, FileCabinetRecord current, string propName);

        /// <summary>
        /// Редактирование записи в списке.
        /// </summary>
        /// <param name="newRecord">Новые параметры записи.</param>
        void EditRecord(FileCabinetRecord newRecord);

        /// <summary>
        /// Получение списка записей.
        /// </summary>
        /// <returns>Список записей.</returns>
        ReadOnlyCollection<FileCabinetRecord> GetRecords();

        /// <summary>
        /// Поиск по имени.
        /// </summary>
        /// <param name="firstName">Искомое имя.</param>
        /// <returns>Массив найденных записей с именем firstName.</returns>
        ReadOnlyCollection<FileCabinetRecord> FindByFirstName(string firstName);

        /// <summary>
        /// Поиск по фамилии.
        /// </summary>
        /// <param name="lastName">Искомая фамилия.</param>
        /// <returns>Массив найденных записей с фамилией lastName.</returns>
        ReadOnlyCollection<FileCabinetRecord> FindByLastName(string lastName);

        /// <summary>
        /// Поиск по дате рождения.
        /// </summary>
        /// <param name="birthDate">Искомая дата рождения.</param>
        /// <returns>Массив найденных записей с искомой датой рождения.</returns>
        ReadOnlyCollection<FileCabinetRecord> FindByBirthDate(string birthDate);

        /// <summary>
        /// Получение числа записей.
        /// </summary>
        /// <returns>Число записей.</returns>
        int GetStat();

        /// <summary>
        /// Создает копию.
        /// </summary>
        /// <returns>Объект содержащий копию и метод записи.</returns>
        FileCabinetServiceSnapshot MakeSnapshot();

        /// <summary>
        /// Замена и добавление импортированных записей.
        /// </summary>
        /// <param name="snap">Snap.</param>
        void Restore(FileCabinetServiceSnapshot snap);

        /// <summary>
        /// Удаляет записи из списка.
        /// </summary>
        /// <param name="number">Номер удаляемой записи.</param>
        void Remove(int number);
    }
}
