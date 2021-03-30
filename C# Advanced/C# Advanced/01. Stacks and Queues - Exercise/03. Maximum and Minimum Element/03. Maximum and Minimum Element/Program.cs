using System;
using System.Collections.Generic;
using System.Linq;

namespace _03._Maximum_and_Minimum_Element
{
    class Program
    {
        static void Main(string[] args)
        {
            var numCommands = int.Parse(Console.ReadLine());
            var stack = new Stack<int>();

            for (int i = 0; i < numCommands; i++)
            {
                var input = Console.ReadLine().Split().Select(int.Parse).ToArray();

                switch (input[0])
                {
                    case 1: 
                        stack.Push(input[1]); 
                        break;
                    case 2:
                        if (stack.Any())
                        {
                            stack.Pop();
                        }
                        break;
                    case 3:
                        if (stack.Any())
                        {
                            Console.WriteLine(stack.Max());
                        }
                        break;
                    case 4:
                        if (stack.Any())
                        {
                            Console.WriteLine(stack.Min());
                        }
                        break;
                }
            }

            Console.WriteLine(String.Join(", ", stack));
        }
    }
}
