using System;
using System.Collections.Generic;
using System.Text;

namespace FileCabinetApp
{
    public class FileCabinetService
    {
        private readonly List<FileCabinetRecord> list = new List<FileCabinetRecord>();
        private DateTime minDate = new DateTime(1950, 1, 1);
        private DateTime maxDate = DateTime.Now;

        public int CreateRecord(string firstName, string lastName, DateTime dateOfBirth, decimal wage, char favouriteNumber, short height)
        {
            if (firstName == null || lastName == null)
            {
                throw new ArgumentNullException($"Name cannot be null. Please, try again. First name is {firstName}, last name is {lastName}.");
            }

            if (firstName.Length < 2 || firstName.Length > 60 || firstName.Contains(' ', StringComparison.CurrentCulture) ||
                lastName.Length < 2 || lastName.Length > 60 || lastName.Contains(' ', StringComparison.CurrentCulture) ||
                dateOfBirth > this.maxDate || dateOfBirth < this.minDate ||
                wage < 35 || wage > 200 ||
                favouriteNumber < '0' || favouriteNumber > '9')
            {
                throw new ArgumentException("Error input");
            }

            var record = new FileCabinetRecord
            {
                Id = this.list.Count + 1,
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = dateOfBirth,
                Wage = wage,
                FavouriteNumber = favouriteNumber,
                Height = height,
            };

            this.list.Add(record);

            return record.Id;
        }

        public FileCabinetRecord[] GetRecords()
        {
            return this.list.ToArray();
        }

        public int GetStat()
        {
            return this.list.Count;
        }
    }
}
