using System;
using System.Linq;

namespace _2._Squares_in_Matrix
{
    class Program
    {
        static void Main(string[] args)
        {
            var size = Console.ReadLine().Split().Select(int.Parse).ToArray();
            if (size.Length < 2)
            {
                Console.WriteLine(0);
            }
            else
            {
                var matrix = new int[size[0], size[1]];

                for (var row = 0; row < matrix.GetLength(0); row++)
                {

                    var colElements = 
                        Console.ReadLine()
                        .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                        .Select(char.Parse)
                        .ToArray();

                    for (var col = 0; col < matrix.GetLength(1); col++)

                        matrix[row, col] = colElements[col];

                }

                var counter = 0;

                if (size[0] >= 2 || size[1] >= 2)
                {
                    for (var row = 0; row < matrix.GetLength(0) - 1; row++)
                    {
                        for (var col = 0; col < matrix.GetLength(1) - 1; col++)
                        {

                            if (matrix[row, col] == matrix[row + 1, col] 
                                && matrix[row, col] == matrix[row, col + 1] 
                                && matrix[row, col] == matrix[row + 1, col + 1])
                            {
                                counter++;
                            }
                        }

                    }
                }

                Console.WriteLine((size[0] < 2 || size[1] < 2) ? 0 : counter);
            }
        }
    }
}
