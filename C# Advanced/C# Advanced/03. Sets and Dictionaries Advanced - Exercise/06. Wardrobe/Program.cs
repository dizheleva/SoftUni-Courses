using System;
using System.Collections.Generic;
using System.Linq;

namespace _06._Wardrobe
{
    class Program
    {
        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());
            var wardrobe = new Dictionary<string, Dictionary<string, int>>();

            for (var i = 0; i < n; i++)
            {
                var input = Console.ReadLine().Split(" -> ");
                var color = input[0];
                var clothes = input[1].Split(',').ToArray();
                if (!wardrobe.ContainsKey(color))
                {
                    wardrobe[color] = new Dictionary<string, int>();
                }
                foreach (var cloth in clothes)
                {
                    if (!wardrobe[color].ContainsKey(cloth))
                    {
                        wardrobe[color].Add(cloth, 0);
                    }

                    wardrobe[color][cloth]++;
                }
            }

            var wantedCloth = Console.ReadLine().Split();
            foreach (var item in wardrobe)
            {
                Console.WriteLine($"{item.Key} clothes:");
                foreach (var clothCount in item.Value)
                {
                    Console.Write($"* {clothCount.Key} - {clothCount.Value}");
                    if (item.Key == wantedCloth[0] && clothCount.Key == wantedCloth[1])
                    {
                        Console.WriteLine(" (found!)");
                    }
                    else
                    {
                        Console.WriteLine();
                    }
                }
            }
        }
    }
}
