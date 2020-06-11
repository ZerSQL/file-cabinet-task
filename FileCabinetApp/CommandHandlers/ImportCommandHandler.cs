using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileCabinetApp.CommandHandlers
{
    /// <summary>
    /// Класс, представляющий обработчик команды импорта.
    /// </summary>
    public class ImportCommandHandler : CommandHandlerBase
    {
        /// <summary>
        /// Обработчкик команды импорта.
        /// </summary>
        /// <param name="request">Запрос.</param>
        public override void Handle(AppCommandRequest request)
        {
            if (request != null)
            {
                if (request.Command.Equals("Import", comparisonType: StringComparison.InvariantCultureIgnoreCase))
                {
                    Import(request.Parameters);
                }
                else
                {
                    this.nextHandler.Handle(request);
                }
            }
        }

        private static void Import(string parameters)
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
                if (File.Exists(values[1]))
                {
                    using (FileStream fs = new FileStream(values[1], FileMode.Open))
                    {
                        List<FileCabinetRecord> list = new List<FileCabinetRecord>();
                        FileCabinetServiceSnapshot snap = new FileCabinetServiceSnapshot(list);
                        snap.LoadFromCsv(fs);
                        Program.fileCabinetService.Restore(snap);
                        Console.WriteLine($"Notes has been imported from {values[1]}.");
                    }
                }
                else
                {
                    Console.WriteLine("File not exists");
                }
            }
            else if (values[0].Equals("xml", StringComparison.InvariantCultureIgnoreCase))
            {
                if (File.Exists(values[1]))
                {
                    using (FileStream fs = new FileStream(values[1], FileMode.Open))
                    {
                        List<FileCabinetRecord> list = new List<FileCabinetRecord>();
                        FileCabinetServiceSnapshot snap = new FileCabinetServiceSnapshot(list);
                        snap.LoadFromXml(fs);
                        Program.fileCabinetService.Restore(snap);
                        Console.WriteLine($"Notes has been imported from {values[1]}.");
                    }
                }
                else
                {
                    Console.WriteLine("File not exists");
                }
            }
        }
    }
}
