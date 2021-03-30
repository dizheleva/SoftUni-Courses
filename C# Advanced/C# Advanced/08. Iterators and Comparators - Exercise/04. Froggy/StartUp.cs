using System;
using System.Linq;
using System.Threading.Channels;

namespace Froggy
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine()
                .Split(", ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();
            var output = new Lake(input);
            Console.WriteLine(string.Join(", ", output));
        }
    }
}
