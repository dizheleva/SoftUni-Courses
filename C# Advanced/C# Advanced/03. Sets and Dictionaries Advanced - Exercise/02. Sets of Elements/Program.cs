using System;
using System.Collections.Generic;
using System.Linq;

namespace _02._Sets_of_Elements
{
    class Program
    {
        static void Main(string[] args)
        {
            var sizes = Console.ReadLine().Split().Select(int.Parse).ToArray();
            var firstSet = new HashSet<int>();
            var secondSet = new HashSet<int>();
            for (var i = 0; i < sizes[0]; i++)
            {
                firstSet.Add(int.Parse(Console.ReadLine()));
            }

            for (var j = 0; j < sizes[1]; j++)
            {
                secondSet.Add(int.Parse(Console.ReadLine()));
            }

            foreach (var number in firstSet.Where(number => secondSet.Contains(number)))
            {
                Console.Write(number + " ");
            }
        }
    }
}
