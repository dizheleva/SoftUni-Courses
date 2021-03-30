using System;
using System.Linq;

namespace _8._Bombs
{
    class Program
    {
        static void Main(string[] args)
        {
            var size = int.Parse(Console.ReadLine());
            var matrix = new int[size, size];

            Read(matrix);
            var bombs = Console.ReadLine().Split();

            foreach (var bomb in bombs)
            {
                var bombCoordinates = bomb.Split(',').Select(int.Parse).ToArray();
                var bombRow = bombCoordinates[0];
                var bombCol = bombCoordinates[1];
                var damage = matrix[bombRow, bombCol];

                if (damage <= 0) continue;
                ExplodeCell(matrix, bombRow - 1, bombCol - 1, damage);
                ExplodeCell(matrix, bombRow - 1, bombCol, damage);
                ExplodeCell(matrix, bombRow - 1, bombCol + 1, damage);
                ExplodeCell(matrix, bombRow, bombCol - 1, damage);
                ExplodeCell(matrix, bombRow, bombCol + 1, damage);
                ExplodeCell(matrix, bombRow + 1, bombCol - 1, damage);
                ExplodeCell(matrix, bombRow + 1, bombCol, damage);
                ExplodeCell(matrix, bombRow + 1, bombCol + 1, damage);
                matrix[bombRow, bombCol] = 0;
            }

            var aliveCells = 0;
            var sumOfCells = 0;

            foreach (var cell in matrix)
            {
                if (cell <= 0) continue;
                aliveCells++;
                sumOfCells += cell;
            }

            Console.WriteLine($"Alive cells: {aliveCells}");
            Console.WriteLine($"Sum: {sumOfCells}");
            Print(matrix);
        }

        private static void Read(int[,] matrix)
        {
            for (var row = 0; row < matrix.GetLength(0); row++)
            {
                var line = Console.ReadLine().Split().Select(int.Parse).ToArray();

                for (var col = 0; col < matrix.GetLength(1); col++)
                {
                    matrix[row, col] = line[col];
                }
            }
        }
        private static void ExplodeCell(int[,] matrix, int row, int col, int damage)
        {
            if (InRange(matrix, row, col) && matrix[row, col] > 0)
            {
                matrix[row, col] -= damage;
            }
        }
        private static bool InRange(int[,] matrix, int row, int col)
        {
            return row >= 0 
                   && row < matrix.GetLength(0) 
                   && col >= 0 && col < matrix.GetLength(1);
        }
        private static void Print(int[,] matrix)
        {
            for (var row = 0; row < matrix.GetLength(0); row++)
            {
                for (var col = 0; col < matrix.GetLength(1); col++)
                {
                    Console.Write(matrix[row, col] + " ");
                }

                Console.WriteLine();
            }
        }
    }
}
