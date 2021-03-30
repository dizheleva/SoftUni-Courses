using System;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace PresentDelivery
{
    class Program
    {
        static char[,] matrix;
        static int foodNeeded;
        static int snakeRow;
        static int snakeCol;
        private static bool gameIsOver = false;
        static void Main(string[] args)
        {
            foodNeeded = 10;
            var size = int.Parse(Console.ReadLine());

            matrix = new char[size, size];
            ReadMatrix();

            while (foodNeeded > 0)
            {
                matrix[snakeRow, snakeCol] = '.';
                var command = Console.ReadLine();
                Move(command);
                if (gameIsOver)
                {
                    matrix[snakeRow, snakeCol] = '.';
                    Console.WriteLine("Game over!");
                    PrintResult();
                    Environment.Exit(0);
                }
                CheckCell();
            }
            matrix[snakeRow, snakeCol] = 'S';
            Console.WriteLine("You won! You fed the snake.");
            PrintResult();
        }
        private static void ReadMatrix()
        {
            for (var row = 0; row < matrix.GetLength(0); row++)
            {
                var line = Console.ReadLine();
                for (var col = 0; col < matrix.GetLength(1); col++)
                {
                    matrix[row, col] = line[col];
                    if (line[col] == 'S')
                    {
                        snakeRow = row;
                        snakeCol = col;
                    }
                }
            }
        }
        private static void Move(string direction)
        {
            switch (direction)
            {
                case "up":
                    if (InRange(snakeRow - 1, snakeCol))
                    {
                        snakeRow--;
                    }
                    else
                    {
                        gameIsOver = true;
                    }
                    break;
                case "down":
                    if (InRange(snakeRow + 1, snakeCol))
                    {
                        snakeRow++;
                    }
                    else
                    {
                        gameIsOver = true;
                    }
                    break;
                case "left":
                    if (InRange(snakeRow, snakeCol - 1))
                    {
                        snakeCol--;
                    }
                    else
                    {
                        gameIsOver = true;
                    }
                    break;
                case "right":
                    if (InRange(snakeRow, snakeCol + 1))
                    {
                        snakeCol++;
                    }
                    else
                    {
                        gameIsOver = true;
                    }
                    break;
            }
        }
        private static bool InRange(int x, int y)
        {
            return (x >= 0 && x < matrix.GetLength(0))
                   && (y >= 0 && y < matrix.GetLength(1));
        }
        private static void CheckCell()
        {
            switch (matrix[snakeRow, snakeCol])
            {
                case '*':
                    foodNeeded--;
                    break;
                case 'B':
                    GoTroughLair();
                    break;
            }
        }
        private static void GoTroughLair()
        {
            matrix[snakeRow, snakeCol] = '.';
            for (var row = 0; row < matrix.GetLength(0); row++)
            {
                for (var col = 0; col < matrix.GetLength(1); col++)
                {
                    if (matrix[row, col] == 'B')
                    {
                        snakeRow = row;
                        snakeCol = col;
                    }
                }
            }
        }
        private static void PrintMatrix()
        {
            
            for (var row = 0; row < matrix.GetLength(0); row++)
            {
                for (var col = 0; col < matrix.GetLength(1); col++)
                {
                    Console.Write($"{matrix[row, col]}");
                }
                Console.WriteLine();
            }
        }
        private static void PrintResult()
        {
            Console.WriteLine($"Food eaten: {10 - foodNeeded}");
            PrintMatrix();
        }
    }
}