using AquaShop.Core.Contracts;
using AquaShop.Models.Aquariums;
using AquaShop.Models.Aquariums.Contracts;
using AquaShop.Models.Decorations;
using AquaShop.Models.Decorations.Contracts;
using AquaShop.Models.Fish;
using AquaShop.Models.Fish.Contracts;
using AquaShop.Repositories;
using AquaShop.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AquaShop.Core
{
    public class Controller : IController
    {
        private readonly DecorationRepository _decorations;
        private readonly ICollection<IAquarium> _aquariums;
        public Controller()
        {
            this._decorations = new DecorationRepository();
            this._aquariums = new List<IAquarium>();
        }
        public string AddAquarium(string aquariumType, string aquariumName)
        {
            var aquarium = aquariumType switch
            {
                nameof(FreshwaterAquarium) => (IAquarium)new FreshwaterAquarium(aquariumName),
                nameof(SaltwaterAquarium) => new SaltwaterAquarium(aquariumName),
                _ => throw new InvalidOperationException(ExceptionMessages.InvalidAquariumType)
            };

            this._aquariums.Add(aquarium);
            return string.Format(OutputMessages.SuccessfullyAdded, aquariumType);
        }

        public string AddDecoration(string decorationType)
        {
            var decoration = decorationType switch
            {
                nameof(Ornament) => (IDecoration)new Ornament(),
                nameof(Plant) => new Plant(),
                _ => throw new InvalidOperationException(ExceptionMessages.InvalidDecorationType)
            };

            this._decorations.Add(decoration);
            return string.Format(OutputMessages.SuccessfullyAdded, decorationType);
        }

        public string InsertDecoration(string aquariumName, string decorationType)
        {
            var decoration = _decorations.FindByType(decorationType);
            if (decoration == null)
            {
                throw new InvalidOperationException(ExceptionMessages.InexistentDecoration);
            }

            this._aquariums.First(a=>a.Name==aquariumName).AddDecoration(decoration);
            return string.Format(OutputMessages.EntityAddedToAquarium, decorationType, aquariumName);
        }

        public string AddFish(string aquariumName, string fishType, string fishName, string fishSpecies, decimal price)
        {
            var aquarium = _aquariums.First(a => a.Name == aquariumName);
            var fish = fishType switch
            {
                nameof(FreshwaterFish) => (IFish)new FreshwaterFish(fishName, fishSpecies, price),
                nameof(SaltwaterFish) => new SaltwaterFish(fishName, fishSpecies, price),
                _ => throw new InvalidOperationException(ExceptionMessages.InvalidFishType)
            };

            var sameTypes = (aquarium.GetType().Name == nameof(FreshwaterAquarium) &&
                             fish.GetType().Name == nameof(FreshwaterFish))
                            || (aquarium.GetType().Name == nameof(SaltwaterAquarium) &&
                                fish.GetType().Name == nameof(SaltwaterFish));
            if (sameTypes)
            {
                aquarium.AddFish(fish);
            }

            var msgAdded = string.Format(OutputMessages.EntityAddedToAquarium, fishType, aquariumName);
            return string.Format(sameTypes ? msgAdded  : OutputMessages.UnsuitableWater);
        }

        public string FeedFish(string aquariumName)
        {
            this._aquariums.First(a=>a.Name==aquariumName).Feed();
            var fishCount = this._aquariums.First(a => a.Name == aquariumName).Fish.Count;

            return string.Format(OutputMessages.FishFed, fishCount);
        }

        public string CalculateValue(string aquariumName)
        {
            var aquarium = this._aquariums.First(a => a.Name == aquariumName);
            var sum = aquarium.Fish.Sum(f => f.Price)
                         + aquarium.Decorations.Sum(d => d.Price);

            return String.Format(OutputMessages.AquariumValue, aquariumName, sum);
        }

        public string Report()
        {
            var result = new StringBuilder();
            foreach (var aquarium in _aquariums)
            {
                result.AppendLine(aquarium.GetInfo());
            }

            return result.ToString().TrimEnd();
        }
    }
}