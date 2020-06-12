using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace FileCabinetApp.CommandHandlers
{
    /// <summary>
    /// Класс, представляющий обработчик команды экспорта.
    /// </summary>
    public class ExportCommandHandler : ServiceCommandHandlerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExportCommandHandler"/> class.
        /// </summary>
        /// <param name="service">Используемый сервис.</param>
        public ExportCommandHandler(IFileCabinetService service)
            : base(service)
        {
        }

        /// <summary>
        /// Обработчкик команды экспорта.
        /// </summary>
        /// <param name="request">Запрос.</param>
        public override void Handle(AppCommandRequest request)
        {
            if (request != null)
            {
                if (request.Command.Equals("Export", comparisonType: StringComparison.InvariantCultureIgnoreCase))
                {
                    this.Export(request.Parameters);
                }
                else
                {
                    this.nextHandler.Handle(request);
                }
            }
        }

        private void Export(string parameters)
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
                                    this.service.MakeSnapshot().SaveToCsv(writer);
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
                            this.service.MakeSnapshot().SaveToCsv(writer);
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
                                    this.service.MakeSnapshot().SaveToXml(writer);
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
                            this.service.MakeSnapshot().SaveToXml(writer);
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
    }
}
