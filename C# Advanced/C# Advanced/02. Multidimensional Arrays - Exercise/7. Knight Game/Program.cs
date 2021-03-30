using System;

namespace _7._Knight_Game
{
    class Program
    {
        static void Main(string[] args)
        {
            var size = int.Parse(Console.ReadLine());
            var matrix = new char[size, size];

            Read(matrix);
            var knightsToRemove = 0;
            var rowToRemove = 0;
            var colToRemove = 0;

            while (true)
            {
                var mostAttacked = 0;

                for (var row = 0; row < matrix.GetLength(0); row++)
                {
                    for (var col = 0; col < matrix.GetLength(1); col++)
                    {
                        var attacked = 0;

                        if (matrix[row, col] == 'K')
                        {
                            attacked = AttackCell(matrix, row - 2, col - 1, attacked);
                            attacked = AttackCell(matrix, row - 2, col + 1, attacked);
                            attacked = AttackCell(matrix, row - 1, col - 2, attacked);
                            attacked = AttackCell(matrix, row - 1, col + 2, attacked);
                            attacked = AttackCell(matrix, row + 1, col - 2, attacked);
                            attacked = AttackCell(matrix, row + 1, col + 2, attacked);
                            attacked = AttackCell(matrix, row + 2, col - 1, attacked);
                            attacked = AttackCell(matrix, row + 2, col + 1, attacked);
                        }

                        if (attacked > mostAttacked)
                        {
                            mostAttacked = attacked;
                            rowToRemove = row;
                            colToRemove = col;
                        }
                    }
                }

                if (mostAttacked != 0)
                {
                    matrix[rowToRemove, colToRemove] = 'O';
                    knightsToRemove++;
                }
                else
                {
                    Console.WriteLine(knightsToRemove);
                    break;
                }
            }
        }

        private static void Read(char[,] matrix)
        {
            for (var row = 0; row < matrix.GetLength(0); row++)
            {
                var line = Console.ReadLine();

                for (var col = 0; col < matrix.GetLength(1); col++)
                {
                    matrix[row, col] = line[col];
                }
            }
        }
        private static int AttackCell(char[,] matrix, int row, int col, int attacked)
        {
            if (InRange(matrix, row, col) && matrix[row, col] == 'K')
            {
                attacked++;
            }

            return attacked;
        }
        private static bool InRange(char[,] matrix, int row, int col)
        {
            return row >= 0 
                   && row < matrix.GetLength(0) 
                   && col >= 0 && col < matrix.GetLength(1);
        }
    }
}
