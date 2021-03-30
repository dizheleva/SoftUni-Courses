using System;
using System.Collections.Generic;
using System.Linq;

namespace OddOccurrences
{
    class OddOccurrences
    {
        static void Main(string[] args)
        {
            var words = Console.ReadLine().Split().ToArray();

            var counts = new Dictionary<string, int>();

            foreach (var word in words)
            {
                var wordInLowercase = word.ToLower();

                if (counts.ContainsKey(wordInLowercase))
                {
                    counts[wordInLowercase]++;
                }
                else
                {
                    counts.Add(wordInLowercase, 1);
                }
            }

            foreach (var count in counts)
            {
                if (count.Value % 2 != 0)
                {
                    Console.Write(count.Key + " ");
                }
            }
        }
    }
}
