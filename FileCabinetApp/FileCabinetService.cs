using System;
using System.Collections.Generic;
using System.Text;

namespace FileCabinetApp
{
    public class FileCabinetService
    {
        private readonly Dictionary<string, List<FileCabinetRecord>> firstNameDictionary = new Dictionary<string, List<FileCabinetRecord>>();
        private readonly List<FileCabinetRecord> list = new List<FileCabinetRecord>();
        private readonly DateTime minDate = new DateTime(1950, 1, 1);
        private readonly DateTime maxDate = DateTime.Now;

        public int CreateRecord(string firstName, string lastName, DateTime dateOfBirth, decimal wage, char favouriteNumeral, short height)
        {
            if (firstName == null || lastName == null)
            {
                throw new ArgumentNullException($"Name cannot be null. Please, try again. First name is {firstName}, last name is {lastName}.");
            }

            if (firstName.Length < 2 || firstName.Length > 60 || firstName.Contains(' ', StringComparison.CurrentCulture) ||
                lastName.Length < 2 || lastName.Length > 60 || lastName.Contains(' ', StringComparison.CurrentCulture) ||
                dateOfBirth > this.maxDate || dateOfBirth < this.minDate ||
                wage < 300 ||
                favouriteNumeral < '0' || favouriteNumeral > '9')
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
                FavouriteNumeral = favouriteNumeral,
                Height = height,
            };

            if (this.firstNameDictionary.TryGetValue(firstName, out List<FileCabinetRecord> note))
            {
                note.Add(record);
            }
            else
            {
                this.firstNameDictionary.Add(firstName, new List<FileCabinetRecord>() { record });
            }

            this.list.Add(record);

            return record.Id;
        }

        public void EditRecord(int id, string firstName, string lastName, DateTime dateOfBirth, decimal wage, char favouriteNumeral, short height)
        {
            string key = string.Empty;
            FileCabinetRecord current = this.list.Find(x => x.Id == id);

            if (current == null)
            {
                throw new ArgumentException($"No element with id = {id}");
            }

            foreach (var note in this.firstNameDictionary)
            {
                foreach (var listItems in note.Value)
                {
                    if (listItems.Id == id)
                    {
                        key = listItems.FirstName;
                    }
                }
            }

            FileCabinetRecord temp = this.firstNameDictionary[key].Find(x => x.Id == id);
            current.FirstName = firstName;
            current.LastName = lastName;
            current.DateOfBirth = dateOfBirth;
            current.Wage = wage;
            current.FavouriteNumeral = favouriteNumeral;
            current.Height = height;

            if (this.firstNameDictionary.TryGetValue(firstName, out List<FileCabinetRecord> k))
            {
                k.Add(current);
            }
            else
            {
                this.firstNameDictionary.Add(firstName, new List<FileCabinetRecord>() { current });
            }

            this.firstNameDictionary[key].Remove(temp);
            Console.WriteLine($"Record #{id} is updated.");
        }

        public FileCabinetRecord[] GetRecords()
        {
            return this.list.ToArray();
        }

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

        public FileCabinetRecord[] FindByLastName(string lastName)
        {
            List<FileCabinetRecord> result = new List<FileCabinetRecord>();
            foreach (var item in this.list)
            {
                if (item.LastName.ToLower(System.Globalization.CultureInfo.CurrentCulture) == lastName.ToLower(System.Globalization.CultureInfo.CurrentCulture))
                {
                    result.Add(item);
                }
            }

            return result.ToArray();
        }

        public FileCabinetRecord[] FindByBirthDate(string birthDate)
        {
            List<FileCabinetRecord> result = new List<FileCabinetRecord>();
            foreach (var note in this.list)
            {
                if (note.DateOfBirth.ToShortDateString() == birthDate)
                {
                    result.Add(note);
                }
            }

            return result.ToArray();
        }

        public int GetStat()
        {
            return this.list.Count;
        }
    }
}
