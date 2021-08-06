using System;

namespace _02.PoundsToDollars
{
    class StartUp
    {
        static void Main(string[] args)
        {
            decimal num = decimal.Parse(Console.ReadLine());
            decimal result = 1.31m * num;

            Console.WriteLine($"{result:f3}");
        }
    }
}
