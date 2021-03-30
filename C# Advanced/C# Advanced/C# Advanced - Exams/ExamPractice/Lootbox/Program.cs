using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Lootbox
{
    class Program
    {
        static void Main(string[] args)
        {
            var firstBox = new Queue<int>(Console.ReadLine().Split(' ').Select(int.Parse));
            var secondBox = new Stack<int>(Console.ReadLine().Split(' ').Select(int.Parse));
            var claimedItemsSum = 0;

            while (true)
            {
                var currentSum = firstBox.Peek() + secondBox.Peek();
                if (currentSum%2==0)
                {
                    claimedItemsSum+=currentSum;
                    firstBox.Dequeue();
                    secondBox.Pop();
                }
                else
                {
                    firstBox.Enqueue(secondBox.Pop());
                }

                if (!firstBox.Any() || !secondBox.Any())
                {
                    break;
                }
            }

            Console.WriteLine(firstBox.Any() ? "Second lootbox is empty" : "First lootbox is empty");
            Console.WriteLine(claimedItemsSum>=100 ? $"Your loot was epic! Value: {claimedItemsSum}" : $"Your loot was poor... Value: {claimedItemsSum}");


        }
    }
}
