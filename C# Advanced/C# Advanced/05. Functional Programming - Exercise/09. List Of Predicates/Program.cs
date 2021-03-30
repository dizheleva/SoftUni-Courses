using System;
using System.Collections.Generic;
using System.Linq;

namespace _09._List_Of_Predicates
{
    class Program
    {
        static void Main(string[] args)
        {
            Func<int, int, bool> checkDivision = (x, y) => x % y != 0;

            var range = int.Parse(Console.ReadLine());
            var dividers = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();

            var result = new List<int>();

            for (var i = 1; i <= range; i++)
            {
                result.Add(i);
            }

            foreach (var divider in dividers)
            {
                result.RemoveAll(x => checkDivision(x, divider));
            }

            Console.WriteLine(string.Join(" ", result));
        }
    }
}
