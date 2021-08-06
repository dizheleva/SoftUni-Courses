using System;

namespace _07.WaterOverflow
{
    class StartUp
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int sum = 0;

            while (n > 0)
            {
                int input = int.Parse(Console.ReadLine());

                if (sum + input <= 255)
                {
                    sum += input;
                }
                else
                {
                    Console.WriteLine("Insufficient capacity!");
                }

                n--;
            }

            Console.WriteLine(sum);
        }
    }
}
