using System;

namespace _04.SumOfChars
{
    class StartUp
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int totalSum = 0;

            for (int i = 0; i < n; i++)
            {
                char input = char.Parse(Console.ReadLine());
                totalSum += input;
            }

            Console.WriteLine($"The sum equals: {totalSum}");
        }
    }
}
