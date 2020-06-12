using System;
using System.Globalization;
using System.IO;
using FileCabinetApp.CommandHandlers;

namespace FileCabinetApp
{
    /// <summary>
    /// Класс, содержащий основную информацию о программе.
    /// </summary>
    public static class Program
    {
        private const string DeveloperName = "Andrei Drabliankou";
        private const string HintMessage = "Enter your command, or enter 'help' to get help.";
        private static bool isDefaultRules = true;
        private static bool isRunning = true;
        private static IFileCabinetService fileCabinetService;
        private static Action<bool> stopProgram = IsRunning;

        /// <summary>
        /// Точка входа в программу и вызов функционала в зависимости от введенной команды.
        /// </summary>
        /// <param name="args">Переменная для передачи параметров при запуске через консоль.</param>
        public static void Main(string[] args)
        {
            Console.WriteLine($"File Cabinet Application, developed by {Program.DeveloperName}");
            if (args == null)
            {
                fileCabinetService = new FileCabinetMemoryService(new DefaultValidator());
            }
            else
            {
                fileCabinetService = ChooseFileSystem(args);
            }

            Console.WriteLine(Program.HintMessage);
            Console.WriteLine();
            do
            {
                Console.Write("> ");
                var inputs = Console.ReadLine().Split(' ', 2);
                const int commandIndex = 0;
                var command = inputs[commandIndex];
                var commandHandler = CreateCommandHandlers();
                if (string.IsNullOrEmpty(command))
                {
                    Console.WriteLine(Program.HintMessage);
                    continue;
                }

                const int parametersIndex = 1;
                var parameters = inputs.Length > 1 ? inputs[parametersIndex] : string.Empty;
                commandHandler.Handle(new AppCommandRequest
                {
                    Command = command,
                    Parameters = parameters,
                });
            }
            while (isRunning);
        }

        /// <summary>
        /// Метод, выполняющий общие команды создания/редактирования записей.
        /// </summary>
        /// <param name="firstName">Имя.</param>
        /// <param name="lastName">Фамилия.</param>
        /// <param name="dateOfBirth">Дата рождения.</param>
        /// <param name="personalWage">Заработная плата.</param>
        /// <param name="favouriteNumeral">Любимое число.</param>
        /// <param name="personalHeight">Рост.</param>
        internal static void CreateOrEditCommands(out string firstName, out string lastName, out DateTime dateOfBirth, out decimal personalWage, out char favouriteNumeral, out short personalHeight)
        {
            personalWage = 0;
            personalHeight = 0;
            favouriteNumeral = ' ';
            firstName = string.Empty;
            lastName = string.Empty;
            Console.WriteLine("Input Firstname");
            firstName = ReadInput(StringConventer, StringValidator);
            Console.WriteLine("Input Lastname");
            lastName = ReadInput(StringConventer, StringValidator);
            dateOfBirth = InputBirthDate();
            Console.WriteLine("Input wage");
            personalWage = ReadInput(DecimalConventer, DecimalValidator);
            Console.WriteLine("Input height");
            personalHeight = ReadInput(ShortConventer, ShortValidator);
            Console.WriteLine("Input favourite numeral");
            favouriteNumeral = ReadInput(CharConventer, CharValidator);
        }

        private static IFileCabinetService ChooseFileSystem(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                if (string.Equals(args[i], "--storage=file", StringComparison.InvariantCultureIgnoreCase) ||
                    (string.Equals(args[i], "-s", StringComparison.InvariantCultureIgnoreCase) && args[i + 1] != null && string.Equals(args[i + 1], "file", StringComparison.InvariantCultureIgnoreCase)))
                {
                    Console.WriteLine("Using filesystem service.");
                    using (FileStream ftream = new FileStream("cabinet-records.db", FileMode.Append))
                    {
                        return new FileCabinetFilesystemService(ftream);
                    }
                }
            }

            Console.WriteLine("Using memory service.");
            return new FileCabinetMemoryService(ChooseService(args));
        }

        private static IRecordValidator ChooseService(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                if (string.Equals(args[i], "--validation-rules=custom", StringComparison.InvariantCultureIgnoreCase))
                {
                    isDefaultRules = false;
                    Console.WriteLine("Using custom validation rules.");
                    return new CustomValidator();
                }
                else if (string.Equals(args[i], "-v", StringComparison.InvariantCultureIgnoreCase) && args[i + 1] != null && string.Equals(args[i + 1], "custom", StringComparison.InvariantCultureIgnoreCase))
                {
                    isDefaultRules = false;
                    Console.WriteLine("Using custom validation rules.");
                    return new CustomValidator();
                }
            }

