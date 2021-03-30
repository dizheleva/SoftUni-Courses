using CommandPattern.Core.Commands;
using CommandPattern.Core.Contracts;
using System;
using System.Linq;
using System.Reflection;

namespace CommandPattern.Core
{
    public class CommandInterpreter : ICommandInterpreter
    {
        private const string CommandPostfix = "Command";
        public string Read(string args)
        {
            var tokens = args.Split();
            var commandName = tokens[0];

            //Using Reflection
            var commandTypeName = commandName + CommandPostfix;

            Type commandType = Assembly
                .GetCallingAssembly()
                .GetTypes()
                .Where(t => t.GetInterfaces()
                    .Any(i => i.Name == nameof(ICommand))
                )
                .FirstOrDefault(t=> t.Name == commandTypeName);

            if (commandType == null)
            {
                throw  new InvalidOperationException("Invalid command name!");
            }

            ICommand command = Activator.CreateInstance(commandType) as ICommand;
            // tova castvane commandObject as ICommand vodi do res null, ako ne moje da se slu4i, dokato s (ICommand)commandObject 6te polu4im exeption

            //ICommand command = commandName switch
            // {
            //     "Hello" => new HelloCommand(),
            //     "Exit" => new ExitCommand(),
            //     _ => throw  new InvalidOperationException("Invalid command name!")
            // };
            //ICommand command = null;

            var neededData = tokens.Skip(1).ToArray();
            var result = command.Execute(neededData);

            return result;
        }
    }
}
