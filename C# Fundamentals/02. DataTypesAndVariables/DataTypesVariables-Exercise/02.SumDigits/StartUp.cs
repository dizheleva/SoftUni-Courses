using System;

namespace _02.SumDigits
{
    class StartUp
    {
        static void Main(string[] args)
        {
            int num = int.Parse(Console.ReadLine());
            int sum = 0;

            while (num != 0)
            {
                int digit = num - num / 10 * 10;
                sum += digit;
                num = num / 10;
            }

            Console.WriteLine(sum);
        }
    }
}
