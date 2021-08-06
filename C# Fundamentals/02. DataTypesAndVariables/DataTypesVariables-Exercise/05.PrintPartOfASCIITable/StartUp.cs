using System;

namespace _05.PrintPartOfASCIITable
{
    class StartUp
    {
        static void Main(string[] args)
        {
            int start = int.Parse(Console.ReadLine());
            int end = int.Parse(Console.ReadLine());

            for (int i = start; i <= end; i++)
            {
                char output = (char)i;
                Console.Write(output + " ");
            }
        }
    }
}
