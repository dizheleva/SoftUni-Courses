using System;

namespace _03.ExactSumOfRealNumbers
{
    class StartUp
    {
        static void Main(string[] args)
        {
            int num = int.Parse(Console.ReadLine());
            decimal result = 0;

            for (int i = 0; i < num; i++)
            {
                result += decimal.Parse(Console.ReadLine());
            }

            Console.WriteLine($"{result}");
        }
    }
}
