using System;
using System.Linq;

namespace _07._Predicate_For_Names
{
    class Program
    {
        static void Main(string[] args)
        {
            Func<string, int, bool> alowedLenght = (x, y) => x.Length <= y;

            var maxLenght = int.Parse(Console.ReadLine());

            Console.ReadLine()
                .Split().Where(x => alowedLenght(x, maxLenght))
                .ToList()
                .ForEach(x => Console.WriteLine(x));
        }
    }
}
