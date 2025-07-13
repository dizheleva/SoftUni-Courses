namespace _01.EvenNumbersThread
{
    using System;

    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter start number:");
            var start = int.Parse(Console.ReadLine() ?? "0");

            Console.WriteLine("Enter end number:");
            var end = int.Parse(Console.ReadLine() ?? "0");

            Console.WriteLine();

            Thread evens = new Thread(() => PrintEvenNumbers(start, end));

            evens.Start();
            evens.Join(); // Wait for the thread to finish
            Console.WriteLine("Thread finished work");
        }

        private static void PrintEvenNumbers(int start, int end)
        {
            for (int i = start; i <= end; i++)
            {
                if (i % 2 == 0)
                {
                    Console.WriteLine(i);
                }
            }
        }
    }
}
