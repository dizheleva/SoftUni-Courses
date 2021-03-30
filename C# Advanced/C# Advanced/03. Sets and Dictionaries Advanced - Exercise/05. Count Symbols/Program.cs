using System;
using System.Collections.Generic;

namespace _05._Count_Symbols
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputText = Console.ReadLine();
            var numbers = new SortedDictionary<char, int>();

            foreach (var sign in inputText)
            {
                if (!numbers.ContainsKey(sign))
                {
                    numbers.Add(sign, 0);
                }
                numbers[sign]++;
            }

            foreach (var sign in numbers)
            {
                Console.WriteLine($"{sign.Key}: {sign.Value} time/s");
            }
        }
    }
}
