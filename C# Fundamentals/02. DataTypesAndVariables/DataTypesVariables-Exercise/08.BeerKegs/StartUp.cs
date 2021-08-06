using System;

namespace _08.BeerKegs
{
    class StartUp
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            double max = 0.0;
            string winner = String.Empty;

            while (n > 0)
            {
                string model = Console.ReadLine();
                double radius = double.Parse(Console.ReadLine());
                int height = int.Parse(Console.ReadLine());
                double volume = Math.PI * radius * radius * height;

                if (volume > max)
                {
                    max = volume;
                    winner = model;
                }

                n--;
            }

            Console.WriteLine(winner);
        }
    }
}
