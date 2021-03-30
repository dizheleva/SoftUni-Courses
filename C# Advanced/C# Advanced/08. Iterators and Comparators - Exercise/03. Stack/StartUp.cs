using System;
using System.Linq;

namespace Stack
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var customStack = new Stack<string>();

            while (true)
            {
                var input = Console.ReadLine().Split(new string[]{" ", ", "}, StringSplitOptions.RemoveEmptyEntries);
                var data = input.Skip(1).ToList();

                switch (input[0])
                {
                    case "Push":
                        customStack.Push(data);
                        break;
                    case "Pop":
                        customStack.Pop();
                        break;
                    case "END":
                        customStack.End();
                        return;
                }
            }
        }
    }
}
