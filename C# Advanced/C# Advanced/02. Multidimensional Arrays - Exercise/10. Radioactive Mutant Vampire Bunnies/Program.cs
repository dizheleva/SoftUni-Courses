using System;
using System.Collections.Generic;
using System.Linq;

namespace _10._Radioactive_Mutant_Vampire_Bunnies
{
    class Program
    {
        static void Main(string[] args)
        {
            var dimentions = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

            var rows = dimentions[0];
            var cols = dimentions[1];

            var matrix = new char[rows, cols];
            var playerRow = 0;
            var playerCol = 0;

            for (var row = 0; row < rows; row++)
            {
                var rowValues = Console.ReadLine().ToCharArray();

                for (var col = 0; col < cols; col++)
                {
                    matrix[row, col] = rowValues[col];

                    if (matrix[row, col] == 'P')
                    {
                        playerRow = row;
                        playerCol = col;
                    }
                }
            }

            var directions = Console.ReadLine().ToCharArray();
            var isWon = false;
            var isDead = false;

            foreach (var direction in directions)
            {
                var playerNewRow = playerRow;
                var playerNewCol = playerCol;

                switch (direction)
                {
                    case 'U': playerNewRow--; break;
                    case 'D': playerNewRow++; break;
                    case 'L': playerNewCol--; break;
                    case 'R': playerNewCol++; break;
                }

                isWon = IsWon(matrix, playerNewRow, playerNewCol);

                if (!isWon)
                {
                    isDead = IsSymbol(matrix, 'B', playerNewRow, playerNewCol);

                    if (!isDead)
                    {
                        matrix[playerNewRow, playerNewCol] = 'P';
                    }

                    matrix[playerRow, playerCol] = '.';
                    playerRow = playerNewRow;
                    playerCol = playerNewCol;
                }
                else
                {
                    matrix[playerRow, playerCol] = '.';
                }

                var bunniesCoordinates = new List<int>();

                for (var row = 0; row < rows; row++)
                {
                    for (var col = 0; col < cols; col++)
                    {
                        if (matrix[row, col] == 'B')
                        {
                            bunniesCoordinates.Add(row);
                            bunniesCoordinates.Add(col);
                        }
                    }
                }

                for (var i = 0; i < bunniesCoordinates.Count; i += 2)
                {
                    var bunnyRow = bunniesCoordinates[i];
                    var bunnyCol = bunniesCoordinates[i + 1];

                    SpreadBunny(matrix, bunnyRow, bunnyCol);
                }

                isDead = IsSymbol(matrix, 'B', playerRow, playerCol);

                if (isWon || isDead)
                {
                    break;
                }
            }

            PrintMatrix(matrix);
            Console.WriteLine(isWon ? $"won: {playerRow} {playerCol}" : $"dead: {playerRow} {playerCol}");
        }

        static void SpreadBunny(char[,] matrix, int row, int col)
        {
            if (row - 1 >= 0)
            {
                matrix[row - 1, col] = 'B';
            }
            if (row + 1 < matrix.GetLength(0))
            {
                matrix[row + 1, col] = 'B';
            }
            if (col - 1 >= 0)
            {
                matrix[row, col - 1] = 'B';
            }
            if (col + 1 < matrix.GetLength(1))
            {
                matrix[row, col + 1] = 'B';
            }
        }

        static bool IsSymbol(char[,] matrix, char symbol, int row, int col)
        {
            bool isSymbol = matrix[row, col] == symbol;

            return isSymbol;
        }

        static bool IsWon(char[,] matrix, int row, int col)
        {
            bool isWon = !(row >= 0 
                           && row < matrix.GetLength(0)
                           && col >= 0 
                           && col < matrix.GetLength(1));

            return isWon;

        }

        static void PrintMatrix(char[,] matrix)
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
