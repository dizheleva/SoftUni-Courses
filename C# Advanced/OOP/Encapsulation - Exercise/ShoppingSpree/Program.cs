using System;
using System.Collections.Generic;

namespace ShoppingSpree
{
    public class Program
    {
        static void Main(string[] args)
        {
            var personNames = Console.ReadLine().Split(';', StringSplitOptions.RemoveEmptyEntries);
            var productNames = Console.ReadLine().Split(';', StringSplitOptions.RemoveEmptyEntries);

            var people = new Dictionary<string, Person>();
            var products = new Dictionary<string, Product>();

            try
            {
                foreach (var person in personNames)
                {
                    var personArgs = person.Split('=', StringSplitOptions.RemoveEmptyEntries);
                    var name = personArgs[0];
                    var money = decimal.Parse(personArgs[1]);

                    if (!people.ContainsKey(name))
                    {
                        people.Add(name, new Person(name, money));
                    }
                }

                foreach (var product in productNames)
                {
                    var productArgs = product.Split('=', StringSplitOptions.RemoveEmptyEntries);
                    var name = productArgs[0];
                    var cost = decimal.Parse(productArgs[1]);

                    if (!products.ContainsKey(name))
                    {
                        products.Add(name, new Product(name, cost));
                    }
                }
                var input = string.Empty;

                while ((input = Console.ReadLine()) != "END")
                {
                    var inputArgs = input.Split(' ');
                    var personName = inputArgs[0];
                    var productName = inputArgs[1];

                    people[personName].BuyProduct(products[productName]);
                }

                foreach (var (name, person) in people)
                {
                    Console.Write($"{name} - ");

                    Console.WriteLine(person.BagOfProducts.Count == 0 ? "Nothing bought" : string.Join(", ", person));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
