using System;

namespace _01.ConvertMetersToKilometers
{
    class StartUp
    {
        static void Main(string[] args)
        {
            decimal meters = decimal.Parse(Console.ReadLine());
            decimal kilometers = meters / 1000.0m;

            Console.WriteLine($"{kilometers:f2}");
        }
    }
}
