using System;
using System.Collections.Generic;
using System.Linq;

namespace _5._Snake_Moves
{
    class Program
    {
        static void Main(string[] args)
        {
            var size = 
                Console.ReadLine()
                    .Split()
                    .Select(int.Parse)
                    .ToArray();

            var matrix = new char[size[0]][];
            var snake = Console.ReadLine();
            var queue = new Queue<char>(snake);

            for (var row = 0; row < matrix.GetLength(0); row++)
            {
                var currentRow = new char[size[1]];

                for (var i = 0; i < currentRow.Length; i++)
                {
                    currentRow[i] = queue.Peek();
                    queue.Enqueue(queue.Dequeue());
                }

                if (row % 2 == 0)
                {
                    matrix[row] = currentRow;
                }
                else
                {
                    matrix[row] = currentRow.Reverse().ToArray();
                }
            }


            for (var row = 0; row < matrix.GetLength(0); row++)
            {
                foreach (var ch in matrix[row])
                {
                    Console.Write($"{ch}");
                }

                Console.WriteLine();
            }
        }
    }
}
