using System.Text;

namespace Parking
{
    public class Car
    {
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public Car()
        {
        }
        public Car(string name, string model, int year)
        {
            Manufacturer = name;
            Model = model;
            Year = year;
        }
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{Manufacturer} {Model} ({Year})");
            return sb.ToString().TrimEnd();
        }
    }
}