            Console.WriteLine("Using default validation rules.");
            return new DefaultValidator();
        }

        private static ICommandHandler CreateCommandHandlers()
        {
            var recordPrinter = new DefaultRecordPrinter();
            var createHandler = new CreateCommandHandler(Program.fileCabinetService);
            var editHandler = new EditCommandHandler(Program.fileCabinetService);
            var exitHandler = new ExitCommandHandler(stopProgram);
            var exportHandler = new ExportCommandHandler(Program.fileCabinetService);
            var findHandler = new FindCommandHandler(
                Program.fileCabinetService, recordPrinter);
            var helpHandler = new HelpCommandHandler();
            var importHandler = new ImportCommandHandler(Program.fileCabinetService);
            var listHandler = new ListCommandHandler(
                Program.fileCabinetService, recordPrinter);
            var purgeHandler = new PurgeCommandHandler(Program.fileCabinetService);
            var removeHandler = new RemoveCommandHandler(Program.fileCabinetService);
            var statHandler = new StatCommandHandler(Program.fileCabinetService);
            var defaultHandler = new DefaultHandler();
            createHandler.SetNext(editHandler);
            editHandler.SetNext(exitHandler);
            exitHandler.SetNext(exportHandler);
            exportHandler.SetNext(findHandler);
            findHandler.SetNext(helpHandler);
            helpHandler.SetNext(importHandler);
            importHandler.SetNext(listHandler);
            listHandler.SetNext(purgeHandler);
            purgeHandler.SetNext(removeHandler);
            removeHandler.SetNext(statHandler);
            statHandler.SetNext(defaultHandler);
            return createHandler;
        }

        private static void IsRunning(bool isRunning)
        {
            Program.isRunning = false;
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
            while (!DateTime.TryParseExact(input, "dd.MM.yyyy", null, DateTimeStyles.None, out dob) || (dob > DateTime.Now || dob < new DateTime(1950, 1, 1)));

            return dob;
        }

        private static Tuple<bool, string, string> StringConventer(string str = "")
        {
            if (str == null)
            {
                return new Tuple<bool, string, string>(false, "string is empty", string.Empty);
            }
            else
            {
                return new Tuple<bool, string, string>(true, string.Empty, str);
            }
        }

        private static Tuple<bool, string, decimal> DecimalConventer(string str = "")
        {
            if (decimal.TryParse(str, out decimal wage))
            {
                return new Tuple<bool, string, decimal>(true, string.Empty, wage);
            }
            else
            {
                return new Tuple<bool, string, decimal>(false, "This is not a number", 0);
            }
        }

        private static Tuple<bool, string, char> CharConventer(string symbol)
        {
            if (char.TryParse(symbol, out char number))
            {
                return new Tuple<bool, string, char>(true, string.Empty, number);
            }
            else
            {
                return new Tuple<bool, string, char>(false, "this is not a char", ' ');
            }
        }

        private static Tuple<bool, string, short> ShortConventer(string height)
        {
            if (short.TryParse(height, out short personalHeight))
            {
                return new Tuple<bool, string, short>(true, string.Empty, personalHeight);
            }
            else
            {
                return new Tuple<bool, string, short>(false, "cannot cast to a short,", 0);
            }
        }

        private static Tuple<bool, string> StringValidator(string str = "")
        {
            if (isDefaultRules == true)
            {
                if (str.Length < 2 || str.Length > 60 || str.Contains(' ', StringComparison.CurrentCulture))
                {
                    return new Tuple<bool, string>(false, "string is too short or too long");
                }
                else
                {
                    return new Tuple<bool, string>(true, string.Empty);
                }
            }
            else
            {
                if (str.Length < 2 || str.Length > 15 || str.Contains(' ', StringComparison.CurrentCulture))
                {
                    return new Tuple<bool, string>(false, "string is too short or too long");
                }
                else
                {
                    return new Tuple<bool, string>(true, string.Empty);
                }
            }
        }

        private static Tuple<bool, string> DecimalValidator(decimal wage)
        {
            if (isDefaultRules == true)
            {
                if (wage < 300)
                {
                    return new Tuple<bool, string>(false, "wage is too small");
                }
                else
                {
                    return new Tuple<bool, string>(true, string.Empty);
                }
            }
            else
            {
                if (wage < 150)
                {
                    return new Tuple<bool, string>(false, "wage is too small");
                }
                else
                {
                    return new Tuple<bool, string>(true, string.Empty);
                }
            }
        }

        private static Tuple<bool, string> CharValidator(char symbol)
        {
            if (isDefaultRules == true)
            {
                if (symbol < '0' || symbol > '9')
                {
                    return new Tuple<bool, string>(false, "this is not a numeral");
                }
                else
                {
                    return new Tuple<bool, string>(true, string.Empty);
                }
            }
            else
            {
                if (symbol < '0' || symbol > '9' || symbol == '4')
                {
                    return new Tuple<bool, string>(false, "this is not a numeral or this is 4");
                }
                else
                {
                    return new Tuple<bool, string>(true, string.Empty);
                }
            }
        }

        private static Tuple<bool, string> ShortValidator(short height)
        {
            if (isDefaultRules == true)
            {
                if (height < 120 || height > 250)
                {
                    return new Tuple<bool, string>(false, "height must be between 120 and 250");
                }
                else
                {
                    return new Tuple<bool, string>(true, string.Empty);
                }
            }
            else
            {
                if (height < 135 || height > 250)
                {
                    return new Tuple<bool, string>(false, "height must be between 135 and 250");
                }
                else
                {
                    return new Tuple<bool, string>(true, string.Empty);
                }
            }
        }

        private static T ReadInput<T>(Func<string, Tuple<bool, string, T>> converter, Func<T, Tuple<bool, string>> validator)
        {
            do
            {
                T value;

                var input = Console.ReadLine();
                var conversionResult = converter(input);

                if (!conversionResult.Item1)
                {
                    Console.WriteLine($"Conversion failed: {conversionResult.Item2}. Please, correct your input.");
                    continue;
                }

                value = conversionResult.Item3;

                var validationResult = validator(value);
                if (!validationResult.Item1)
                {
                    Console.WriteLine($"Validation failed: {validationResult.Item2}. Please, correct your input.");
                    continue;
                }

                return value;
            }
            while (true);
        }
    }
}