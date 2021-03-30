using System;
using System.Collections.Generic;

namespace CountCharsInAString
{
    class CountCharsInAString
    {
        static void Main(string[] args)
        {
            var text = Console.ReadLine().Split();

            var charCounts = new Dictionary<char, int>();

            foreach (var word in text)
            {
                foreach (var character in word)
                {
                    if (charCounts.ContainsKey(character))
                    {
                        charCounts[character]++;
                    }
                    else
                    {
                        charCounts.Add(character, 1);
                    }
                }
            }

            foreach (var currentChar in charCounts)
            {
                Console.WriteLine($"{currentChar.Key} -> {currentChar.Value}");
            }
        }
    }
}
