using System;
using System.Collections.Generic;
using System.Linq;

namespace _11._Key_Revolver
{
    class Program
    {
        static void Main(string[] args)
        {
            var price = int.Parse(Console.ReadLine());
            var gunBarrelSize = int.Parse(Console.ReadLine());

            var bullets = new Stack<int>(Console.ReadLine()
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse));

            var locks = new Queue<int>(Console.ReadLine()
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse));

            var intelligenceValue = int.Parse(Console.ReadLine());
            var shots = 0;

            while (bullets.Any() && locks.Any())
            {
                if (locks.Peek() >= bullets.Pop())
                {
                    locks.Dequeue();
                    Console.WriteLine("Bang!");
                    shots++;
                }
                else
                {
                    Console.WriteLine("Ping!");
                    shots++;
                }
                if (shots % gunBarrelSize == 0 && bullets.Any())
                {
                    Console.WriteLine("Reloading!");
                }

                intelligenceValue -= price;
            }

            if (bullets.Count >= 0)
            {
                Console.WriteLine(locks.Any()
                    ? $"Couldn't get through. Locks left: {locks.Count}"
                    : $"{bullets.Count} bullets left. Earned ${intelligenceValue}");
            }
        }
    }
}
