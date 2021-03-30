using System;
using System.Linq;

namespace _9._Miner
{
    class Program
    {
        static void Main(string[] args)
        {
            var size = int.Parse(Console.ReadLine());
            var field = new char[size, size];
            var commands = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            Read(field);

            var totalCoal = 0;
            var collectedCoal = 0;
            var startRow = 0;
            var startCol = 0;

            for (var row = 0; row < field.GetLength(0); row++)
            {
                for (var col = 0; col < field.GetLength(1); col++)
                {
                    if (field[row, col] == 's')
                    {
                        startRow = row;
                        startCol = col;
                    }
                    if (field[row, col] == 'c')
                    {
                        totalCoal++;
                    }
                }
            }

            for (var i = 0; i < commands.Length; i++)
            {
                var command = commands[i];
                var currentRow = startRow;
                var currentCol = startCol;
                var currentCell = '*';
                switch (command)
                {
                    case "up":
                        currentCell = CheckCell(field, currentRow - 1, currentCol, currentCell);
                        if (InRange(field, currentRow - 1, currentCol))
                        {
                            currentRow--;
                        }
                        break;
                    case "down":
                        currentCell = CheckCell(field, currentRow + 1, currentCol, currentCell);
                        if (InRange(field, currentRow + 1, currentCol))
                        {
                            currentRow++;
                        }
                        break;
                    case "left":
                        currentCell = CheckCell(field, currentRow, currentCol - 1, currentCell);
                        if (InRange(field, currentRow, currentCol - 1))
                        {
                            currentCol--;
                        }
                        break;
                    case "right":
                        currentCell = CheckCell(field, currentRow, currentCol + 1, currentCell);
                        if (InRange(field, currentRow, currentCol + 1))
                        {
                            currentCol++;
                        }
                        break;
                }

                switch (currentCell)
                {
                    case 'c':
                        collectedCoal++;
                        field[currentRow, currentCol] = '*';
                        if (collectedCoal == totalCoal)
                        {
                            Console.WriteLine($"You collected all coals! ({currentRow}, {currentCol})");
                            return;
                        }

                        break;
                    case 'e':
                        {
                            Console.WriteLine($"Game over! ({currentRow}, {currentCol})");
                        }
                        return;
                }

                if (i == commands.Length - 1)
                {
                    Console.WriteLine($"{totalCoal - collectedCoal} coals left. ({currentRow}, {currentCol})");
                }

                startRow = currentRow;
                startCol = currentCol;
            }


        }
        private static void Read(char[,] matrix)
        {
            for (var row = 0; row < matrix.GetLength(0); row++)
            {
                var line = Console.ReadLine().Split().Select(char.Parse).ToArray();

                for (var col = 0; col < matrix.GetLength(1); col++)
                {
                    matrix[row, col] = line[col];
                }
            }
        }
        private static char CheckCell(char[,] matrix, int row, int col, char currentCell)
        {
            if (InRange(matrix, row, col))
            {
                currentCell = matrix[row, col];
            }
            return currentCell;
        }
        private static bool InRange(char[,] matrix, int row, int col)
        {
            return row >= 0 && row < matrix.GetLength(0) && col >= 0 && col < matrix.GetLength(1);
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
