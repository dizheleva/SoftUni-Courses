using System;
using System.Linq;

namespace _4._Matrix_Shuffling
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

            var matrix = new string[size[0], size[1]];

            for (var row = 0; row < matrix.GetLength(0); row++)
            {
                var colElements = 
                    Console.ReadLine()
                        .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                        .ToArray();

                for (var col = 0; col < matrix.GetLength(1); col++)

                    matrix[row, col] = colElements[col];

            }

            while (true)
            {
                var command = Console.ReadLine();
                if (command == "END")
                {
                    break;
                }

                var tokens = command
                        .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                        .ToArray();

                if (tokens[0] != "swap" || tokens.Length != 5)
                {
                    Console.WriteLine("Invalid input!");
                }
                else
                {
                    var firstRow = int.Parse(tokens[1]);
                    var firstCol = int.Parse(tokens[2]);
                    var secondRow = int.Parse(tokens[3]);
                    var secondCol = int.Parse(tokens[4]);

                    var outOfRange = firstRow < 0 || firstRow > size[0] - 1 || secondRow < 0 
                                     || secondRow > size[0] - 1 || firstCol < 0 || firstCol > size[1] - 1 
                                     || secondCol < 0 || secondCol > size[1] - 1;
                    if (!outOfRange)
                    {
                        var current = matrix[firstRow, firstCol];
                        matrix[firstRow, firstCol] = matrix[secondRow, secondCol];
                        matrix[secondRow, secondCol] = current;

                        for (var row = 0; row < matrix.GetLength(0); row++)
                        {
                            for (var col = 0; col < matrix.GetLength(1); col++)
                            {
                                Console.Write($"{matrix[row, col]} ");
                            }
                            Console.WriteLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input!");
                    }
                }
            }
        }
    }
}
