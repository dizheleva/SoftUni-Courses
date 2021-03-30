using System;
using System.Linq;

namespace _01._Action_Print
{
    class Program
    {
        static void Main(string[] args)
        {
            static void Main(string[] args)
            {
                Action<string> printing = PrintLine;
                Console.ReadLine().Split().ToList().ForEach(printing);
            }

            static void PrintLine(string line) => Console.WriteLine(line);
        }
    }
}
