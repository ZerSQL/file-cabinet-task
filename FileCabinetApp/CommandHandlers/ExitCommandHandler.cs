using System;
using System.Collections.Generic;
using System.Text;

namespace FileCabinetApp.CommandHandlers
{
    /// <summary>
    /// Класс, представляющий обработчик команды выхода.
    /// </summary>
    public class ExitCommandHandler : CommandHandlerBase
    {
        private static Action<bool> action;

        public ExitCommandHandler(Action<bool> isRunning)
        {
            action = isRunning;
        }

        /// <summary>
        /// Обработчкик команды выхода.
        /// </summary>
        /// <param name="request">Запрос.</param>
        public override void Handle(AppCommandRequest request)
        {
            if (request != null)
            {
                if (request.Command.Equals("Exit", comparisonType: StringComparison.InvariantCultureIgnoreCase))
                {
                    Exit(request.Parameters);
                }
                else
                {
                    this.nextHandler.Handle(request);
                }
            }
        }

        private static void Exit(string parameters)
        {
            Console.WriteLine("Exiting an application...");
            action(default(bool));
        }
    }
}
