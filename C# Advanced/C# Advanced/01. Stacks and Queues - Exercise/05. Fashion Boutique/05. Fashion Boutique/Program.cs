using System;
using System.Collections.Generic;
using System.Linq;

namespace _05._Fashion_Boutique
{
    class Program
    {
        static void Main(string[] args)
        {
            var clothes = new Stack<int>(Console.ReadLine()
                .Split()
                .Select(int.Parse));

            var rackCap = int.Parse(Console.ReadLine());

            var racks = 1;
            var currentRack = rackCap;

            while (clothes.Any())
            {

                if (currentRack >= clothes.Peek())
                {
                    currentRack -= clothes.Pop();
                }
                else
                {
                    racks++;
                    currentRack = rackCap;
                }
            }

            Console.WriteLine(racks);
        }
    }
}
