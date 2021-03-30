using System;
using System.Collections.Generic;
using System.Linq;

namespace ClubParty
{
    class Program
    {
        static void Main(string[] args)
        {
            var capacity = int.Parse(Console.ReadLine());
            var inputStack = new Stack<string>(Console.ReadLine().Split(' '));
            var hallsQueue = new Queue<char>();
            var halls = new Dictionary<char, List<int>>();

            while (inputStack.Any())
            {
                var currentElement = inputStack.Peek();

                if (char.IsLetter(currentElement[0]))
                {
                    halls[char.Parse(currentElement)] = new List<int>();
                    hallsQueue.Enqueue(char.Parse(inputStack.Pop()));
                    continue;
                }

                if (halls.Count == 0)
                {
                    inputStack.Pop();
                    continue;
                }

                foreach (var hall in halls)
                {

                    if (hall.Value.Sum() + int.Parse(currentElement) <= capacity)
                    {
                        halls[hall.Key].Add(int.Parse(inputStack.Pop()));
                        break;
                    }

                    if (hall.Value.Sum() + int.Parse(currentElement) > capacity && halls.Any())
                    {
                        Console.WriteLine($"{hallsQueue.Dequeue()} -> {string.Join(", ", hall.Value)}");
                        halls.Remove(hall.Key);
                    }

                    break;
                }
            }
        }
    }
}
