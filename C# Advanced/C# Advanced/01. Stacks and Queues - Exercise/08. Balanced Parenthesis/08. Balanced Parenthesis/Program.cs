using System;
using System.Collections.Generic;
using System.Linq;

namespace _08._Balanced_Parenthesis
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            var stack = new Stack<char>();
            var pairs = new Dictionary<char, char>()
            {
                { ')', '(' }, 
                { ']', '[' }, 
                { '}', '{' }
            };
            var balanced = true;

            foreach (var sign in input)
            {
                if (pairs.ContainsValue(sign))
                {
                    stack.Push(sign);
                }
                else if (stack.Any() && pairs[sign] == stack.Peek())
                {
                    stack.Pop();
                }
                else
                {
                    balanced = false;
                }
            }

            Console.WriteLine(balanced ? "YES" : "NO");
        }
    }
}
