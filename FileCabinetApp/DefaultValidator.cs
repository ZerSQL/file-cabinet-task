using System;
using System.Collections.Generic;
using System.Text;

namespace FileCabinetApp
{
    /// <summary>
    /// Класс, представляющий обычный валидатор.
    /// </summary>
    public class DefaultValidator : IRecordValidator
    {
        private const int MinLength = 2;
        private const int MaxLength = 60;
        private const decimal MinWage = 300;
        private const short MinHeight = 120;
        private const short MaxHeight = 250;
        private static readonly DateTime MinDateTime = new DateTime(1950, 1, 1);

        /// <summary>
        /// Метод, выполняющий обычную валидацию.
        /// </summary>
        /// <param name="newRecord">Проверяемая запись.</param>
        public void ValidateParameters(FileCabinetRecord newRecord)
        {
            if (newRecord == null)
            {
                throw new Exception();
            }

            if (newRecord.FirstName == null || newRecord.LastName == null)
            {
                throw new ArgumentNullException($"Name cannot be null. Please, try again. First name is {newRecord.FirstName}, last name is {newRecord.LastName}.");
            }

            ValidateFirstName(newRecord.FirstName);
            ValidateLastName(newRecord.LastName);
            ValidateDateOfBirth(newRecord.DateOfBirth);
            ValidateWage(newRecord.Wage);
            ValidateFavouriteNumeral(newRecord.FavouriteNumeral);
            ValidateHeight(newRecord.Height);
        }

        private static void ValidateFirstName(string firstName)
        {
            if (firstName.Length < MinLength || firstName.Length > MaxLength || firstName.Contains(' ', StringComparison.CurrentCulture))
            {
                throw new ArgumentException($"Firstname must contain more than {MinLength} and less than {MaxLength} letters.");
            }
        }

        private static void ValidateLastName(string lastName)
        {
            if (lastName.Length < MinLength || lastName.Length > MaxLength || lastName.Contains(' ', StringComparison.CurrentCulture))
            {
                throw new ArgumentException($"Lastname must contain more than {MinLength} and less than {MaxLength} letters.");
            }
        }

        private static void ValidateDateOfBirth(DateTime dateOfBirth)
        {
            if (dateOfBirth > DateTime.Now || dateOfBirth < MinDateTime)
            {
                throw new ArgumentException($"DateOfBirth must be less than current time and more than {MinDateTime.ToShortDateString()}.");
            }
        }

        private static void ValidateWage(decimal wage)
        {
            if (wage < MinWage)
            {
                throw new ArgumentException($"Wage must be more than {MinWage}.");
            }
        }

        private static void ValidateFavouriteNumeral(char numeral)
        {
            if (numeral < '0' || numeral > '9' || numeral == '4')
            {
                throw new ArgumentException($"Numeral must be between '0' and '9'.");
            }
        }

        private static void ValidateHeight(short height)
        {
            if (height < MinHeight || height > MaxHeight)
            {
                throw new ArgumentException($"Height must be between {MinHeight} and {MaxHeight}.");
            }
        }
    }
}
