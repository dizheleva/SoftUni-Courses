using System.Collections.Generic;

namespace WildFarm
{
    public class Owl : Bird
    {
        private const string FoodSound = "Hoot Hoot";
        private const double DefaultWeightRate = 0.25;
        private readonly List<string> personalFoods = new List<string>() { "Meat" };

        public Owl(string name, double weight, double wingSize) : base(name, weight, wingSize)
        {
        }

        public override double WeightRate => DefaultWeightRate;
        public override string Sound => FoodSound;
        public override List<string> Foods => personalFoods;
    }
}
