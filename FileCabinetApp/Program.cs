using System;
using System.Globalization;
using System.IO;
using System.Xml;

namespace FileCabinetApp
{
    /// <summary>
    /// Класс, содержащий основную информацию о программе.
    /// </summary>
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
            new Tuple<string, Action<string>>("edit", Edit),
            new Tuple<string, Action<string>>("create", Create),
            new Tuple<string, Action<string>>("find", Find),
            new Tuple<string, Action<string>>("export", Export),
            new Tuple<string, Action<string>>("exit", Exit),
        };

        private static string[][] helpMessages = new string[][]
        {
            new string[] { "help", "prints the help screen", "The 'help' command prints the help screen." },
            new string[] { "stat", "prints count of notes", "The 'stat' prints count of notes." },
            new string[] { "list", "prints notes", "The 'list' print notes." },
            new string[] { "edit", "edit notes", "The 'edit' is to edit notes." },
            new string[] { "create", "create new note", "The 'create' creates new note." },
            new string[] { "find", "find notes", "The 'find' command is to find notes." },
            new string[] { "export csv", "export notes to csv", "The 'export csv' command is to export notes to csv format." },
            new string[] { "exit", "exits the application", "The 'exit' command exits the application." },
        };

        private static bool isDefaultRules = true;
        private static IFileCabinetService fileCabinetService;

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
            using (FileStream ftream = new FileStream("cabinet-records.db", FileMode.Append))
            {
                return new FileCabinetFilesystemService(ftream);
            }
            return new FileCabinetMemoryService(ChooseService(args));
        }

        private static IRecordValidator ChooseService(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                if (string.Equals(args[i], "--validation-rules=custom", StringComparison.InvariantCultureIgnoreCase))
                {
                    Console.WriteLine("Using custom validation rules.");
                    return new CustomValidator();
                }
                else if (string.Equals(args[i], "-v", StringComparison.InvariantCultureIgnoreCase) && args[i + 1] != null && string.Equals(args[i + 1], "custom", StringComparison.InvariantCultureIgnoreCase))
                {
                    Console.WriteLine("Using custom validation rules.");
                    return new CustomValidator();
                }
            }

            Console.WriteLine("Using default validation rules.");
            return new DefaultValidator();
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
            if (fileCabinetService.GetRecords().Count > 0)
            {
                foreach (var t in fileCabinetService.GetRecords())
                {
                    Console.WriteLine($"#{t.Id}, {t.FirstName}, {t.LastName}, {t.DateOfBirth.ToShortDateString()}, height: {t.Height}, wage: {t.Wage}, favourite numeral: {t.FavouriteNumeral}");
                }
            }
            else
            {
                Console.WriteLine("No any notes");
            }
        }

        private static void Create(string parameters)
        {
            CreateOrEditCommands(out string firstName, out string lastName, out DateTime dateOfBirth, out decimal personalWage, out char favouriteNumeral, out short personalHeight);
            FileCabinetRecord newRecord = new FileCabinetRecord() { FirstName = firstName, LastName = lastName, DateOfBirth = dateOfBirth, Wage = personalWage, FavouriteNumeral = favouriteNumeral, Height = personalHeight };
            fileCabinetService.CreateRecord(newRecord);
        }

        private static void Stat(string parameters)
        {
            var recordsCount = Program.fileCabinetService.GetStat();
            Console.WriteLine($"{recordsCount} record(s).");
        }

        private static void Find(string parameters)
        {
            string[] propertyAndValue = parameters.Replace("\"", string.Empty, StringComparison.CurrentCultureIgnoreCase).Split(' ', 2);
            if (propertyAndValue.Length != 2)
            {
                Console.WriteLine("'Find' command working in 'find *name of property* *value*' format. For birthdate use dd.MM.yyyy format.Please,try again.");
                return;
            }

            if (propertyAndValue[0].ToLower(CultureInfo.CurrentCulture) == "firstname")
            {
                foreach (var note in fileCabinetService.FindByFirstName(propertyAndValue[1]))
                {
                    Console.WriteLine($"#{note.Id}, {note.FirstName}, {note.LastName}, {note.DateOfBirth.ToShortDateString()}");
                }
            }
            else if (propertyAndValue[0].ToLower(CultureInfo.CurrentCulture) == "lastname")
            {
                foreach (var note in fileCabinetService.FindByLastName(propertyAndValue[1]))
                {
                    Console.WriteLine($"#{note.Id}, {note.FirstName}, {note.LastName}, {note.DateOfBirth.ToShortDateString()}");
                }
            }
            else if (propertyAndValue[0].ToLower(CultureInfo.CurrentCulture) == "dateofbirth")
            {
                foreach (var note in fileCabinetService.FindByBirthDate(propertyAndValue[1]))
                {
                    Console.WriteLine($"#{note.Id}, {note.FirstName}, {note.LastName}, {note.DateOfBirth.ToShortDateString()}");
                }
            }
            else
            {
                Console.WriteLine("'Find' command working in 'find *name of property* *value*' format. For birthdate use dd.MM.yyyy format.Please,try again.");
            }
        }

        private static void Edit(string parameters)
        {
            bool exact;
            int id;
            while (true)
            {
                Console.WriteLine("Input id of note to be changed or '0' to go to menu");
                exact = int.TryParse(Console.ReadLine(), out id);
                if (exact)
                {
                    if (fileCabinetService.GetStat() >= id)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"#{id} record is not found.");
                    }
                }
            }

            if (id == 0)
            {
                return;
            }

            CreateOrEditCommands(out string firstName, out string lastName, out DateTime dateOfBirth, out decimal personalWage, out char favouriteNumeral, out short personalHeight);
            FileCabinetRecord record = new FileCabinetRecord() { Id = id, FirstName = firstName, LastName = lastName, DateOfBirth = dateOfBirth, Wage = personalWage, FavouriteNumeral = favouriteNumeral, Height = personalHeight };
            fileCabinetService.EditRecord(record);
        }

        private static void Exit(string parameters)
        {
            Console.WriteLine("Exiting an application...");
            isRunning = false;
        }

        private static void Export(string parameters)
        {
            if (parameters.Split(' ').Length < 2)
            {
                Console.WriteLine("Type type and path to export file.");
                return;
            }

            string[] values = parameters.Split(' ', 2);
            string pathDirectory = string.Empty;
            if (values[0].Equals("csv", StringComparison.InvariantCultureIgnoreCase))
            {
                if (values[1].Contains('\\', StringComparison.InvariantCulture) && !values[1].EndsWith("/", StringComparison.InvariantCulture))
                {
                    pathDirectory = values[1].Substring(0, values[1].LastIndexOf("\\", StringComparison.InvariantCulture));
                }

                if (Directory.Exists(pathDirectory) || Path.GetExtension(values[1]) == ".csv")
                {
                    if (File.Exists(values[1]))
                    {
                        Console.WriteLine($"File is exist - rewrite {values[1]}? [y/n]");
                    M:
                        switch (Console.ReadLine())
                        {
                            case "y":
                                using (StreamWriter writer = new StreamWriter(values[1]))
                                {
                                    Program.fileCabinetService.MakeSnapshot().SaveToCsv(writer);
                                    Console.WriteLine($"All records are exported to file {values[1]}");
                                }

                                break;
                            case "n":
                                break;
                            default:
                                Console.WriteLine("Type 'y' or 'n' to continue.");
                                goto M;
                        }
                    }
                    else
                    {
                        using (StreamWriter writer = new StreamWriter(values[1]))
                        {
                            Program.fileCabinetService.MakeSnapshot().SaveToCsv(writer);
                            Console.WriteLine($"All records are exported to file {values[1]}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Directory not exists");
                }
            }
            else if (values[0].Equals("xml", StringComparison.InvariantCultureIgnoreCase))
            {
                if (values[1].Contains('\\', StringComparison.InvariantCulture) && !values[1].EndsWith("/", StringComparison.InvariantCulture))
                {
                    pathDirectory = values[1].Substring(0, values[1].LastIndexOf("\\", StringComparison.InvariantCulture));
                }

                if (Directory.Exists(pathDirectory) || Path.GetExtension(values[1]) == ".xml")
                {
                    if (File.Exists(values[1]))
                    {
                        Console.WriteLine($"File is exist - rewrite {values[1]}? [y/n]");
                    M:
                        switch (Console.ReadLine())
                        {
                            case "y":
                                using (XmlWriter writer = XmlWriter.Create(values[1]))
                                {
                                    Program.fileCabinetService.MakeSnapshot().SaveToXml(writer);
                                    Console.WriteLine($"All records are exported to file {values[1]}");
                                }

                                break;
                            case "n":
                                break;
                            default:
                                Console.WriteLine("Type 'y' or 'n' to continue.");
                                goto M;
                        }
                    }
                    else
                    {
                        using (XmlWriter writer = XmlWriter.Create(values[1]))
                        {
                            Program.fileCabinetService.MakeSnapshot().SaveToXml(writer);
                            Console.WriteLine($"All records are exported to file {values[1]}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Directory not exists");
                }
            }
            else
            {
                Console.WriteLine("Error comand. There is only 'export csv' and 'export xml' available commands.");
            }
        }

        private static void CreateOrEditCommands(out string firstName, out string lastName, out DateTime dateOfBirth, out decimal personalWage, out char favouriteNumeral, out short personalHeight)
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