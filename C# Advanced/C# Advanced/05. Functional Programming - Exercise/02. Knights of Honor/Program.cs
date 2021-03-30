using System;
using System.Linq;

namespace _02._Knights_of_Honor
{
    class Program
    {
        static void Main(string[] args)
        {
            Action<string> printing = PrintLine;
            Console.ReadLine().Split().ToList().ForEach(printing);
        }

        static void PrintLine(string line) => Console.WriteLine("Sir " + line);
    }
}
