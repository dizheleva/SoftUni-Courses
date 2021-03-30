using System;
using System.Linq;

namespace WordFilter
{
    class WordFilter
    {
        static void Main(string[] args)
        {
            var words = Console.ReadLine().Split().Where(w => w.Length % 2 == 0).ToArray();

            foreach (var word in words)
            {
                Console.WriteLine(word);
            }
        }
    }
}
