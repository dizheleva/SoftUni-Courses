using System;
using System.Collections.Generic;

namespace _01._Unique_Usernames
{
    class Program
    {
        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());
            var names = new HashSet<string>();
            for (int i = 0; i < n; i++)
            {
                names.Add(Console.ReadLine());
            }
            Console.WriteLine(String.Join("\n", names));
        }
    }
}
