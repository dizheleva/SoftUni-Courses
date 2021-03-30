using System;
using System.Collections.Generic;

namespace _02._Generic_Box_of_Integer
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());
            var elements = new List<int>();

            for (var i = 0; i < n; i++)
            {
                var input = int.Parse(Console.ReadLine());
                elements.Add(input);
            }

            var box = new Box<int>(elements);
            Console.WriteLine(box.ToString());
        }
    }
}
