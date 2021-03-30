using System;
using System.Collections.Generic;
using System.Linq;

namespace _09._Simple_Text_Editor
{
    class Program
    {
        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());
            var stack = new Stack<string>();
            var text = String.Empty;

            for (var i = 0; i < n; i++)
            {
                var input = Console.ReadLine().Split();

                var command = int.Parse(input[0]);

                switch (command)
                {
                    case 1:
                        text += input[1];
                        stack.Push(text);
                        break;
                    case 2:
                        text = text.Substring(0, text.Length - int.Parse(input[1]));
                        stack.Push(text);
                        break;
                    case 3:
                        Console.WriteLine(text[int.Parse(input[1]) - 1]);
                        break;
                    case 4:
                        stack.Pop();
                        text = stack.Any() ? stack.Peek() : String.Empty;
                        break;
                }
            }
        }
    }
}
