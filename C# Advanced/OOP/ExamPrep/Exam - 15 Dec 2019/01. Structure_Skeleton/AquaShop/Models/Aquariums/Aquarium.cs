using AquaShop.Models.Aquariums.Contracts;
using AquaShop.Models.Decorations.Contracts;
using AquaShop.Models.Fish.Contracts;
using AquaShop.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AquaShop.Models.Aquariums
{
    public abstract class Aquarium : IAquarium
    {
        private string _name;
        private readonly List<IDecoration> _decorations;
        private readonly List<IFish> _fishes;
        protected Aquarium(string name, int capacity)
        {
            this.Name = name;
            this.Capacity = capacity;
            this._decorations = new List<IDecoration>();
            this._fishes = new List<IFish>();
        }
        public string Name 
        {
            get => this._name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidAquariumName);
                }

                this._name = value;
            }
        }
        public int Capacity { get; }
        public int Comfort => this._decorations.Sum(d => d.Comfort);
        public ICollection<IDecoration> Decorations => this._decorations.AsReadOnly();
        public ICollection<IFish> Fish => this._fishes.AsReadOnly();
        public void AddFish(IFish fish)
        {
            if (this._fishes.Count == this.Capacity)
            {
                throw new InvalidOperationException(ExceptionMessages.NotEnoughCapacity);
            }

            this._fishes.Add(fish);
        }

        public bool RemoveFish(IFish fish) => this._fishes.Remove(fish);
        public void AddDecoration(IDecoration decoration)
        {
            this._decorations.Add(decoration);
        }

        public void Feed()
        {
            foreach (var fish in this._fishes)
            {
                fish.Eat();
            }
        }

        public string GetInfo()
        {
            var result = new StringBuilder();
            var fishNames = !this._fishes.Any() ? "none" : string.Join(", ", this.Fish.Select(f => f.Name));

            result.AppendLine($"{this.Name} ({this.GetType().Name}):")
                .AppendLine($"Fish: {fishNames}")
                .AppendLine($"Decorations: {this.Decorations.Count}")
                .AppendLine($"Comfort: {this.Comfort}");
            
            return result.ToString().TrimEnd();
        }
    }
}