using System;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace PresentDelivery
{
    class Program
    {
        static char[,] matrix;
        static int countOfPresents;
        static int niceKids;
        static int niceKidsLeft;
        static int santaRow;
        static int santaCol;
        static void Main(string[] args)
        {
            countOfPresents = int.Parse(Console.ReadLine());
            var size = int.Parse(Console.ReadLine());

            matrix = new char[size, size];
            ReadMatrix();
            niceKids = matrix.Cast<char>().Count(cell => cell == 'V');

            var command = string.Empty;
            while ((command = Console.ReadLine()) != "Christmas morning" && countOfPresents != 0)
            {
                matrix[santaRow, santaCol] = '-';
                Move(command);
                GivePresent();
                
            }
            matrix[santaRow, santaCol] = 'S';
            niceKidsLeft = matrix.Cast<char>().Count(cell => cell == 'V');
            PrintResult();
        }
        private static void ReadMatrix()
        {
            for (var row = 0; row < matrix.GetLength(0); row++)
            {
                var line = Console.ReadLine().Where(c => c != ' ').ToArray();
                for (var col = 0; col < matrix.GetLength(1); col++)
                {
                    matrix[row, col] = line[col];
                    if (line[col] == 'S')
                    {
                        santaRow = row;
                        santaCol = col;
                    }
                }
            }
        }
        private static void Move(string direction)
        {
            switch (direction)
            {
                case "up":
                    santaRow--;
                    break;
                case "down":
                    santaRow++;
                    break;
                case "left":
                    santaCol--;
                    break;
                case "right":
                    santaCol++;
                    break;
            }
        }
        private static void GivePresent()
        {
            switch (matrix[santaRow, santaCol])
            {
                case 'V':
                    countOfPresents--;
                    break;
                case 'C':
                    CheckForKid(santaRow, santaCol + 1); // right
                    CheckForKid(santaRow, santaCol - 1); // left
                    CheckForKid(santaRow - 1, santaCol); //up;
                    CheckForKid(santaRow + 1, santaCol); //down);
                    break;
            }
        }
        private static void CheckForKid(int row, int col)
        {
            if (matrix[row, col] != '-' ) 
                countOfPresents--;
        }
        private static void PrintMatrix()
        {
            for (var row = 0; row < matrix.GetLength(0); row++)
            {
                for (var col = 0; col < matrix.GetLength(1); col++)
                {
                    Console.Write($"{matrix[row, col]} ");
                }
                Console.WriteLine();
            }
        }
        private static void PrintResult()
        {
            if (countOfPresents <= 0)
            {
                Console.WriteLine("Santa ran out of presents!");
            }
            PrintMatrix();
            Console.WriteLine(niceKidsLeft == 0
                ? $"Good job, Santa! {niceKids} happy nice kid/s."
                : $"No presents for {niceKidsLeft} nice kid/s.");
        }
    }
}