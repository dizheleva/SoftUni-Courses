using System;
using System.Collections.Generic;

namespace _01._Generic_Box_of_String
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());
            var elements = new List<string>();

            for (var i = 0; i < n; i++)
            {
                var input = Console.ReadLine();
                elements.Add(input);
            }

            var box = new Box<string>(elements);
            Console.WriteLine(box.ToString());
        }
    }
}
