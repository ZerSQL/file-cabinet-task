using System;
using System.Collections.Generic;
using System.Text;
using FileCabinetApp;

namespace FileCabinetGenerator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            List<FileCabinetRecord> records = new List<FileCabinetRecord>();
            Random rnd = new Random();
            DateTime currentTime = DateTime.Now;
            string outputType = string.Empty;
            string path = string.Empty;
            int recordsAmount = 0;
            int startId = 0;
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].Contains("--output-type=", StringComparison.InvariantCultureIgnoreCase))
                {
                    outputType = args[i].Substring(14);
                }
                else if (args[i].Equals("t", StringComparison.InvariantCultureIgnoreCase))
                {
                    outputType = args[i + 1];
                }
                else if (args[i].Contains("--output=", StringComparison.InvariantCultureIgnoreCase))
                {
                    path = args[i].Substring(9);
                }
                else if (args[i].Equals("o", StringComparison.InvariantCultureIgnoreCase))
                {
                    path = args[i + 1];
                }
                else if (args[i].Contains("--records-amount=", StringComparison.InvariantCultureIgnoreCase))
                {
                    Int32.TryParse(args[i].Substring(17), out recordsAmount);
                }
                else if (args[i].Equals("a", StringComparison.InvariantCultureIgnoreCase))
                {
                    Int32.TryParse(args[i + 1], out recordsAmount);
                }
                else if (args[i].Contains("--start-id=", StringComparison.InvariantCultureIgnoreCase))
                {
                    Int32.TryParse(args[i].Substring(11), out startId);
                }
                else if (args[i].Equals("i", StringComparison.InvariantCultureIgnoreCase))
                {
                    Int32.TryParse(args[i + 1], out startId);
                }
            }

            records = GenerateRecords(startId, recordsAmount);
            Console.WriteLine($"{recordsAmount} records were written to {path}.");
            // foreach (var t in records)
            // {
            //     Console.WriteLine($"{t.Id},{t.LastName},{t.FirstName},{t.Wage},{t.FavouriteNumeral},{t.DateOfBirth.ToShortDateString()}");
            // }
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
