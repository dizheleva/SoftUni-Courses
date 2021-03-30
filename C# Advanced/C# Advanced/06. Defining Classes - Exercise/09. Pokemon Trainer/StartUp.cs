using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace DefiningClasses
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var trainers = new HashSet<Trainer>();

            var input = string.Empty;

            while ((input = Console.ReadLine()) != "Tournament")
            {
                var inputInfo = input.Split();

                var trainerName = inputInfo[0];
                var pokemonName = inputInfo[1];
                var pokemonElement = inputInfo[2];
                var pokemonHealth = int.Parse(inputInfo[3]);
                var trainer = new Trainer(trainerName);
                var pokemon = new Pokemon(pokemonName, pokemonElement, pokemonHealth);
                if (!trainers.Any(x => x.Name.Equals(trainerName)))
                {
                    trainers.Add(trainer);
                }
                trainers.First(x=>x.Name==trainerName).Pokemons.Add(pokemon);
            }

            var element = string.Empty;

            while ((element = Console.ReadLine()) != "End")
            {
                foreach (var trainer in trainers)
                {
                    if (trainer.Pokemons.Any(x => x.Element == element))
                    {
                        trainer.Badges++;
                    }
                    else
                    {
                        foreach (var pokemon in trainer.Pokemons)
                        {
                            pokemon.Health -= 10;
                        }
                    }
                    trainer.Pokemons = trainer.Pokemons.Where(x => x.Health > 0).ToList();
                }
            }

            foreach (var trainer in trainers.OrderByDescending(x => x.Badges))
            {
                Console.WriteLine($"{trainer.Name} {trainer.Badges} {trainer.Pokemons.Count}");
            }
        }
    }
}
