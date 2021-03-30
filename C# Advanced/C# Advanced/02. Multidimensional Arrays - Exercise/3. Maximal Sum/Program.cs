using System;
using System.Linq;

namespace _3._Maximal_Sum
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

            var sum = 0;
            var maxSum = 0;
            var matrix = new int[size[0], size[1]];
            var miniMatrix = new int[3, 3];

            for (var row = 0; row < matrix.GetLength(0); row++)
            {
                var colElements = 
                    Console.ReadLine()
                        .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                        .Select(int.Parse)
                        .ToArray();

                for (var col = 0; col < matrix.GetLength(1); col++)

                    matrix[row, col] = colElements[col];

            }

            if (size[0] >= 3 && size[1] >= 3)
            {
                for (var row = 0; row < matrix.GetLength(0) - 2; row++)
                {
                    for (var col = 0; col < matrix.GetLength(1) - 2; col++)
                    {
                        var currentMiniMatrix = new int[3, 3];

                        for (var i = 0; i < 3; i++)
                        {
                            for (var j = 0; j < 3; j++)
                            {
                                sum += matrix[row + i, col + j];
                                currentMiniMatrix[i, j] = matrix[row + i, col + j];
                            }
                        }

                        if (sum > maxSum)
                        {
                            maxSum = sum;
                            miniMatrix = currentMiniMatrix;
                        }

                        sum = 0;
                    }

                }
            }

            Console.WriteLine($"Sum = {maxSum}");
            for (var row = 0; row < 3; row++)
            {
                for (var col = 0; col < 3; col++)
                {
                    Console.Write($"{miniMatrix[row, col]} ");
                }
                Console.WriteLine();
            }
        }
    }
}
