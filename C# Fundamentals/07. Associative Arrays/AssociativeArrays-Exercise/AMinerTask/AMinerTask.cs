using System;
using System.Collections.Generic;

namespace AMinerTask
{
    class AMinerTask
    {
        static void Main(string[] args)
        {
            var resourcesQuantity = new Dictionary<string, int>();

            string resource;

            while ((resource = Console.ReadLine()) != "stop")
            {
                var quantity = int.Parse(Console.ReadLine());

                if (resourcesQuantity.ContainsKey(resource))
                {
                    resourcesQuantity[resource] += quantity;
                }
                else
                {
                    resourcesQuantity.Add(resource, quantity);
                }
            }

            foreach (var currentResource in resourcesQuantity)
            {
                Console.WriteLine($"{currentResource.Key} -> {currentResource.Value}");
            }
        }
    }
}
