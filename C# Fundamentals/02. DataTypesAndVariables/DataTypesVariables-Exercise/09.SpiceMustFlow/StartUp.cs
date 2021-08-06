using System;

namespace _09.SpiceMustFlow
{
    class StartUp
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int spice = 0;
            int days = 0;

            while (n >= 100)
            {
                spice += n - 26;
                days++;
                n -= 10;
            }

            Console.WriteLine(days);

            if (spice - 26 >= 0)
            {
                Console.WriteLine(spice - 26);
            }
            else
            {
                Console.WriteLine(0);
            }
        }
    }
}
