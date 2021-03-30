using System.Collections.Generic;

namespace WildFarm
{
    public class Hen : Bird
    {
        private const string FoodSound = "Cluck";
        private const double DefaultWeightRate = 0.35;
        private readonly List<string> personalFoods = new List<string>() { "Vegetable", "Fruit", "Meat", "Seeds" };

        public Hen(string name, double weight, double wingSize) : base(name, weight, wingSize)
        {
        }

        public override double WeightRate => DefaultWeightRate;
        public override string Sound => FoodSound;
        public override List<string> Foods => personalFoods;
    }
}
