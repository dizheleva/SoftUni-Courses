using System;
using System.Collections.Generic;
using System.Linq;

namespace BorderControl
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var fake = new List<IIdentifiable>();

            var input = string.Empty;

            while ((input = Console.ReadLine()) != "End")
            {
                var inputArgs = input.Split(' ');

                var countOfArgs = inputArgs.Length;

                switch (countOfArgs)
                {
                    case 3:
                    {
                        var name = inputArgs[0];
                        var age = int.Parse(inputArgs[1]);
                        var id = inputArgs[2];

                        fake.Add(new Citizen(name, age, id));
                        break;
                    }
                    case 2:
                    {
                        var name = inputArgs[0];
                        var id = inputArgs[1];

                        fake.Add(new Robot(name, id));
                        break;
                    }
                }
            }

            var specificNumber = Console.ReadLine();

            foreach (var p in fake.Where(x => x.Id.EndsWith(specificNumber)))
            {
                Console.WriteLine(p.Id);
            }
        }
    }
}
