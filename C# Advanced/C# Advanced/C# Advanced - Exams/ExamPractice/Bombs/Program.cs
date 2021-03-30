using System;
using System.Collections.Generic;
using System.Linq;

namespace Bombs
{
    class Program
    {
        public static SortedDictionary<string, int> bombs;
        public static Stack<int> bombCasing;
        public static Queue<int> bombEffect;
        static void Main(string[] args)
        {
            bombEffect = new Queue<int>(Console.ReadLine().Split(", ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse));
            bombCasing = new Stack<int>(Console.ReadLine().Split(", ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse));
            bombs = new SortedDictionary<string, int>()
            {
                {"Datura Bombs", 0},
                {"Cherry Bombs", 0},
                {"Smoke Decoy Bombs", 0}

            };

            while (bombEffect.Any() && bombCasing.Any())
            {
                var currentBombEffect = bombEffect.Peek();
                var currentBombCasing = bombCasing.Peek();
                var currentBombSum = currentBombEffect + currentBombCasing;

                if (currentBombSum<40)
                {
                    bombCasing.Pop();
                }
                else
                {
                    CheckForBomb(currentBombSum);
                }

                if (bombs.Count(b => b.Value>=3)==3)
                {
                    Console.WriteLine("Bene! You have successfully filled the bomb pouch!");
                    PrintResult();
                    Environment.Exit(0);
                }
            }

            Console.WriteLine("You don't have enough materials to fill the bomb pouch.");
            PrintResult();
        }

        private static void CheckForBomb(int sum)
        {
            var newSum = sum - 5;
            switch (sum)
            {
                case 40:
                    MakeBomb("Datura Bombs");
                    break;
                case 60:
                    MakeBomb("Cherry Bombs");
                    break;
                case 120:
                    MakeBomb("Smoke Decoy Bombs");
                    break;
                default:
                    CheckForBomb(newSum);
                    break;
            }
        }
        private static void MakeBomb(string bomb)
        {
            bombs[bomb]++;
            bombEffect.Dequeue();
            bombCasing.Pop();
        }

        private static void PrintResult()
        {
            Console.WriteLine(bombEffect.Any() ? $"Bomb Effects: {string.Join(", ", bombEffect)}" : "Bomb Effects: empty");
            Console.WriteLine(bombCasing.Any() ? $"Bomb Casings: {string.Join(", ", bombCasing)}" : "Bomb Casings: empty");

            foreach (var bomb in bombs)
            {
                Console.WriteLine($"{bomb.Key}: {bomb.Value}");
            }
        }
    }
}
