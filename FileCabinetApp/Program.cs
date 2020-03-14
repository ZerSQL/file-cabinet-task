using System;
using System.Globalization;

namespace FileCabinetApp
{
    public static class Program
    {
        private const string DeveloperName = "Andrei Drabliankou";
        private const string HintMessage = "Enter your command, or enter 'help' to get help.";
        private const int CommandHelpIndex = 0;
        private const int DescriptionHelpIndex = 1;
        private const int ExplanationHelpIndex = 2;

        private static bool isRunning = true;

        private static Tuple<string, Action<string>>[] commands = new Tuple<string, Action<string>>[]
        {
            new Tuple<string, Action<string>>("help", PrintHelp),
            new Tuple<string, Action<string>>("stat", Stat),
            new Tuple<string, Action<string>>("list", List),
            new Tuple<string, Action<string>>("create", Create),
            new Tuple<string, Action<string>>("exit", Exit),
        };

        private static string[][] helpMessages = new string[][]
        {
            new string[] { "help", "prints the help screen", "The 'help' command prints the help screen." },
            new string[] { "stat", "prints count of notes", "The 'stat' prints count of notes." },
            new string[] { "list", "prints notes", "The 'list' print notes." },
            new string[] { "create", "create new note", "The 'create' creates new note." },
            new string[] { "exit", "exits the application", "The 'exit' command exits the application." },
        };

        private static FileCabinetService fileCabinetService = new FileCabinetService();

        public static void Main(string[] args)
        {
            Console.WriteLine($"File Cabinet Application, developed by {Program.DeveloperName}");
            Console.WriteLine(Program.HintMessage);
            Console.WriteLine();

            do
            {
                Console.Write("> ");
                var inputs = Console.ReadLine().Split(' ', 2);
                const int commandIndex = 0;
                var command = inputs[commandIndex];

                if (string.IsNullOrEmpty(command))
                {
                    Console.WriteLine(Program.HintMessage);
                    continue;
                }

                var index = Array.FindIndex(commands, 0, commands.Length, i => i.Item1.Equals(command, StringComparison.InvariantCultureIgnoreCase));
                if (index >= 0)
                {
                    const int parametersIndex = 1;
                    var parameters = inputs.Length > 1 ? inputs[parametersIndex] : string.Empty;
                    commands[index].Item2(parameters);
                }
                else
                {
                    PrintMissedCommandInfo(command);
                }
            }
            while (isRunning);
        }

        private static void PrintMissedCommandInfo(string command)
        {
            Console.WriteLine($"There is no '{command}' command.");
            Console.WriteLine();
        }

        private static void PrintHelp(string parameters)
        {
            if (!string.IsNullOrEmpty(parameters))
            {
                var index = Array.FindIndex(helpMessages, 0, helpMessages.Length, i => string.Equals(i[Program.CommandHelpIndex], parameters, StringComparison.InvariantCultureIgnoreCase));
                if (index >= 0)
                {
                    Console.WriteLine(helpMessages[index][Program.ExplanationHelpIndex]);
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
                    Console.WriteLine("\t{0}\t- {1}", helpMessage[Program.CommandHelpIndex], helpMessage[Program.DescriptionHelpIndex]);
                }
            }

            Console.WriteLine();
        }

        private static void List(string parameters)
        {
            if (fileCabinetService.GetRecords().Length > 0)
            {
                foreach (var t in fileCabinetService.GetRecords())
                {
                    Console.WriteLine($"#{t.Id}, {t.FirstName}, {t.LastName}, {t.DateOfBirth.ToShortDateString()}, height: {t.Height}, wage: {t.Wage}, favourite number: {t.FavouriteNumber}");
                }
            }
            else
            {
                Console.WriteLine("No any notes");
            }
        }

        private static void Create(string parameters)
        {
            bool exact = false;
            decimal personalWage = 0;
            short personalHeight = 0;
            char favouriteNumber = ' ';
            Console.WriteLine("Input first name");
            string firstName = Console.ReadLine();
            Console.WriteLine("Input last name");
            string lastName = Console.ReadLine();

            while (!exact)
            {
                Console.WriteLine("Input Wage");
                exact = decimal.TryParse(Console.ReadLine(), out decimal wage);
                personalWage = wage;
            }

            exact = false;
            while (!exact)
            {
                Console.WriteLine("Input Height");
                exact = short.TryParse(Console.ReadLine(), out short height);
                personalHeight = height;
                if (height < 120 || height > 250)
                {
                    exact = false;
                }
            }

            exact = false;
            while (!exact)
            {
                Console.WriteLine("Input favouriteNumber");
                exact = char.TryParse(Console.ReadLine(), out char number);
                favouriteNumber = number;
                if (favouriteNumber < '0' || favouriteNumber > '9')
                {
                    exact = false;
                }
            }

            fileCabinetService.CreateRecord(firstName, lastName, InputBirthDate(), personalWage, favouriteNumber, personalHeight);
        }

        private static void Stat(string parameters)
        {
            var recordsCount = Program.fileCabinetService.GetStat();
            Console.WriteLine($"{recordsCount} record(s).");
        }

        private static void Exit(string parameters)
        {
            Console.WriteLine("Exiting an application...");
            isRunning = false;
        }

        private static DateTime InputBirthDate()
        {
            DateTime dob;
            string input;

            do
            {
                Console.WriteLine("Input birth date in dd.MM.yyyy format (day.month.year):");
                input = Console.ReadLine();
            }
            while (!DateTime.TryParseExact(input, "dd.MM.yyyy", null, DateTimeStyles.None, out dob));

            return dob;
        }
    }
}