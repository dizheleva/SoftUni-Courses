using System.Collections.Generic;

namespace WildFarm
{
    public class Mouse : Mammal
    {
        private const string FoodSound = "Squeak"; 
        private const double DefaultWeightRate = 0.10;
        private readonly List<string> personalFoods = new List<string>() { "Vegetable", "Fruit" };

        public Mouse(string name, double weight, string livingRegion) : base(name, weight, livingRegion)
        {
        }

        public override double WeightRate => DefaultWeightRate;
        public override string Sound => FoodSound;
        public override List<string> Foods => personalFoods;
        public override string ToString()
        {
            return base.ToString() + $"{this.Weight}, {this.LivingRegion}, {this.FoodEaten}]";
        }
    }
}
