using System.Text;

namespace Rabbits
{
    public class Rabbit
    {
        public string Name { get; set; }
        public string Species { get; set; }
        public bool Available { get; set; }
        public Rabbit()
        {
            Available = true;
        }
        public Rabbit(string name, string species)
            : this()
        {
            Name = name;
            Species = species;
        }
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Rabbit ({Species}): {Name}");

            return sb.ToString().TrimEnd();
        }
    }
}
