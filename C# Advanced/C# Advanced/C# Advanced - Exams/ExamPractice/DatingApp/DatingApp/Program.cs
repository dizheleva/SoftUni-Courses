using System;
using System.Collections.Generic;
using System.Linq;

namespace DatingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var males = new Stack<int>(Console.ReadLine().Split(' ').Select(int.Parse));
            var females = new Queue<int>(Console.ReadLine().Split(' ').Select(int.Parse));
            
            var matchesCount = 0;

            while (females.Any() && males.Any())
            {
                if (females.Peek() <= 0)
                {
                    females.Dequeue();
                    continue;
                }
                if (males.Peek() <= 0)
                {
                    males.Pop();
                    continue;
                }
                
                if (males.Peek() % 25 == 0)
                {
                    males.Pop();
                    if (males.Any())
                    {
                        males.Pop();
                    }
                    continue;
                }
                if (females.Peek() % 25 == 0)
                {
                    females.Dequeue();
                    if (females.Any())
                    {
                        females.Dequeue();
                    }
                    continue;
                }
                
                if (females.Peek() == males.Peek())
                {
                    matchesCount++;
                    females.Dequeue();
                    males.Pop();
                }
                else
                {
                    females.Dequeue();
                    males.Push(males.Pop()-2);
                }
            }

            Console.WriteLine($"Matches: {matchesCount}");
            Console.WriteLine(males.Any() ? $"Males left: {string.Join(", ", males)}" : "Males left: none");
            Console.WriteLine(females.Any() ? $"Females left: {string.Join(", ", females)}" : "Females left: none");
        }
    }
}
