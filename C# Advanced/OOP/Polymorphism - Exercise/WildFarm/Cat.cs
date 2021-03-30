using System.Collections.Generic;

namespace WildFarm
{
    public class Cat : Feline
    {
        private const string FoodSound = "Meow";
        private const double DefaultWeightRate = 0.30;
        private readonly List<string> personalFoods = new List<string>() { "Vegetable", "Meat"};
        public Cat(string name, double weight, string livingRegion, string breed) : base(name, weight, livingRegion, breed)
        {
        }

        public override double WeightRate => DefaultWeightRate;
        public override string Sound => FoodSound;
        public override List<string> Foods => personalFoods;
    }
}
