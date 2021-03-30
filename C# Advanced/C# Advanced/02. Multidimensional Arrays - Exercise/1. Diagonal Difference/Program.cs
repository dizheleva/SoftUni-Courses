using System;
using System.Linq;

namespace _1._Diagonal_Difference
{
    class Program
    {
        static void Main(string[] args)
        {
            var size = int.Parse(Console.ReadLine());
            var cube = new int[size, size];

            for (var row = 0; row < cube.GetLength(0); row++)
            {

                var colElements = Console.ReadLine().Split()

                    .Select(int.Parse).ToArray();

                for (var col = 0; col < cube.GetLength(1); col++)

                    cube[row, col] = colElements[col];

            }

            var primarySum = 0;
            var secondarySum = 0;

            for (var i = 0; i < size; i++)
            {
                primarySum += cube[i, i];
            }

            for (var i = 0; i < size; i++)
            {
                secondarySum += cube[i, size - i - 1];
            }

            Console.WriteLine(Math.Abs(primarySum - secondarySum));
        }
    }
}
