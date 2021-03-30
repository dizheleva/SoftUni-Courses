using System;
using System.Collections.Generic;

namespace _03._Periodic_Table
{
    class Program
    {
        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());
            var elements = new SortedSet<string>();

            for (var i = 0; i < n; i++)
            {
                var input = Console.ReadLine().Split();
                foreach (var element in input)
                {
                    elements.Add(element);
                }
            }

            Console.WriteLine(String.Join(" ", elements));
        }
    }
}
