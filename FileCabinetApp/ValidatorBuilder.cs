using System;
using System.Collections.Generic;
using System.Text;
using FileCabinetApp.Validators;

namespace FileCabinetApp
{
    /// <summary>
    /// Класс для создания любых валидаторов.
    /// </summary>
    public class ValidatorBuilder
    {
        private List<IRecordValidator> validators;

        /// <summary>
        /// Метод создания валидатора по умолчанию.
        /// </summary>
        /// <returns>Валидатор по умолчанию.</returns>
        public static IRecordValidator CreateDefault()
        {
            return new DefaultValidator();
        }

        /// <summary>
        /// Метод создания измененного валидатора.
        /// </summary>
        /// <returns>Измененный валидатор.</returns>
        public static IRecordValidator CreateCustom()
        {
            return new CustomValidator();
        }

        /// <summary>
        /// Метод создания.
        /// </summary>
        /// <returns>Валидатор.</returns>
        public IRecordValidator Create()
        {
            return new CompositeValidator(this.validators);
        }

        /// <summary>
        /// Валидация имени.
        /// </summary>
        /// <param name="min">Мин.длина.</param>
        /// <param name="max">Макс.длина.</param>
        /// <returns>Этот же объект.</returns>
        public ValidatorBuilder ValidateFirstName(int min, int max)
        {
            this.validators.Add(new FirstNameValidator(min, max));
            return this;
        }

        /// <summary>
        /// Валидация фамилии.
        /// </summary>
        /// <param name="min">Мин.длина.</param>
        /// <param name="max">Макс.длина.</param>
        /// <returns>Этот же объект.</returns>
        public ValidatorBuilder ValidateLastName(int min, int max)
        {
            this.validators.Add(new LastNameValidator(min, max));
            return this;
        }

        /// <summary>
        /// Валидация даты рождения.
        /// </summary>
        /// <param name="from">Мин.дата.</param>
        /// <param name="to">Макс.дата.</param>
        /// <returns>Этот же объект.</returns>
        public ValidatorBuilder ValidateBirthDate(DateTime from, DateTime to)
        {
            this.validators.Add(new DateOfBirthValidator(from, to));
            return this;
        }

        /// <summary>
        /// Валидация роста.
        /// </summary>
        /// <param name="min">Мин.рост.</param>
        /// <param name="max">Макс.рост.</param>
        /// <returns>Этот же объект.</returns>
        public ValidatorBuilder ValidateHeight(short min, short max)
        {
            this.validators.Add(new HeightValidator(min, max));
            return this;
        }

        /// <summary>
        /// Валидация заработной платы.
        /// </summary>
        /// <param name="min">Мин.з/п.</param>
        /// <returns>Этот же объект.</returns>
        public ValidatorBuilder ValidateWage(decimal min)
        {
            this.validators.Add(new WageValidator(min));
            return this;
        }

        /// <summary>
        /// Валидация числа.
        /// </summary>
        /// <param name="min">Мин.число.</param>
        /// <param name="max">Макс.число.</param>
        /// <param name="four">Неожидаемое число.</param>
        /// <returns>Этот же объект.</returns>
        public ValidatorBuilder ValidateNumeral(char min, char max, char four)
        {
            this.validators.Add(new FavouriteNumeralValidator(min, max, four));
            return this;
        }
    }
}
