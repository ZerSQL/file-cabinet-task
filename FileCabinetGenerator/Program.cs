using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using FileCabinetApp;

namespace FileCabinetGenerator
{
    public class Program
    {
        public static void Main(string[] arg)
        {
            List<FileCabinetRecord> records = new List<FileCabinetRecord>();
            Random rnd = new Random();
            DateTime currentTime = DateTime.Now;
            string outputType = string.Empty;
            string path = string.Empty;
            string pathDirectory = string.Empty;
            int recordsAmount = 0;
            int startId = 0;
            string[] args = {"-t", "xml", "-o", "d:\\records.xml", "-a", "14", "-i", "45" };
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].Contains("--output-type=", StringComparison.InvariantCultureIgnoreCase))
                {
                    outputType = args[i].Substring(14);
                }
                else if (args[i].Equals("-t", StringComparison.InvariantCultureIgnoreCase))
                {
                    outputType = args[i + 1];
                }
                else if (args[i].Contains("--output=", StringComparison.InvariantCultureIgnoreCase))
                {
                    path = args[i].Substring(9);
                }
                else if (args[i].Equals("-o", StringComparison.InvariantCultureIgnoreCase))
                {
                    path = args[i + 1];
                }
                else if (args[i].Contains("--records-amount=", StringComparison.InvariantCultureIgnoreCase))
                {
                    Int32.TryParse(args[i].Substring(17), out recordsAmount);
                }
                else if (args[i].Equals("-a", StringComparison.InvariantCultureIgnoreCase))
                {
                    Int32.TryParse(args[i + 1], out recordsAmount);
                }
                else if (args[i].Contains("--start-id=", StringComparison.InvariantCultureIgnoreCase))
                {
                    Int32.TryParse(args[i].Substring(11), out startId);
                }
                else if (args[i].Equals("-i", StringComparison.InvariantCultureIgnoreCase))
                {
                    Int32.TryParse(args[i + 1], out startId);
                }
            }
            records = GenerateRecords(startId, recordsAmount);

            if (path.Contains('\\', StringComparison.InvariantCulture) && !path.EndsWith("/", StringComparison.InvariantCulture))
            {
                pathDirectory = path.Substring(0, path.LastIndexOf("\\", StringComparison.InvariantCulture));
            }
            if (outputType.Equals("csv",StringComparison.InvariantCultureIgnoreCase))
            {
                if (Directory.Exists(pathDirectory))
                {
                    if (File.Exists(path))
                    {
                        Console.WriteLine($"File is exist - rewrite {path}? [y/n]");
                    M:
                        switch (Console.ReadLine())
                        {
                            case "y":
                                using (StreamWriter writer = new StreamWriter(path))
                                {
                                    writer.WriteLine("Id, First Name, Last Name, Date of Birth, Wage, Height, Favourite numeral");
                                    if (records != null)
                                    {
                                        foreach (FileCabinetRecord record in records)
                                        {
                                            writer.Write($"{record.Id},{record.FirstName},{record.LastName},{record.DateOfBirth.ToShortDateString()},{record.Wage},{record.Height},{record.FavouriteNumeral}");
                                            writer.WriteLine();
                                        }
                                    }
                                    Console.WriteLine($"{recordsAmount} records were written to {path}.");
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
                        using (StreamWriter writer = new StreamWriter(path))
                        {
                            writer.WriteLine("Id, First Name, Last Name, Date of Birth, Wage, Height, Favourite numeral");
                            if (records != null)
                            {
                                foreach (FileCabinetRecord record in records)
                                {
                                    writer.Write($"{record.Id},{record.FirstName},{record.LastName},{record.DateOfBirth.ToShortDateString()},{record.Wage},{record.Height},{record.FavouriteNumeral}");
                                    writer.WriteLine();
                                }
                            }
                            Console.WriteLine($"{recordsAmount} records were written to {path}.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Directory not exists");
                }
            }
            else if (outputType.Equals("xml", StringComparison.InvariantCultureIgnoreCase))
            {
                if (Directory.Exists(pathDirectory) || Path.GetExtension(path) == ".xml")
                {
                    XmlSerializer formatter = new XmlSerializer(typeof(FileCabinetRecord));
                    if (File.Exists(path))
                    {
                        Console.WriteLine($"File is exist - rewrite {path}? [y/n]");
                    M:
                        switch (Console.ReadLine())
                        {
                            case "y":
                                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                                {
                                    foreach (var record in records)
                                    {
                                        formatter.Serialize(fs, record);
                                    }
                                    Console.WriteLine($"{recordsAmount} records were written to {path}.");
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
                        using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                        {
                            foreach (var record in records)
                            {
                                formatter.Serialize(fs, record);
                            }
                            Console.WriteLine($"{recordsAmount} records were written to {path}.");
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
                Console.WriteLine("Record weren't written. Check entered data.");
            }
        }

        private static List<FileCabinetRecord> GenerateRecords(int startId, int recordsAmount)
        {
            List<FileCabinetRecord> records = new List<FileCabinetRecord>();
            Random rnd = new Random();

            for (int i = startId; i < recordsAmount + startId; i++)
            {
                FileCabinetRecord record = new FileCabinetRecord();
                record.Id = i;
                record.FirstName = GenRandomString(rnd.Next(2, 60));
                record.LastName = GenRandomString(rnd.Next(2, 60));
                record.Height = (short)rnd.Next(120, 250);
                record.Wage = rnd.Next(300, Int32.MaxValue);
                record.FavouriteNumeral = Char.Parse(rnd.Next(0, 9).ToString());
                int year = rnd.Next(1950, DateTime.Now.Year);
                int month = rnd.Next(1, 12);
                int day = rnd.Next(1, DateTime.DaysInMonth(year, month));
                DateTime dateTime = new DateTime(year, month, day);
                record.DateOfBirth = dateTime;
                records.Add(record);
            }

            return records;
        }

        private static string GenRandomString(int length)
        {
            Random rnd = new Random();
            string alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            StringBuilder sb = new StringBuilder(length - 1);
            int Position = 0;

            for (int i = 0; i < length; i++)
            {
                Position = rnd.Next(0, alphabet.Length - 1);
                sb.Append(alphabet[Position]);
            }
            return sb.ToString();
        }
    }
}
