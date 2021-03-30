using System.IO;

namespace Vehicles.IO
{
    public class FileReader : IReader
    {
        private readonly string path;

        public FileReader(string path)
        {
            this.path = path;
        }
        public string CustomReadLine()
        {
            return File.ReadAllText(this.path);
        }
    }
}
