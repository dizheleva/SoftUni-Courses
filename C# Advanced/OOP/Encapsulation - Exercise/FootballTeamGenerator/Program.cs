using System;
using System.Collections.Generic;

namespace FootballTeamGenerator
{
    public class Program
    {
        static void Main(string[] args)
        {
            var teams = new Dictionary<string, Team>();

            var input = string.Empty;

            while ((input = Console.ReadLine()) != "END")
            {
                var inputArgs = input.Split(';', StringSplitOptions.RemoveEmptyEntries);

                var command = inputArgs[0];
                var teamName = inputArgs[1];

                if (command == "Team")
                {
                    if (!teams.ContainsKey(teamName))
                    {
                        try
                        {
                            teams.Add(teamName, new Team(teamName));
                        }
                        catch (ArgumentException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
                else if (command == "Add")
                {
                    var playerName = inputArgs[2];
                    var endurance = int.Parse(inputArgs[3]);
                    var sprint = int.Parse(inputArgs[4]);
                    var dribble = int.Parse(inputArgs[5]);
                    var passing = int.Parse(inputArgs[6]);
                    var shooting = int.Parse(inputArgs[7]);

                    if (!teams.ContainsKey(teamName))
                    {
                        Console.WriteLine($"Team {teamName} does not exist.");
                    }
                    else
                    {
                        try
                        {
                            var player = new Player(playerName, endurance, sprint, dribble, passing, shooting);
                            teams[teamName].AddPlayer(player);
                        }
                        catch (ArgumentException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
                else if (command == "Remove")
                {
                    var playerName = inputArgs[2];

                    if (!teams.ContainsKey(teamName))
                    {
                        Console.WriteLine($"Team {teamName} does not exist.");
                    }
                    else
                    {
                        try
                        {
                            teams[teamName].RemovePlayer(playerName);
                        }
                        catch (ArgumentException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
                else if (command == "Rating")
                {
                    if (!teams.ContainsKey(teamName))
                    {
                        Console.WriteLine($"Team {teamName} does not exist.");
                    }
                    else
                    {
                        teams[teamName].PrintRating();
                    }
                }
            }
        }
    }
}
