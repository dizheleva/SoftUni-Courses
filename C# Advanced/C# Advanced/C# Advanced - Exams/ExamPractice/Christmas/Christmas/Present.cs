using System.Text;

namespace Christmas
{
    public class Present
    {
        public string Name { get; set; }
        public double Weight { get; set; }
        public string Gender { get; set; }

        public Present()
        {
        }
        public Present(string name, double weight, string gender)
        {
            Name = name;
            Weight = weight;
            Gender = gender;
        }
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Present {Name} ({Weight}) for a {Gender}");
            return sb.ToString().TrimEnd();
        }
    }
}
