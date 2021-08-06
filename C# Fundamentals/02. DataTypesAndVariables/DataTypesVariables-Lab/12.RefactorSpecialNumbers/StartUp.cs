using System;

namespace _12.RefactorSpecialNumbers
{
    class StartUp
    {
        static void Main(string[] args)
        {
            int number = int.Parse(Console.ReadLine());

            for (int n = 1; n <= number; n++)
            {
                int sumOfDigits = 0;
                int digits = n;
                bool isSpecial = false;

                while (digits > 0)
                {
                    sumOfDigits += digits % 10;
                    digits = digits / 10;
                }

                if (sumOfDigits == 7 || sumOfDigits == 11 || sumOfDigits == 5)
                {
                    isSpecial = true;
                }

                Console.WriteLine($"{n} -> {isSpecial}");
            }
        }
    }
}
