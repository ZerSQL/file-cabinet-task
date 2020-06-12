using System;
using System.Collections.Generic;
using System.Text;

namespace FileCabinetApp.CommandHandlers
{
    /// <summary>
    /// Класс, представляющий обрабочик команды помощи.
    /// </summary>
    public class HelpCommandHandler : CommandHandlerBase
    {
        private const int CommandHelpIndex = 0;
        private const int DescriptionHelpIndex = 1;
        private const int ExplanationHelpIndex = 2;

        private static string[][] helpMessages = new string[][]
        {
            new string[] { "help", "prints the help screen", "The 'help' command prints the help screen." },
            new string[] { "stat", "prints count of notes", "The 'stat' prints count of notes." },
            new string[] { "list", "prints notes", "The 'list' print notes." },
            new string[] { "edit", "edit notes", "The 'edit' is to edit notes." },
            new string[] { "create", "create new note", "The 'create' creates new note." },
            new string[] { "find", "find notes", "The 'find' command is to find notes." },
            new string[] { "export", "export notes to csv or xml", "The 'export csv || export xml' command is to export notes to csv or xml format." },
            new string[] { "import", "import notes from csv or xml", "The 'import csv || import xml' command is to import notes from csv or xml format." },
            new string[] { "remove", "remove notes from memory", "The 'remove' command is to remove notes from memory." },
            new string[] { "purge", "removes blank spaces in db file", "The 'purge' command is to remove blank spaces in db file." },
            new string[] { "exit", "exits the application", "The 'exit' command exits the application." },
        };

        /// <summary>
        /// Обработчкик команды помощи.
        /// </summary>
        /// <param name="request">Запрос.</param>
        public override void Handle(AppCommandRequest request)
        {
            if (request != null)
            {
                if (request.Command.Equals("Help", comparisonType: StringComparison.InvariantCultureIgnoreCase))
                {
                    PrintHelp(request.Parameters);
                }
                else
                {
                    this.nextHandler.Handle(request);
                }
            }
        }

        private static void PrintHelp(string parameters)
        {
            if (!string.IsNullOrEmpty(parameters))
            {
                var index = Array.FindIndex(helpMessages, 0, helpMessages.Length, i => string.Equals(i[CommandHelpIndex], parameters, StringComparison.InvariantCultureIgnoreCase));
                if (index >= 0)
                {
                    Console.WriteLine(helpMessages[index][ExplanationHelpIndex]);
                }
                else
                {
                    Console.WriteLine($"There is no explanation for '{parameters}' command.");
                }
            }
            else
            {
                Console.WriteLine("Available commands:");

                foreach (var helpMessage in helpMessages)
                {
                    Console.WriteLine("\t{0}\t- {1}", helpMessage[CommandHelpIndex], helpMessage[DescriptionHelpIndex]);
                }
            }

            Console.WriteLine();
        }
    }
}
