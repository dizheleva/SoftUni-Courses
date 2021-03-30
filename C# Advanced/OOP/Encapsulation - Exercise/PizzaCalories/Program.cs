using System;

namespace PizzaCalories
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string[] pizzaArgs = Console.ReadLine().Split();
                string pizzaName = pizzaArgs[1];

                string[] doughArgs = Console.ReadLine().Split();
                string flourType = doughArgs[1];
                string bakingTechnique = doughArgs[2];
                int weight = int.Parse(doughArgs[3]);

                Dough dough = new Dough(flourType, bakingTechnique, weight);
                Pizza pizza = new Pizza(pizzaName, dough);

                string input = string.Empty;

                while ((input = Console.ReadLine()) != "END")
                {
                    string[] toppingArgs = input.Split();
                    string toppingType = toppingArgs[1];
                    int toppingWeight = int.Parse(toppingArgs[2]);

                    Topping topping = new Topping(toppingType, toppingWeight);
                    pizza.AddTopping(topping);
                }

                Console.WriteLine($"{pizza.Name} - {pizza.GetTotalCalories():F2} Calories.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
