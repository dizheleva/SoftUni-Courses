using System;
using System.Collections.Generic;

namespace SoftUniParking
{
    class SoftUniParking
    {
        static void Main(string[] args)
        {
            var plates = new Dictionary<string, string>();

            var commandsCount = int.Parse(Console.ReadLine());

            for (var i = 0; i < commandsCount; i++)
            {
                var command = Console.ReadLine().Split();

                var action = command[0];
                var userName = command[1];

                if (action == "register")
                {
                    var plateNumber = command[2];

                    if (plates.ContainsKey(userName))
                    {
                        Console.WriteLine($"ERROR: already registered with plate number {plates[userName]}");
                    }
                    else
                    {
                        plates[userName] = plateNumber;
                        Console.WriteLine($"{userName} registered {plateNumber} successfully");
                    }
                }
                else if (action == "unregister")
                {
                    if (plates.ContainsKey(userName))
                    {
                        plates.Remove(userName);
                        Console.WriteLine($"{userName} unregistered successfully");
                    }
                    else
                    {
                        Console.WriteLine($"ERROR: user {userName} not found");
                    }
                }
            }

            foreach (var plate in plates)
            {
                Console.WriteLine($"{plate.Key} => {plate.Value}");
            }
        }
    }
}
