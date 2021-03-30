using System;
using System.Collections.Generic;

namespace WildFarm
{
    class Program
    {
        static void Main(string[] args)
        {
            var animals = new List<Animal>();

            var command = String.Empty;

            while ((command = Console.ReadLine()) != "End")
            {
                var animal = CreateAnimal(command.Split(' ', StringSplitOptions.RemoveEmptyEntries));
                animals.Add(animal);
                Console.WriteLine(animal.AskFood());
                var food = CreateFood(Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries));

                try
                {
                    animal.Eat(food);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            animals.ForEach(Console.WriteLine);
        }
        public static Animal CreateAnimal(string[] args)
        {
            var type = args[0];

            return type switch
            {
                nameof(Owl) => new Owl(args[1], double.Parse(args[2]), double.Parse(args[3])),
                nameof(Hen) => new Hen(args[1], double.Parse(args[2]), double.Parse(args[3])),
                nameof(Cat) => new Cat(args[1], double.Parse(args[2]), args[3], args[4]),
                nameof(Tiger) => new Tiger(args[1], double.Parse(args[2]), args[3], args[4]),
                nameof(Dog) => new Dog(args[1], double.Parse(args[2]), args[3]),
                nameof(Mouse) => new Mouse(args[1], double.Parse(args[2]), args[3]),
                _ => throw new ArgumentException($"{type} is not a valid Animal type.")
            };
        }
        public static Food CreateFood(string[] args)
        {
            var type = args[0];
            var quantity = int.Parse(args[1]);
            return args[0] switch
            {
                nameof(Vegetable) => new Vegetable(quantity),
                nameof(Fruit) => new Fruit(quantity),
                nameof(Meat) => new Meat(quantity),
                nameof(Seeds) => new Seeds(quantity),
                _ => throw new ArgumentException($"{type} is not a valid Food")
            };
        }
    }
}
