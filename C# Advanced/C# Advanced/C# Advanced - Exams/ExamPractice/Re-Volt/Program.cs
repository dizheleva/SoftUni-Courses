using System;

namespace Re_Volt
{
    class Program
    {
        static char[,] matrix;
        static int playerRow;
        static int playerCol;
        static void Main(string[] args)
        {
            var size = int.Parse(Console.ReadLine());
            var countOfCommands = int.Parse(Console.ReadLine());
            matrix = new char[size, size];
            ReadMatrix();

            for (var i = 0; i < countOfCommands; i++)
            {
                var command = Console.ReadLine();
                Move(command);
            }

            Console.WriteLine("Player lost!");
            matrix[playerRow, playerCol] = 'f';
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
                        matrix[row, col] = '-';
                    }
                }
            }
        }
        private static bool InRange(int dimension)
        {
            return dimension >= 0 && dimension < matrix.GetLength(0);
        }
        private static void Move(string direction)
        {
            var oppositeDirection = direction switch
            {
                "up" => "down",
                "down" => "up",
                "left" => "right",
                "right" => "left"
            };
            switch (direction)
            {
                case "up":
                    playerRow = InRange(playerRow - 1) ? playerRow - 1 : matrix.GetLength(0) - 1;
                    break;
                case "down":
                    playerRow = InRange(playerRow + 1) ? playerRow + 1 : 0;
                    break;
                case "left":
                    playerCol = InRange(playerCol - 1) ? playerCol - 1 : matrix.GetLength(1) - 1;
                    break;
                case "right":
                    playerCol = InRange(playerCol + 1) ? playerCol + 1 : 0;
                    break;
            }
            switch (matrix[playerRow, playerCol])
            {
                case 'B':
                    Move(direction);
                    break;
                case 'T':
                    Move(oppositeDirection);
                    break;
                case 'F':
                    Console.WriteLine("Player won!");
                    matrix[playerRow, playerCol] = 'f';
                    PrintMatrix();
                    Environment.Exit(0);
                    break;
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