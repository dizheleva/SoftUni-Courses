using System.Collections.Generic;

namespace WildFarm
{
    public class Dog : Mammal
    {
        private const string FoodSound = "Woof!";
        private const double DefaultWeightRate = 0.40;
        private readonly List<string> personalFoods = new List<string>() { "Meat" };

        public Dog(string name, double weight, string livingRegion) : base(name, weight, livingRegion)
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