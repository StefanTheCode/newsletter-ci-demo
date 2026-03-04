namespace Newsletter.Api
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.Extensions.Logging;

    internal class Program
    {
        private readonly LoggerFactory _loggerFactory = (LoggerFactory)LoggerFactory.Create(b => b.AddConsole()); // should become readonly (IDE0044)

        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("No args"); // missing braces (prefer braces)
            }

            var stream = new MemoryStream(); // var elsewhere not apparent -> should become explicit type

            var data = new List<int>() { 5, 3, 1, 2, 4 };
            data.Sort();

            var program = new Program();
            Console.WriteLine(program.Sum(data));
            Console.WriteLine(program.UserDisplayName("stefan", "djokic"));
        }

        public int Sum(List<int> numbers)
        {
            return numbers.Sum();
        }

        public string UserDisplayName(string first, string last)
        {
            return $"{first} {last}";
        }
    }
}
