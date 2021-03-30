using System;
using System.Collections.Generic;
using System.Linq;

namespace _11._The_Party_Reservation_Filter_Module
{
    class Program
    {
        static void Main(string[] args)
        {
            var names = Console.ReadLine()
                .Split()
                .ToList();
            var filters = new List<string>();
            var input = string.Empty;

            while ((input = Console.ReadLine()) != "Print")
            {
                var inputArgs = input.Split(';');
                var command = inputArgs[0];

                switch (command)
                {
                    case "Add filter":
                        filters.Add(input.Substring(command.Length+1));
                        break;
                    case "Remove filter":
                        filters.Remove(input.Substring(command.Length+1));
                        break;
                }
            }
            
            foreach (var filter in filters)
            {
                var command = filter.Split(';');
                var condition = command[0];
                var parameter = command[1];
                names = condition switch
                {
                    "Starts with" => names.Where(x => !x.StartsWith(parameter)).ToList(),
                    "Ends with" => names.Where(x => !x.EndsWith(parameter)).ToList(),
                    "Length" => names.Where(x => x.Length != int.Parse(parameter)).ToList(),
                    "Contains" => names.Where(x => !x.Contains(parameter)).ToList(),
                    _ => names
                };
            }
            Console.WriteLine(string.Join(" ", names));
        }
    }
}
