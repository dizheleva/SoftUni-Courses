using System;
using System.Linq;

namespace _06._Reverse_And_Exclude
{
    class Program
    {
        static void Main(string[] args)
        {
            Func<int, int, bool> notDivisible = (x, y) => x % y != 0;

            var input = Console.ReadLine();
            var divider = int.Parse(Console.ReadLine());

            input
                .Split()
                .Select(int.Parse)
                .Reverse()
                .Where(x => notDivisible(x, divider))
                .ToList()
                .ForEach(x => Console.Write(x + " "));
            
        }
    }
}
