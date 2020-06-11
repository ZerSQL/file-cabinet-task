using System;
using System.Collections.Generic;
using System.Text;

namespace FileCabinetApp.CommandHandlers
{
    /// <summary>
    /// Абстрактный класс(цепочка обязанностей).
    /// </summary>
    public abstract class CommandHandlerBase : ICommandHandler
    {
        /// <summary>
        /// Текущий обработчик.
        /// </summary>
        protected ICommandHandler nextHandler;

        /// <summary>
        /// Абстрактный метод(выполняет команду запроса).
        /// </summary>
        /// <param name="request">Запрос.</param>
        public abstract void Handle(AppCommandRequest request);

        /// <summary>
        /// Метод перехода к следующему обработчику.
        /// </summary>
        /// <param name="handler">Обработчик.</param>
        public void SetNext(ICommandHandler handler)
        {
            this.nextHandler = handler;
        }
    }
}
