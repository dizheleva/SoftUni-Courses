using System;
using System.Linq;

namespace _03._Custom_Min_Function
{
    class Program
    {
        static void Main(string[] args)
        {
            Func<string, int> printSmallestNumber = x => x.Split().Select(int.Parse).Min();
            Console.WriteLine(printSmallestNumber(Console.ReadLine()));
        }
    }
}
