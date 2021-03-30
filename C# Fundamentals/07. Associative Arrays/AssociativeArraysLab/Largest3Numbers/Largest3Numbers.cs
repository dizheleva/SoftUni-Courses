using System;
using System.Linq;

namespace Largest3Numbers
{
    class Largest3Numbers
    {
        static void Main(string[] args)
        {
            var numbers = Console.ReadLine().Split().Select(int.Parse).OrderByDescending(n => n).ToArray();

            var count = numbers.Length >= 3 ? 3 : numbers.Length;

            for (var i = 0; i < count; i++)
            {
                Console.Write($"{numbers[i]} ");
            }
        }
    }
}
