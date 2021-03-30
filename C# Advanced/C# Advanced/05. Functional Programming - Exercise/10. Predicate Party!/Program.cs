using System;
using System.Linq;

namespace _10._Predicate_Party_
{
    class Program
    {
        static void Main(string[] args)
        {
            Func<string, string, bool> startWithFunc = (x, y) => x.StartsWith(y);
            Func<string, string, bool> endsWithFunc = (x, y) => x.EndsWith(y);
            Func<string, int, bool> lengthFunc = (x, y) => x.Length == y;

            var names = Console.ReadLine()
                .Split()
                .ToList();

            var input = string.Empty;

            while ((input = Console.ReadLine()) != "Party!")
            {
                var inputArgs = input.Split();
                var command = inputArgs[0];
                var condition = inputArgs[1];
                var parameter = inputArgs[2];

                switch (command)
                {
                    case "Remove":
                        names = condition switch
                        {
                            "StartsWith" => names.Where(x => !startWithFunc(x, parameter)).ToList(),
                            "EndsWith" => names.Where(x => !endsWithFunc(x, parameter)).ToList(),
                            "Length" => names.Where(x => !lengthFunc(x, int.Parse(parameter))).ToList(),
                            _ => names
                        };
                        break;
                    case "Double":
                        for (var i = names.Count - 1; i >= 0; i--)
                        {
                            var currentName = names[i];
                            var index = names.IndexOf(currentName) + 1;
                            switch (condition)
                            {
                                case "StartsWith" when startWithFunc(currentName, parameter):
                                    names.Insert(index, currentName);
                                    break;
                                case "EndsWith" when endsWithFunc(currentName, parameter):
                                    names.Insert(index, currentName);
                                    break;
                                case "Length" when lengthFunc(currentName, int.Parse(parameter)):
                                    names.Insert(index, currentName);
                                    break;
                            }
                        }

                        break;
                }
            }

            Console.WriteLine(names.Count == 0
                ? "Nobody is going to the party!"
                : $"{string.Join(", ", names)} are going to the party!");
        }
    }
}
