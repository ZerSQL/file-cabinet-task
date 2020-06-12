using System;
using System.Collections.Generic;
using System.Text;
using FileCabinetApp.Validators;

namespace FileCabinetApp
{
    /// <summary>
    /// Класс, представляющий обычный валидатор.
    /// </summary>
    public class DefaultValidator : CompositeValidator
    {
        private const int MinLength = 2;
        private const int MaxLength = 60;
        private const short MinHeight = 120;
        private const short MaxHeight = 250;
        private const char MinNum = '0';
        private const char MaxNum = '9';
        private const char Four = ' ';
        private const decimal MinWage = 300;
        private static readonly DateTime From = new DateTime(1950, 1, 1);
        private static readonly DateTime To = DateTime.Now;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultValidator"/> class.
        /// </summary>
        public DefaultValidator()
            : base(new IRecordValidator[]
            {
                new FirstNameValidator(MinLength, MaxLength),
                new LastNameValidator(MinLength, MaxLength),
                new DateOfBirthValidator(From, To),
                new WageValidator(MinWage),
                new FavouriteNumeralValidator(MinNum, MaxNum, Four),
                new HeightValidator(MinHeight, MaxHeight),
            })
        {
        }
    }
}
