using System;

namespace Vehicles.IO
{
    public class ConsoleReader : IReader
    {
        public string CustomReadLine()
        {
            return Console.ReadLine();
        }
    }
}
