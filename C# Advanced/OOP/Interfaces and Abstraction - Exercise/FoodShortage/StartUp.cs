using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodShortage
{
    public class StartUp
    {
        public static void Main()
        {
            var buyers = new HashSet<IBuyer>();

            var numberOfPeople = int.Parse(Console.ReadLine());

            for (var i = 0; i < numberOfPeople; i++)
            {
                var input = Console.ReadLine().Split();

                if (input.Length == 3)
                {
                    var name = input[0];
                    var age = int.Parse(input[1]);
                    var group = input[2];

                    buyers.Add(new Rebel(name, age, group));
                }
                else
                {
                    var name = input[0];
                    var age = int.Parse(input[1]);
                    var id = input[2];
                    var birthdate = input[3];

                    buyers.Add(new Citizen(name, age, id, birthdate));
                }
            }

            var buyerName = string.Empty;

            while ((buyerName = Console.ReadLine()) != "End")
            {
                if (buyers.Any(buyer => buyer.Name == buyerName))
                {
                    buyers.First(buyer => buyer.Name==buyerName).BuyFood();
                }
            }

            var totalFood = buyers.Sum(x => x.Food);

            Console.WriteLine(totalFood); ;
        }
    }
}