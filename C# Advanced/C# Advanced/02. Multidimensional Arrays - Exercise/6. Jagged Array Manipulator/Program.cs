using System;
using System.Linq;

namespace _6._Jagged_Array_Manipulator
{
    class Program
    {
        static void Main(string[] args)
        {
            var rows = int.Parse(Console.ReadLine());
            var matrix = new double[rows][];

            for (var row = 0; row < matrix.GetLength(0); row++)
            {
                matrix[row] = Console.ReadLine().Split().Select(double.Parse).ToArray();
            }

            for (var i = 0; i < rows - 1; i++)
            {
                if (matrix[i].Length == matrix[i + 1].Length)
                {
                    for (var col = 0; col < matrix[i].Length; col++)
                    {
                        matrix[i][col] *= 2;
                        matrix[i + 1][col] *= 2;
                    }
                }
                else
                {
                    for (var col = 0; col < matrix[i].Length; col++)
                    {
                        matrix[i][col] /= 2;
                    }
                    for (var col = 0; col < matrix[i + 1].Length; col++)
                    {
                        matrix[i + 1][col] /= 2;
                    }
                }
            }

            while (true)
            {
                var input = Console.ReadLine();
                if (input == "End")
                {
                    break;
                }

                var tokens = input.Split().ToArray();
                var command = tokens[0];
                var row = int.Parse(tokens[1]);
                var column = int.Parse(tokens[2]);
                var value = long.Parse(tokens[3]);

                if (row < rows && row >= 0 && column < matrix[row].Length && column >= 0)
                {
                    switch (command)
                    {
                        case "Add":
                            matrix[row][column] += value;
                            break;
                        case "Subtract":
                            matrix[row][column] -= value;
                            break;
                    }
                }
            }

            for (var row = 0; row < matrix.GetLength(0); row++)
            {
                foreach (var ch in matrix[row])
                {
                    Console.Write($"{ch} ");
                }
                Console.WriteLine();
            }
        }
    }
}
