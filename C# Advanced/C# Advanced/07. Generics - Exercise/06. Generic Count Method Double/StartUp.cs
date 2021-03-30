using System;
using System.Collections.Generic;
using System.Linq;

namespace _06._Generic_Count_Method_Double
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());
            var elements = new List<double>();

            for (var i = 0; i < n; i++)
            {
                var input = double.Parse(Console.ReadLine());
                elements.Add(input);
            }

            var targetElement = double.Parse(Console.ReadLine());
            var box = new Box<double>(elements);
            var result = box.CountElements(elements, targetElement);
            Console.WriteLine(result);
        }
    }
}
