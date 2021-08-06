using System;

namespace _10.PokeMon
{
    class StartUp
    {
        static void Main(string[] args)
        {
            int power = int.Parse(Console.ReadLine());
            int distance = int.Parse(Console.ReadLine());
            int exhaustionFactor = int.Parse(Console.ReadLine());

            int pokes = 0;
            decimal powerLeft = power;

            while (powerLeft >= distance)
            {
                pokes++;
                powerLeft -= distance;

                if (powerLeft == (decimal)(0.50 * power) && exhaustionFactor > 0)
                {
                    powerLeft = Math.Floor(powerLeft / exhaustionFactor);
                }
            }

            Console.WriteLine(powerLeft);
            Console.WriteLine(pokes);
        }
    }
}
