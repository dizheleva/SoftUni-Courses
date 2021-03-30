using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rabbits
{
    public class Cage : Rabbit
    {
        private HashSet<Rabbit> rabbits;
        public string Name { get; set; }
        public int Capacity { get; set; }
        public int Count => rabbits.Count;

        private Cage()
        {
            rabbits = new HashSet<Rabbit>();
        }
        public Cage(string name, int capacity) : this()
        {
            Name = name;
            Capacity = capacity;
        }

        public void AddPlayer(Rabbit player)
        {
            if (Capacity > rabbits.Count)
            {
                rabbits.Add(player);
            }
        }

        public bool RemovePlayer(string name)
        {
            var playerIsRemoved = rabbits.Any(p => p.Name == name);
            if (playerIsRemoved)
            {
                rabbits.Remove(rabbits.First(p => p.Name == name));
            }
            return playerIsRemoved;
        }

        public void Add(Rabbit rabbit)
        {
            if (Capacity > rabbits.Count)
            {
                rabbits.Add(rabbit);
            }
        }
        public bool RemoveRabbit(string name)
        {
            var rabbitIsRemoved = rabbits.Any(p => p.Name == name);
            if (rabbitIsRemoved)
            {
                rabbits.Remove(rabbits.First(p => p.Name == name));
            }
            return rabbitIsRemoved;
        }
        public void RemoveSpecies(string species)
        {
            rabbits = rabbits.Where(r => r.Species != species).ToHashSet();
        }

        public Rabbit SellRabbit(string name)
        {
            Rabbit rabbit = rabbits.First(r => r.Name == name);
            rabbit.Available = false;
            return rabbit;
        }
        public Rabbit[] SellRabbitsBySpecies(string species)
        {
            Rabbit[] rabbitSpecies = rabbits.Where(r => r.Species == species).ToArray();
            foreach (var rabbit in rabbitSpecies)
            {
                rabbit.Available = false;
            }
            return rabbitSpecies;
        }
        public string Report()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Rabbits available at {this.Name}:");
            sb.AppendLine($"{ string.Join(Environment.NewLine, this.rabbits.Where(r=>r.Available==true))}");

            return sb.ToString().TrimEnd();
        }
    }
}
