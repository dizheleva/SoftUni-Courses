using System;
using System.Collections.Generic;
using System.Linq;

namespace _01._Basic_Stack_Operations
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputCommands = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();
            
            var numbers = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();

            var stack = new Stack<int>();

            for (var i = 0; i < inputCommands[0]; i++)
            {
                stack.Push(numbers[i]);
            }

            for (var i = 0; i < inputCommands[1]; i++)
            {
                stack.Pop();
            }

            if (stack.Contains(inputCommands[2]))
            {
                Console.WriteLine("true");
            }
            else if (stack.Count == 0)
            {
                Console.WriteLine(0);
            }
            else
            {
                Console.WriteLine(stack.Min());
            }
        }
    }
}
