using System;
using System.Collections.Generic;

namespace Animals
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var animals = new List<Animal>();

            var input = string.Empty;

            while ((input = Console.ReadLine()) != "Beast!")
            {
                var animalInfo = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                var name = animalInfo[0];
                var age = int.Parse(animalInfo[1]);
                var gender = animalInfo[2];

                try
                {
                    Animal animal = input switch
                    {
                        "Dog" => new Dog(name, age, gender),
                        "Cat" => new Cat(name, age, gender),
                        "Frog" => new Frog(name, age, gender),
                        "Kitten" => new Kitten(name, age),
                        "Tomcat" => new Tomcat(name, age),
                        _ => throw new Exception("Invalid input!"),
                    };
                    animals.Add(animal);
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
            }
            foreach (var singleAnimal in animals)
            {
                Console.WriteLine(singleAnimal);
            }
        }
    }
}
