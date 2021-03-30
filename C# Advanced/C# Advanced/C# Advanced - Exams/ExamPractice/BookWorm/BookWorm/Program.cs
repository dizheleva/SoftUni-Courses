using System;
using System.Linq;

namespace BookWorm
{
    class Program
    {
        static char[,] matrix;
        static string worm;
        static int playerRow;
        static int playerCol;
        static void Main(string[] args)
        {
            worm = Console.ReadLine();
            var size = int.Parse(Console.ReadLine());

            matrix = new char[size, size];
            ReadMatrix();

            var command = string.Empty;
            while ((command = Console.ReadLine()) != "end")
            {
                matrix[playerRow, playerCol] = '-';
                Move(command);
            }

            matrix[playerRow, playerCol] = 'P';
            PrintResult();
        }
        private static void ReadMatrix()
        {
            for (var row = 0; row < matrix.GetLength(0); row++)
            {
                var line = Console.ReadLine(); //.Where(c => c != ' ').ToArray();
                for (var col = 0; col < matrix.GetLength(1); col++)
                {
                    matrix[row, col] = line[col];
                    if (line[col] == 'P')
                    {
                        playerRow = row;
                        playerCol = col;
                    }
                }
            }
        }
        private static void Move(string direction)
        {
            switch (direction)
            {
                case "up":
                    CheckForLetter(playerRow-1, playerCol);
                    break;
                case "down":
                    CheckForLetter(playerRow+1, playerCol);
                    break;
                case "left":
                    CheckForLetter(playerRow, playerCol-1);
                    break;
                case "right":
                    CheckForLetter(playerRow, playerCol+1);
                    break;
            }
        }
        private static bool InRange(int x, int y)
        {
            return (x >= 0 && x < matrix.GetLength(0))
                && (y>= 0 && y < matrix.GetLength(1));
        }
        
        private static void CheckForLetter(int x, int y)
        {
            if (InRange(x, y))
            {
                playerRow = x;
                playerCol = y;
                if (matrix[playerRow, playerCol] != '-')
                    worm += matrix[playerRow, playerCol];
            }
            else
            {
                worm = worm.Remove(worm.Length - 1, 1);
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
            Console.WriteLine(worm);
            PrintMatrix();
        }
    }
}
