using System;

namespace Re_Volt
{
    class Matrix
    {
        static char[,] matrix;
        static int playerRow;
        static int playerCol;
        static void MainMethod(string[] args)
        {
            var size = int.Parse(Console.ReadLine());
            var countOfCommands = int.Parse(Console.ReadLine());

            matrix = new char[size, size];
            ReadMatrix();

            for (var index = 0; index < countOfCommands; index++)
            {
                var command = Console.ReadLine();
                switch (command)
                {
                    case "up": Move(-1, 0); break;
                    case "down": Move(1, 0); break;
                    case "left": Move(0, -1); break;
                    case "right": Move(0, 1); break;
                }
            }

            Console.WriteLine("Player lost!");
            PrintMatrix();
        }
        private static void ReadMatrix()
        {
            for (var row = 0; row < matrix.GetLength(0); row++)
            {
                var line = Console.ReadLine();
                for (var col = 0; col < matrix.GetLength(1); col++)
                {
                    matrix[row, col] = line[col];
                    if (line[col] == 'f')
                    {
                        playerRow = row;
                        playerCol = col;
                    }
                }
            }
        }
        private static bool InRange(int dimension)
        {
            return dimension >= 0 && dimension < matrix.GetLength(0);
        }
        private static void Move(int row, int col)
        {
            var currentRow = InRange(playerRow + row) ? playerRow + row : playerRow + row + matrix.GetLength(0);
            var currentCol = InRange(playerCol + col) ? playerCol + col : playerCol + col + matrix.GetLength(1);
            matrix[playerRow, playerCol] = '-';
            
            if (matrix[currentRow, currentCol] == 'B')
            {
                Move(row, col);
            }
            else if (matrix[currentRow, currentCol] == 'T')
            {
                var oppositeRow = row == 0 ? 0 : row > 0 ? -1 : 1;
                var oppositeCol = col == 0 ? 0 : col > 0 ? -1 : 1;
                Move(oppositeRow, oppositeCol);
            }
            else if (matrix[currentRow, currentCol] == 'F')
            {
                Console.WriteLine("Player won!");
                PrintMatrix();
                Environment.Exit(0);
            }
        }
        private static void PrintMatrix()
        {
            for (var row = 0; row < matrix.GetLength(0); row++)
            {
                for (var col = 0; col < matrix.GetLength(1); col++)
                {
                    Console.Write(matrix[row, col]);
                }
                Console.WriteLine();
            }
        }
    }
}
