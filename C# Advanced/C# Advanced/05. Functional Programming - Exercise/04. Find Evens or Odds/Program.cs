using System;
using System.Collections.Generic;
using System.Linq;

namespace _04._Find_Evens_or_Odds
{
    class Program
    {
        static void Main(string[] args)
        {
            Predicate<int> onlyEvenNumbers = x => x % 2 == 0;

            var input = Console.ReadLine().Split();
            var lowerBound = int.Parse(input[0]);
            var upperBound = int.Parse(input[1]);

            var numbers = new List<int>();

            for (var i = lowerBound; i <= upperBound; i++)
            {
                numbers.Add(i);
            }

            var command = Console.ReadLine();

            switch (command)
            {
                case "even":
                    numbers.RemoveAll(x => !onlyEvenNumbers(x));
                    break;
                case "odd":
                    numbers.RemoveAll(onlyEvenNumbers);
                    break;
            }

            Console.WriteLine(string.Join(" ", numbers));
        }
    }
}
