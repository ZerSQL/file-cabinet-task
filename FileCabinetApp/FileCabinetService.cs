using System;
using System.Collections.Generic;
using System.Text;

namespace FileCabinetApp
{
    public class FileCabinetService
    {
        private readonly Dictionary<string, List<FileCabinetRecord>> firstNameDictionary = new Dictionary<string, List<FileCabinetRecord>>();
        private readonly Dictionary<string, List<FileCabinetRecord>> lastNameDictionary = new Dictionary<string, List<FileCabinetRecord>>();
        private readonly Dictionary<string, List<FileCabinetRecord>> dateOfBirthDictionary = new Dictionary<string, List<FileCabinetRecord>>();
        private readonly List<FileCabinetRecord> list = new List<FileCabinetRecord>();
        private readonly DateTime minDate = new DateTime(1950, 1, 1);
        private readonly DateTime maxDate = DateTime.Now;

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

            AddNoteAtDictionary(this.firstNameDictionary, firstName, record);
            AddNoteAtDictionary(this.lastNameDictionary, lastName, record);
            AddNoteAtDictionary(this.dateOfBirthDictionary, dateOfBirth.ToShortDateString(), record);

            this.list.Add(record);

            return record.Id;
        }

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

        public void EditRecord(int id, string firstName, string lastName, DateTime dateOfBirth, decimal wage, char favouriteNumeral, short height)
        {
            FileCabinetRecord current = this.list.Find(x => x.Id == id);
            if (current == null)
            {
                throw new ArgumentException($"No element with id = {id}");
            }

            string prevFirstName = current.FirstName;
            string prevLastName = current.LastName;
            string prevDoB = current.DateOfBirth.ToShortDateString();
            current.FirstName = firstName;
            current.LastName = lastName;
            current.DateOfBirth = dateOfBirth;
            current.Wage = wage;
            current.FavouriteNumeral = favouriteNumeral;
            current.Height = height;
            this.EditNoteAtDictionary(this.firstNameDictionary, firstName, id, current, prevFirstName);
            this.EditNoteAtDictionary(this.lastNameDictionary, lastName, id, current, prevLastName);
            this.EditNoteAtDictionary(this.dateOfBirthDictionary, dateOfBirth.ToShortDateString(), id, current, prevDoB);

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

        public int GetStat()
        {
            return this.list.Count;
        }
    }
}
