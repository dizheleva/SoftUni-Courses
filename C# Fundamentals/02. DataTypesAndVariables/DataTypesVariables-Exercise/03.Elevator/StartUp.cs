using System;

namespace _03.Elevator
{
    class StartUp
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int p = int.Parse(Console.ReadLine());

            int result;

            if (n > p)
            {
                int a = n / p;
                int b = n - a * p;

                if (b > 0)
                {
                    result = a + 1;
                }
                else
                {
                    result = a;
                }
            }
            else
            {
                result = 1;
            }

            Console.WriteLine(result);
        }
    }
}
