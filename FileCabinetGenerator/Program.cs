using System;

namespace FileCabinetGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            string outputType = string.Empty;
            string path = string.Empty;
            int recordsAmount = 0;
            int startId = 0;
            string[] args = { "i", "10000" };

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
            Console.WriteLine(startId);
        }
    }
}
