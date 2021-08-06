using System;

namespace _06.ReversedChars
{
    class StartUp
    {
        static void Main(string[] args)
        {
            char firstchar = char.Parse(Console.ReadLine());
            char secchar = char.Parse(Console.ReadLine());
            char tirdchar = char.Parse(Console.ReadLine());

            Console.WriteLine($"{tirdchar} {secchar} {firstchar}");
        }
    }
}
