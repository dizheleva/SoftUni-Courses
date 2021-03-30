using System;
using System.Collections.Generic;
using System.Linq;

namespace BorderControl
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var population = new List<IBirthable>();

            var input = string.Empty;

            while ((input = Console.ReadLine()) != "End")
            {
                var inputArgs = input.Split(' ');

                var type = inputArgs[0];

                switch (type)
                {
                    case "Citizen":
                    {
                        var name = inputArgs[1];
                        var age = int.Parse(inputArgs[2]);
                        var id = inputArgs[3];
                        var birthdate = inputArgs[4];

                        population.Add(new Citizen(name, age, id, birthdate));
                        break;
                    }
                    case "Pet":
                    {
                        var name = inputArgs[1];
                        var birthdate = inputArgs[2];

                        population.Add(new Pet(name, birthdate));
                        break;
                    }
                }
            }

            var specificYear = Console.ReadLine();

            foreach (var p in population.Where(x => x.Birthdate.EndsWith(specificYear)))
            {
                Console.WriteLine(p.Birthdate);
            }
        }
    }
}
