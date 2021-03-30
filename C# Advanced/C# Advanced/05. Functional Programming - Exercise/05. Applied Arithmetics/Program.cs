using System;
using System.Collections.Generic;
using System.Linq;

namespace _05._Applied_Arithmetics
{
    class Program
    {
        static void Main(string[] args)
        {
            Func<int, int> add = x => x + 1;
            Func<int, int> multiply = x => x * 2;
            Func<int, int> subtract = x => x - 1;
            Action<List<int>> print = x =>
                Console.WriteLine(string.Join(' ', x));

            var numbers = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToList();

            var command = string.Empty;

            while ((command = Console.ReadLine()) != "end")
            {
                switch (command)
                {
                    case "add":
                        numbers = numbers.Select(add).ToList();
                        break;
                    case "multiply":
                        numbers = numbers.Select(multiply).ToList();
                        break;
                    case "subtract":
                        numbers = numbers.Select(subtract).ToList();
                        break;
                    case "print":
                        print(numbers);
                        break;
                }
            }
        }
    }
}
