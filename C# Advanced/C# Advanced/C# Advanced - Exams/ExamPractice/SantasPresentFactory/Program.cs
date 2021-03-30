using System;
using System.Collections.Generic;
using System.Linq;

namespace SantasPresentFactory
{
    class Program
    {
        public static SortedDictionary<string, int> toys;
        public static Stack<int> materialsBoxes;
        public static Queue<int> magicValue;
        static void Main(string[] args)
        {
            toys = new SortedDictionary<string, int>();
            materialsBoxes = new Stack<int>(Console.ReadLine().Split(' ').Select(int.Parse));
            magicValue = new Queue<int>(Console.ReadLine().Split(' ').Select(int.Parse));

            while (magicValue.Any() && materialsBoxes.Any())
            {
                if (materialsBoxes.Peek() == 0)
                {
                    materialsBoxes.Pop();
                }

                if (magicValue.Peek() == 0)
                {
                    magicValue.Dequeue();
                }

                if (!magicValue.Any() || !materialsBoxes.Any())
                {
                    break;
                }

                var currentMagicLevel = magicValue.Peek() * materialsBoxes.Peek();

                if (currentMagicLevel < 0)
                {
                    var sum = materialsBoxes.Pop() + magicValue.Dequeue();
                    materialsBoxes.Push(sum);
                }
                else
                {
                    switch (currentMagicLevel)
                    {
                        case 150:
                            MakeToy("Doll");
                            break;
                        case 250:
                            MakeToy("Wooden train");
                            break;
                        case 300:
                            MakeToy("Teddy bear");
                            break;
                        case 400:
                            MakeToy("Bicycle");
                            break;
                        default:
                            magicValue.Dequeue();
                            var box = materialsBoxes.Pop() + 15;
                            materialsBoxes.Push(box);
                            break;
                    }
                }
            }

            Console.WriteLine((toys.ContainsKey("Doll") && toys.ContainsKey("Wooden train")) 
                              || (toys.ContainsKey("Teddy bear") && toys.ContainsKey("Bicycle"))
                ? "The presents are crafted! Merry Christmas!"
                : "No presents this Christmas!");

            if (materialsBoxes.Any())
            {
                Console.WriteLine("Materials left: " + string.Join(", ", materialsBoxes));
            }

            if (magicValue.Any())
            {
                Console.WriteLine("Magic left: " + string.Join(", ", magicValue));
            }

            foreach (var toy in toys)
            {
                Console.WriteLine($"{toy.Key}: {toy.Value}");
            }
        }

        private static void MakeToy(string toy)
        {
            if (!toys.ContainsKey(toy))
            {
                toys.Add(toy, 0);
            }

            toys[toy]++;
            magicValue.Dequeue();
            materialsBoxes.Pop();
        }
    }
}