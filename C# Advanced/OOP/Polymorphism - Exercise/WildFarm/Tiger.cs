using System.Collections.Generic;

namespace WildFarm
{
    public class Tiger : Feline
    {
        private const string FoodSound = "ROAR!!!";
        private const double DefaultWeightRate = 1.00;
        private readonly List<string> _personalFoods = new List<string>() { "Meat" };

        public Tiger(string name, double weight, string livingRegion, string breed) : base(name, weight, livingRegion, breed)
        {
        }

        public override double WeightRate => DefaultWeightRate;
        public override string Sound => FoodSound;
        public override List<string> Foods => _personalFoods;
    }
}
