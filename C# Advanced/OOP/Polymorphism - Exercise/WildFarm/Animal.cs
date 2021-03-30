using System;
using System.Collections.Generic;

namespace WildFarm
{
    public abstract class Animal
    {
        protected Animal(string name, double weight)
        {
            this.Name = name;
            this.Weight = weight;
        }
        public string Name { get; }
        public double Weight { get; private set; }
        public abstract double WeightRate { get; }
        public int FoodEaten { get; private set; }
        public abstract string Sound { get; }
        public abstract List<string> Foods { get; }
        public string AskFood()
        {
            return this.Sound;
        }

        public void Eat(Food food)
        {
            if (this.Foods.Contains(food.GetType().Name))
            {
                this.FoodEaten += food.Quantity;
                this.Weight += this.WeightRate * food.Quantity;
            }
            else
            {
                throw new ArgumentException($"{this.GetType().Name} does not eat {food.GetType().Name}!");
            }
        }
        public override string ToString()
        {
            return $"{this.GetType().Name} [{this.Name}, ";
        }
    }
}
