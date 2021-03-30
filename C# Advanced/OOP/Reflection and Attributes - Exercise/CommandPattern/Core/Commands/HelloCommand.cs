using CommandPattern.Core.Contracts;

namespace CommandPattern.Core.Commands
{
    public  class HelloCommand : ICommand
    {
        public string Execute(string[] args)
        {
            var result = $"Hello, {args[0]}";
            return result;
        }
    }
}
