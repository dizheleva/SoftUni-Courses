using System;

namespace TronRacers
{
    class Program
    {
        static char[,] matrix;
        static int firstPlayerRow;
        static int firstPlayerCol;
        static int secondPlayerRow;
        static int secondPlayerCol;
        static void Main(string[] args)
        {
            var size = int.Parse(Console.ReadLine());
            matrix = new char[size, size];
            ReadMatrix();

            while(true)
            {
                var commands = Console.ReadLine().Split();
                var firstPlayerCommand = commands[0];
                var secondPlayerCommand = commands[1];
                Move(1, firstPlayerCommand);
                Move(2, secondPlayerCommand);
            }
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
                        firstPlayerRow = row;
                        firstPlayerCol = col;
                    }
                    if (line[col] == 's')
                    {
                        secondPlayerRow = row;
                        secondPlayerCol = col;
                    }
                }
            }
        }
        private static bool InRange(int dimension)
        {
            return dimension >= 0 && dimension < matrix.GetLength(0);
        }
        private static void Move(int player, string direction)
        {
            switch (direction)
            {
                case "up":
                    switch (player)
                    {
                        case 1:
                            firstPlayerRow = InRange(firstPlayerRow - 1) ? firstPlayerRow - 1 : matrix.GetLength(0) - 1;
                            break;
                        case 2:
                            secondPlayerRow = InRange(secondPlayerRow - 1) ? secondPlayerRow - 1 : matrix.GetLength(0) - 1;
                            break;
                    }
                    break;
                case "down":
                    switch (player)
                    {
                        case 1:
                            firstPlayerRow = InRange(firstPlayerRow + 1) ? firstPlayerRow + 1 : 0;
                            break;
                        case 2:
                            secondPlayerRow = InRange(secondPlayerRow + 1) ? secondPlayerRow + 1 : 0;
                            break;
                    }
                    break;
                case "left":
                    switch (player)
                    {
                        case 1:
                            firstPlayerCol = InRange(firstPlayerCol - 1) ? firstPlayerCol - 1 : matrix.GetLength(1) - 1;
                            break;
                        case 2:
                            secondPlayerCol = InRange(secondPlayerCol - 1) ? secondPlayerCol - 1 : matrix.GetLength(1) - 1;
                            break;
                    }
                    break;
                case "right":
                    switch (player)
                    {
                        case 1:
                            firstPlayerCol = InRange(firstPlayerCol + 1) ? firstPlayerCol + 1 : 0; ;
                            break;
                        case 2:
                            secondPlayerCol = InRange(secondPlayerCol + 1) ? secondPlayerCol + 1 : 0;
                            break;
                    }
                    break;
            }
            if (player==1)
            {
                if (matrix[firstPlayerRow, firstPlayerCol] == 's')
                {
                    matrix[firstPlayerRow, firstPlayerCol] = 'x';
                    PrintMatrix();
                    Environment.Exit(0);
                }
                else
                {
                    matrix[firstPlayerRow, firstPlayerCol] = 'f';
                }
            }
            else
            {
                if (matrix[secondPlayerRow, secondPlayerCol] == 'f')
                {
                    matrix[secondPlayerRow, secondPlayerCol] = 'x';
                    PrintMatrix();
                    Environment.Exit(0);
                }
                else
                {
                    matrix[secondPlayerRow, secondPlayerCol] = 's';
                }
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