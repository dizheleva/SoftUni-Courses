using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaCalories
{
    public class Topping
    {
        private Dictionary<string, double> modifiers = new Dictionary<string, double>()
        {
            { "meat",1.2 },
            {"veggies",0.8 },
            {"cheese",1.1 },
            {"sauce", 0.9 },
        };

        private string type;
        private int weight;

        public Topping(string type, int weight)
        {
            this.Type = type;
            this.Weight = weight;
        }

        public string Type
        {
            get { return type; }
            private set
            {
                if (!modifiers.ContainsKey(value.ToLower()))
                {
                    throw new ArgumentException($"Cannot place {value} on top of your pizza.");
                }
                type = value;
            }
        }

        public int Weight
        {
            get { return weight; }
            private set
            {
                if (value < 1 || value > 50)
                {
                    throw new Exception($"{this.type} weight should be in the range [1..50].");
                }
                weight = value;
            }
        }

        public double GetCalories()
        {
            double calories = 2 * this.weight * modifiers[this.type.ToLower()];

            return calories;
        }
    }
}
