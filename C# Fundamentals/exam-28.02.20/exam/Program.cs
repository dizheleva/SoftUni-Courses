using System;
using System.Collections.Generic;
using System.Linq;

namespace exam
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = new Dictionary<string, Dictionary<string, int>>();
            while (true)
            {
                string line = Console.ReadLine();
                if (line == "Sail")
                {
                    break;
                }
                string[] command = line.Split("||");
                if (!data.ContainsKey(command[0]))
                {
                    data.Add(command[0], new Dictionary<string, int> { ["people"]=int.Parse(command[1]), ["gold"]=int.Parse(command[2]) });
                }
                else
                {
                    data[command[0]]["people"] += int.Parse(command[1]);
                    data[command[0]]["gold"] += int.Parse(command[2]);
                }
            }
            while (true)
            {
                string line = Console.ReadLine();
                if (line == "End")
                {
                    break;
                }
                string[] command = line.Split("=>");
                switch (command[0])
                {
                    case "Prosper":
                        {
                            if (int.Parse(command[2]) < 0)
                            {
                                Console.WriteLine($"Gold added cannot be a negative number!");
                            }
                            else
                            {
                                data[command[1]]["gold"] += int.Parse(command[2]);
                                Console.WriteLine($"{command[2]} gold added to the city treasury. {command[1]} now has {data[command[1]]["gold"]} gold.");
                            }
                        }
                        break;
                    case "Plunder":
                        {
                            Console.WriteLine($"{command[1]} plundered! {command[3]} gold stolen, {command[2]} citizens killed.");
                            data[command[1]]["people"] -= int.Parse(command[2]);
                            data[command[1]]["gold"] -= int.Parse(command[3]);
                            if (data[command[1]]["people"] == 0 || data[command[1]]["gold"] == 0)
                            {
                                data.Remove(command[1]);
                                Console.WriteLine($"{command[1]} has been wiped off the map!");
                            }
                        }
                        break;
                }
            }
            if (data.Count > 0)
            {
                Console.WriteLine($"Ahoy, Captain! There are {data.Count} wealthy settlements to go to:");
                foreach (var item in data.OrderByDescending(x => x.Value["gold"]).ThenBy(n => n.Key))
                {
                    Console.WriteLine($"{item.Key} -> Population: {item.Value["people"]} citizens, Gold: {item.Value["gold"]} kg");
                }
            }
            else
            {
                Console.WriteLine("Ahoy, Captain! All targets have been plundered and destroyed!");
            }
        }
    }
}