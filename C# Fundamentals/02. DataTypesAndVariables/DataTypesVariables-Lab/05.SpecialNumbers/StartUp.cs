using System;

namespace _05.SpecialNumbers
{
    class StartUp
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            for (int num = 1; num <= n; num++)
            {
                int sumOfDigits = 0;
                int digits = num;
                bool special = false;

                while (digits > 0)
                {
                    sumOfDigits += digits % 10;
                    digits = digits / 10;
                }

                if (sumOfDigits == 7 || sumOfDigits == 11 || sumOfDigits == 5)
                {
                    special = true;
                }

                Console.WriteLine($"{num} -> {special}");
            }
        }
    }
}
