using System;
using System.Collections.Generic;
using System.Linq;

namespace _07._The_V_Logger
{
    class Program
    {
        static void Main(string[] args)
        {
            var vloggers = new Dictionary<string, Dictionary<string, SortedSet<string>>>();

            while (true)
            {
                var input = Console.ReadLine().Split();
                if (input[0] == "Statistics")
                {
                    break;
                }

                if (input.Contains("joined"))
                {
                    if (!vloggers.ContainsKey(input[0]))
                    {
                        vloggers[input[0]] = new Dictionary<string, SortedSet<string>>
                        {
                            ["following"] = new SortedSet<string>(),
                            ["followers"] = new SortedSet<string>()
                        };
                    }
                }
                else
                {
                    if (input[0] == input[2] 
                        || !vloggers.ContainsKey(input[0]) ||
                        !vloggers.ContainsKey(input[2]))
                        continue;

                    vloggers[input[0]]["following"].Add(input[2]);
                    vloggers[input[2]]["followers"].Add(input[0]);
                }
            }

            vloggers = 
                vloggers
                    .OrderByDescending(x => x.Value["followers"].Count)
                    .ThenBy(x => x.Value["following"].Count)
                    .ToDictionary(x => x.Key, x => x.Value);

            Console.WriteLine($"The V-Logger has a total of {vloggers.Keys.Count} vloggers in its logs.");

            var counter = 1;

            foreach (var (name, sets) in vloggers)
            {
                Console.WriteLine($"{counter}. {name} : {sets["followers"].Count} followers, {sets["following"].Count} following");

                if (counter == 1)
                {
                    foreach (var follower in sets["followers"])
                    {
                        Console.WriteLine($"*  {follower}");
                    }
                }
                counter++;
            }
        }
    }
}
