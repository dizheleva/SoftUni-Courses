using System;
using System.Collections.Generic;
using System.Linq;

namespace _03._Generic_Swap_Method_String
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());
            var elements = new List<string>();

            for (var i = 0; i < n; i++)
            {
                var input = Console.ReadLine();
                elements.Add(input);
            }

            var indexes = Console.ReadLine().Split().Select(int.Parse).ToArray();
            var firstIndex = indexes[0];
            var secondIndex = indexes[1]; 

            var box = new Box<string>(elements);
            box.Swap(elements, firstIndex, secondIndex);
            Console.WriteLine(box.ToString());
        }
    }
}
