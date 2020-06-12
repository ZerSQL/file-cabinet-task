using System;
using System.Collections.Generic;
using System.Text;

namespace FileCabinetApp.CommandHandlers
{
    /// <summary>
    /// Интерфейс, представляющий цепочку.
    /// </summary>
    public interface ICommandHandler
    {
        /// <summary>
        /// Переход к следующему обработчику.
        /// </summary>
        /// <param name="handler">Установка следующего обработчика.</param>
        void SetNext(ICommandHandler handler);

        /// <summary>
        /// Выполнение команды обработчика.
        /// </summary>
        /// <param name="request">Запрос.</param>
        void Handle(AppCommandRequest request);
    }
}
