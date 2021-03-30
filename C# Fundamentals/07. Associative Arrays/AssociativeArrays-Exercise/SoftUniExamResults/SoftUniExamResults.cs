using System;
using System.Collections.Generic;
using System.Linq;

namespace SoftUniExamResults
{
    class SoftUniExamResults
    {
        static void Main(string[] args)
        {
            var studentsResults = new Dictionary<string, int>();
            var submissions = new Dictionary<string, int>();

            string input;

            while ((input = Console.ReadLine()) != "exam finished")
            {
                var splitedInput = input.Split('-');

                var userName = splitedInput[0];
                var language = splitedInput[1];

                if (language == "banned" && studentsResults.ContainsKey(userName))
                {
                    studentsResults.Remove(userName);
                }
                else
                {
                    var points = int.Parse(splitedInput[2]);

                    if (!studentsResults.ContainsKey(userName))
                    {
                        studentsResults[userName] = points;
                    }

                    if (studentsResults[userName] < points)
                    {
                        studentsResults[userName] = points;
                    }

                    if (!submissions.ContainsKey(language))
                    {
                        submissions[language] = 0;
                    }
                    submissions[language]++;
                }
            }

            studentsResults = studentsResults
                .OrderByDescending(x => x.Value)
                .ThenBy(x => x.Key)
                .ToDictionary(x => x.Key, x => x.Value);

            submissions = submissions
                .OrderByDescending(x => x.Value)
                .ThenBy(x => x.Key)
                .ToDictionary(x => x.Key, x => x.Value);

            Console.WriteLine("Results:");

            foreach (var kvp in studentsResults)
            {
                Console.WriteLine($"{kvp.Key} | {kvp.Value}");
            }

            Console.WriteLine("Submissions:");

            foreach (var kvp in submissions)
            {
                Console.WriteLine($"{kvp.Key} - {kvp.Value}");
            }
        }
    }
}
