using System;
using System.Collections.Generic;
using System.Linq;

namespace _05._Generic_Count_Method_String
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

            var targetElement = Console.ReadLine();
            var box = new Box<string>(elements);
            var result = box.CountElements(elements, targetElement);
            Console.WriteLine(result);
        }
    }
}
