using System;
using System.Collections.Generic;
using System.Linq;

namespace ForceBook
{
    class ForceBook
    {
        static void Main(string[] args)
        {
            var forceBook = new Dictionary<string, List<string>>();

            string input;

            while ((input = Console.ReadLine()) != "Lumpawaroo")
            {
                if (input.Contains('|'))
                {
                    var splitedInput = input.Split(" | ");
                    var side = splitedInput[0];
                    var user = splitedInput[1];

                    if (!forceBook.ContainsKey(side))
                    {
                        forceBook[side] = new List<string>();
                    }

                    var memberExists = forceBook.Any(kvp => kvp.Value.Contains(user));

                    if (!forceBook[side].Contains(user) && !memberExists)
                    {
                        forceBook[side].Add(user);
                    }
                }
                else
                {
                    var splitedInput = input.Split(" -> ");
                    var user = splitedInput[0];
                    var side = splitedInput[1];

                    var memberExists = false;
                    var userSide = string.Empty;

                    foreach (var kvp in forceBook.Where(kvp => kvp.Value.Contains(user)))
                    {
                        memberExists = true;
                        userSide = kvp.Key;
                        break;
                    }

                    if (memberExists)
                    {
                        forceBook[userSide].Remove(user);
                    }

                    if (!forceBook.ContainsKey(side))
                    {
                        forceBook[side] = new List<string>();
                    }

                    if (!forceBook[side].Contains(user))
                    {
                        forceBook[side].Add(user);
                        Console.WriteLine($"{user} joins the {side} side!");
                    }
                }
            }
            
            forceBook = forceBook
                .Where(x => x.Value.Count > 0)
                .OrderByDescending(x => x.Value.Count)
                .ThenBy(x => x.Key)
                .ToDictionary(x => x.Key, x => x.Value);

            foreach (var kvp in forceBook.Where(kvp => kvp.Value.Count != 0))
            {
                Console.WriteLine($"Side: {kvp.Key}, Members: {kvp.Value.Count}");

                var users = kvp.Value.OrderBy(x => x).ToList();

                foreach (var user in users)
                {
                    Console.WriteLine($"! {user}");
                }
            }
        }
    }
}
