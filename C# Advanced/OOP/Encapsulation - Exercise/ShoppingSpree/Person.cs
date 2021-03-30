using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingSpree
{
    public class Person : IEnumerable<string>
    {
        private string name;
        private decimal money;
        private List<Product> shoppingBag;

        public Person(string name, decimal money)
        {
            this.Name = name;
            this.Money = money;
            this.shoppingBag = new List<Product>();
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Name cannot be empty");
                }
                name = value;
            }
        }

        public decimal Money
        {
            get => money;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Money cannot be negative");
                }
                money = value;
            }
        }

        public IReadOnlyCollection<Product> BagOfProducts => shoppingBag.AsReadOnly();

        public void BuyProduct(Product product)
        {
            if (product.Cost > this.Money)
            {
                Console.WriteLine($"{this.name} can't afford {product.Name}");
            }
            else
            {
                this.money -= product.Cost;
                shoppingBag.Add(product);
                Console.WriteLine($"{this.name} bought {product.Name}");
            }
        }

        public IEnumerator<string> GetEnumerator()
        {
            return this.shoppingBag.Select(product => product.Name).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
