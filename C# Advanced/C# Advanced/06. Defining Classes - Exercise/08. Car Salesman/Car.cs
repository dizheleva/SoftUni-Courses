using System.Text;

namespace DefiningClasses
{
    public class Car
    {
        private string model;
        private Engine engine;
        private int weight;
        private string color;

        public Car(string model, Engine engine)
        {
            this.model = model;
            this.engine = engine;
            this.weight = 0;
            this.color = "n/a";
        }

        public string Model
        {
            get => this.model;
            set => this.model = value;
        }

        public Engine Engine
        {
            get { return this.engine; }
            set { this.engine = value; }
        }

        public int Weight
        {
            get { return this.weight; }
            set { this.weight = value; }
        }

        public string Color
        {
            get { return this.color; }
            set { this.color = value; }
        }

        public string PrintCar()
        {
            var carDetails = new StringBuilder();

            carDetails.AppendLine($"{Model}:");
            carDetails.AppendLine($"  {Engine.Model}:");
            carDetails.AppendLine($"    Power: {Engine.Power}");

            carDetails.AppendLine(Engine.Displacement == 0
                ? $"    Displacement: n/a"
                : $"    Displacement: {Engine.Displacement}");

            carDetails.AppendLine($"    Efficiency: {Engine.Efficiency}");

            carDetails.AppendLine(Weight == 0 
                ? $"  Weight: n/a" 
                : $"  Weight: {Weight}");

            carDetails.Append($"  Color: {Color}");

            return carDetails.ToString();
        }
    }
}
