using CommandPattern.Core.Contracts;
using System;

namespace CommandPattern.Core
{
    public class Engine : IEngine
    {
        private readonly ICommandInterpreter _commandInterpreter;

        public Engine(ICommandInterpreter commandInterpreter)
        {
            this._commandInterpreter = commandInterpreter;
        }
        public void Run()
        {
            while (true)
            {
                var input = Console.ReadLine();
                var result = this._commandInterpreter.Read(input);
                Console.WriteLine(result);
            }
        }
    }
}
