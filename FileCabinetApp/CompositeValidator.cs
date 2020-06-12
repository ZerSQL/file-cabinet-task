using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileCabinetApp
{
    /// <summary>
    /// Компоновщик.
    /// </summary>
    public class CompositeValidator : IRecordValidator
    {
        private List<IRecordValidator> validators;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompositeValidator"/> class.
        /// </summary>
        /// <param name="validators">Все валидаторы.</param>
        public CompositeValidator(IEnumerable<IRecordValidator> validators)
        {
            this.validators = validators.ToList();
        }

        /// <summary>
        /// Метод производящий валидацию всех свойств.
        /// </summary>
        /// <param name="newRecord">Запись, подвергаемая валидации.</param>
        public void ValidateParameters(FileCabinetRecord newRecord)
        {
            foreach (var validator in this.validators)
            {
                validator.ValidateParameters(newRecord);
            }
        }
    }
}
