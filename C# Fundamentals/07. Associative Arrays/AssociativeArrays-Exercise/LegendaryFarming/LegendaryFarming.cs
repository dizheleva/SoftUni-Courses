using System;
using System.Collections.Generic;
using System.Linq;

namespace LegendaryFarming
{
    class LegendaryFarming
    {
        static void Main(string[] args)
        {
            var materialsQuantity = new Dictionary<string, int>();

            materialsQuantity.Add("shards", 0);
            materialsQuantity.Add("fragments", 0);
            materialsQuantity.Add("motes", 0);

            var junks = new SortedDictionary<string, int>();

            var haveLegendaryItem = false;

            while (!haveLegendaryItem)
            {
                var input = Console.ReadLine().Split();

                for (var i = 0; i < input.Length; i += 2)
                {
                    var quantity = int.Parse(input[i]);
                    var material = input[i + 1].ToLower();

                    if (materialsQuantity.ContainsKey(material))
                    {
                        materialsQuantity[material] += quantity;
                    }
                    else if (junks.ContainsKey(material))
                    {
                        junks[material] += quantity;
                    }
                    else
                    {
                        junks.Add(material, quantity);
                    }
                    if (materialsQuantity["shards"] >= 250)
                    {
                        Console.WriteLine("Shadowmourne obtained!");
                        materialsQuantity["shards"] -= 250;
                        haveLegendaryItem = true;
                        break;
                    }
                    
                    if (materialsQuantity["fragments"] >= 250)
                    {
                        Console.WriteLine("Valanyr obtained!");
                        materialsQuantity["fragments"] -= 250;
                        haveLegendaryItem = true;
                        break;
                    }
                    
                    if (materialsQuantity["motes"] >= 250)
                    {
                        Console.WriteLine("Dragonwrath obtained!");
                        materialsQuantity["motes"] -= 250;
                        haveLegendaryItem = true;
                        break;
                    }
                }
            }

            var sortedMaterials = materialsQuantity.OrderByDescending(x => x.Value).ThenBy(x => x.Key);
            
            foreach (var currentMaterial in sortedMaterials)
            {
                Console.WriteLine($"{currentMaterial.Key}: {currentMaterial.Value}");
            }
            
            foreach (var junk in junks)
            {
                Console.WriteLine($"{junk.Key}: {junk.Value}");
            }
        }
    }
}
