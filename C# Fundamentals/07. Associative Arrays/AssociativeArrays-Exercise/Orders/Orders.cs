using System;
using System.Collections.Generic;

namespace Orders
{
    class Orders
    {
        static void Main(string[] args)
        {
            var products = new Dictionary<string, Item>();

            string input;

            while ((input = Console.ReadLine()) != "buy")
            {
                var splitedInput = input.Split();

                var product = splitedInput[0];
                var price = double.Parse(splitedInput[1]);
                var quantity = int.Parse(splitedInput[2]);

                if (!products.ContainsKey(product))
                {
                    products[product] = new Item(price, quantity);
                }
                else
                {
                    products[product].Price = price;
                    products[product].Quantity += quantity;
                }

            }

            foreach (var product in products)
            {
                var productName = product.Key;
                var productTotalPrice = product.Value.Quantity * product.Value.Price;
                Console.WriteLine($"{productName} -> {productTotalPrice:F2}");
            }
        }
    }
    class Item
    {
        public Item(double price, int quantity)
        {
            this.Price = price;
            this.Quantity = quantity;
        }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}
