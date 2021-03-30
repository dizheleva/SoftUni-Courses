using System;
using System.Collections.Generic;
using System.Linq;

namespace _12._Cups_and_Bottles
{
    class Program
    {
        static void Main(string[] args)
        {
            var cups = new Queue<int>(Console.ReadLine()
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse));

            var bottles = new Stack<int>(Console.ReadLine()
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse));

            var wastedWater = 0;

            while (true)
            {
                if (!bottles.Any() || !cups.Any())
                {
                    break;
                }

                var currentCup = cups.Dequeue();
                var currentBottle = bottles.Pop();

                while (currentCup > currentBottle)
                {
                    currentCup -= currentBottle;
                    currentBottle = bottles.Any() ? bottles.Pop() : 0;
                }

                wastedWater += currentBottle - currentCup;
            }

            Console.WriteLine(cups.Any()
                ? $"Cups: {String.Join(" ", cups)}"
                : $"Bottles: {String.Join(" ", bottles)}");

            Console.WriteLine($"Wasted litters of water: {wastedWater}");
        }
    }
}
