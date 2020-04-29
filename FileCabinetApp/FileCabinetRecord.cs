using System;

namespace FileCabinetApp
{
    /// <summary>
    /// Класс, представляющий запись.
    /// </summary>
    public class FileCabinetRecord
    {
        /// <summary>
        /// Gets or sets id of note.
        /// </summary>
        /// <value>
        /// Порядковый номер записи.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets firstname in note.
        /// </summary>
        /// <value>
        /// Имя сотрудника.
        /// </value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets lastname in note.
        /// </summary>
        /// <value>
        /// Фамилия сотрудника.
        /// </value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets Birthday date in note.
        /// </summary>
        /// <value>
        /// Дата рождения сотрудника.
        /// </value>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets height in note.
        /// </summary>
        /// <value>
        /// Рост сотрудника.
        /// </value>
        public short Height { get; set; }

        /// <summary>
        /// Gets or sets wage in note.
        /// </summary>
        /// <value>
        /// Заработная плата сотрудника.
        /// </value>
        public decimal Wage { get; set; }

        /// <summary>
        /// Gets or sets numeral in note.
        /// </summary>
        /// <value>
        /// Любимое число.
        /// </value>
        public char FavouriteNumeral { get; set; }
    }
}
