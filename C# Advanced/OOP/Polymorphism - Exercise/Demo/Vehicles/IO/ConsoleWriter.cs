using System;

namespace Vehicles.IO
{
    public class ConsoleWriter : IWriter
    {
        public void CustomWriteLine(string text)
        {
            Console.WriteLine(text);
        }

        public void CustomWrite(string text)
        {
            Console.Write(text);
        }
    }
}
