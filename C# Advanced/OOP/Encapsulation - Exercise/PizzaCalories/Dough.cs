using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaCalories
{
    public class Dough
    {
        private Dictionary<string, double> modifiers = new Dictionary<string, double>()
        {
            {"white",1.5 },
            {"wholegrain",1.0 },
            {"crispy",0.9 },
            {"chewy", 1.1 },
            {"homemade",1.0 },
        };
        private string flourType;
        private string bakingTechnique;
        private double weight;

        public Dough(string flourType, string bakingTechnique, int weight)
        {
            this.FlourType = flourType;
            this.BakingTechnique = bakingTechnique;
            this.Weight = weight;
        }

        public string FlourType
        {
            get { return flourType; }
            private set
            {
                if (!(value.ToLower() == "white" ||
                    value.ToLower() == "wholegrain"))
                {
                    throw new ArgumentException("Invalid type of dough.");
                }
                flourType = value;
            }
        }

        public string BakingTechnique
        {
            get { return bakingTechnique; }
            private set
            {
                if (!(value.ToLower() == "crispy" ||
                    value.ToLower() == "chewy" ||
                    value.ToLower() == "homemade"))
                {
                    throw new ArgumentException("Invalid type of dough.");
                }
                bakingTechnique = value;
            }
        }

        public double Weight
        {
            get { return weight; }
            private set
            {
                if (value < 1 || value > 200)
                {
                    throw new ArgumentException("Dough weight should be in the range [1..200].");
                }
                weight = value;
            }
        }

        public double GetCalories()
        {
            double flourCalories = modifiers[this.flourType.ToLower()];
            double bakingCalories = modifiers[this.bakingTechnique.ToLower()];
            double calories = 2 * weight * flourCalories * bakingCalories;

            return calories;
        }
    }
}
