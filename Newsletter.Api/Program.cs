// Copyright (c) TheCodeMan. All rights reserved.
// Licensed under the MIT license.

namespace Newsletter.Api
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                var nums = new List<int> { 5, 3, 1 };
                nums.Sort();

                foreach (var num in nums)
                {
                    Console.WriteLine(num);
                }

                Console.WriteLine("No args");
            }

            var data = new List<int>() { 5, 3, 1, 2, 4 };
            data.Sort();

            Console.WriteLine(Sum(data));
            Console.WriteLine(UserDisplayName("stefan", "djokic"));
        }

        private static int Sum(List<int> numbers)
        {
            return numbers.Sum();
        }

        private static string UserDisplayName(string first, string last)
        {
            return $"{first} {last}";
        }
    }
}
