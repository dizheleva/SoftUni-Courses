using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace regex
{
    class Program
    {
        static void Main(string[] args)
        {            
            string healthPattern = @"";
            string damagePattern = @"";
            char[] charSeparators = new char[] { ',', ' ' };
            var names = Console.ReadLine().Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);
            var healthPoints = new SortedDictionary<string, double>().OrderBy(x => x);
            var damagePoints = new SortedDictionary<string, double>().OrderBy(x => x);
            foreach (var name in names)
            {
                MatchCollection damageMatches = Regex.Matches(name, damagePattern);
                MatchCollection healthMatches = Regex.Matches(name, healthPattern);
                double numberSum = 0.0;
                int healthSum = 0;
                int multiplyers = 0;
                int dividers = 0;
                foreach (var ch in name)
                {
                    if (ch=='/')
                    {
                        dividers++;
                    }
                    else if (ch == '*')
                    {
                        multiplyers++;
                    }
                }
                foreach (Match match in damageMatches)
                {
                    numberSum += double.Parse(match.Groups["number"].Value);
                }
                foreach (Match match in healthMatches)
                {
                    healthSum += int.Parse(match.Groups["symbol"].Value);
                }
                int x, y;
                x = multiplyers != 0 ? 2 * multiplyers : 1;
                y = dividers != 0 ? 2 * dividers : 1;
                double totalDamage = numberSum*x/y;
                healthPoints.Add(name, healthSum);
            }

            //var attackedPlanets = new List<string>();
            //var destroyedPlanets = new List<string>();
            //int n = int.Parse(Console.ReadLine());
            //for (int i = 0; i <n; i++)
            //{
            //    string input = Console.ReadLine();
            //    int counter = 0;
            //    foreach (var ch in input)
            //    {
            //        char[] code = new char[] { 's', 't', 'a', 'r' };
            //        if (code.Contains(Char.ToLower(ch)))
            //        {
            //            counter++;
            //        }
            //    }
            //    StringBuilder sb = new StringBuilder();
            //    foreach (char item in input)
            //    {
            //        sb.Append((char)(item-counter));
            //    }
            //    string message = sb.ToString();
            //    Match match = Regex.Match(message, pattern);
            //    if (match.Success)
            //    {
            //        if (match.Groups["type"].Value=="A")
            //        {
            //            attackedPlanets.Add(match.Groups["planet"].Value);
            //        }
            //        else
            //        {
            //            destroyedPlanets.Add(match.Groups["planet"].Value);
            //        }
            //    }

            //}
            //Console.WriteLine($"Attacked planets: {attackedPlanets.Count}");
            //foreach (var planet in attackedPlanets.OrderBy(x => x))
            //{
            //    Console.WriteLine($"-> {planet}");
            //}
            //Console.WriteLine($"Destroyed planets: {destroyedPlanets.Count}");
            //foreach (var planet in destroyedPlanets.OrderBy(x=>x))
            //{
            //    Console.WriteLine($"-> {planet}");
            //}
        }

    }
}
