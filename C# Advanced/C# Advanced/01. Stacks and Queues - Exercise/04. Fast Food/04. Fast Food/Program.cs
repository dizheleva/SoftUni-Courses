using System;
using System.Collections.Generic;
using System.Linq;

namespace _04._Fast_Food
{
    class Program
    {
        static void Main(string[] args)
        {
            var quantity = int.Parse(Console.ReadLine());
            var orders = new Queue<int>(Console.ReadLine()
                .Split()
                .Select(int.Parse));

            Console.WriteLine(orders.Max());

            while (orders.Any())
            {
                if (quantity >= orders.Peek())
                {
                    quantity -= orders.Dequeue();
                }
                else
                {
                    Console.WriteLine($"Orders left: {String.Join(" ", orders)}");
                    break;
                }
            }

            if (!orders.Any())
            {
                Console.WriteLine("Orders complete");
            }
        }
    }
}
