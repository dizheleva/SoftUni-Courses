using System;
using System.Collections.Generic;
using System.Linq;

namespace _08._Ranking
{
    class Program
    {
        static void Main(string[] args)
        {
            var contestData = new Dictionary<string, string>();
            var input = String.Empty;

            while ((input = Console.ReadLine()) != "end of contests")
            {
                var data = input.Split(':');
                var contestName = data[0];
                var password = data[1];
                if (!contestData.ContainsKey(contestName))
                {
                    contestData[contestName] = password;
                }
            }
            var candidates = new Dictionary<string, Dictionary<string, int>>();

            while ((input = Console.ReadLine()) != "end of submissions")
            {
                var data = input.Split("=>");
                var contestName = data[0];
                var password = data[1];
                var candidate = data[2];
                var points = int.Parse(data[3]);
                if (!contestData.ContainsKey(contestName) || contestData[contestName] != password) 
                    continue;

                if (!candidates.ContainsKey(candidate))
                {
                    candidates.Add(candidate, new Dictionary<string, int>());
                }

                if (candidates[candidate].ContainsKey(contestName))
                {
                    if (candidates[candidate][contestName] < points)
                    {
                        candidates[candidate][contestName] = points;
                    }
                }
                else
                {
                    candidates[candidate].Add(contestName, points);
                }
            }

            var bestCandidate = candidates
                .OrderByDescending(x => x.Value.Sum(s => s.Value))
                .FirstOrDefault();

            Console.WriteLine($"Best candidate is {bestCandidate.Key} with total {bestCandidate.Value.Sum(s => s.Value)} points.");
            Console.WriteLine("Ranking: ");

            foreach (var candidate in candidates.OrderBy(x => x.Key).ThenBy(x => x.Value))
            {
                Console.WriteLine($"{candidate.Key}");

                foreach (var (contest, points) in candidate.Value.OrderByDescending(x => x.Value))
                {
                    Console.WriteLine($"#  {contest} -> {points}");
                }
            }
        }
    }
}
