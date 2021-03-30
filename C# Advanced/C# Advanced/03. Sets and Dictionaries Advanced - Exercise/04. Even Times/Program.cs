using System;
using System.Collections.Generic;
using System.Linq;

namespace _04._Even_Times
{
    class Program
    {
        static void Main(string[] args)
        {
            var count = int.Parse(Console.ReadLine());
            var numbers = new Dictionary<int, int>();

            for (var i = 0; i < count; i++)
            {
                var number = int.Parse(Console.ReadLine());
                if (!numbers.ContainsKey(number))
                {
                    numbers.Add(number, 0);
                }
                numbers[number]++;
            }

            foreach (var kvp in numbers.Where(kvp => kvp.Value % 2 == 0))
            {
                Console.WriteLine(kvp.Key);
                return;
            }
        }
    }
}
