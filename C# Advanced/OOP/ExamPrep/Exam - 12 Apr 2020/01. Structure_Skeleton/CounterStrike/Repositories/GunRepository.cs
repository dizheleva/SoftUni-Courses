using CounterStrike.Models.Guns.Contracts;
using CounterStrike.Repositories.Contracts;
using CounterStrike.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CounterStrike.Repositories
{
    public class GunRepository : IRepository<IGun>

    {
        private readonly HashSet<IGun> guns;

        public GunRepository()
        {
            this.guns = new HashSet<IGun>();
        }
        public IReadOnlyCollection<IGun> Models => guns;
        public void Add(IGun model)
        {
            if (model==null)
            {
                throw new ArgumentException(ExceptionMessages.InvalidGunRepository);
            }

            guns.Add(model);
        }

        public bool Remove(IGun model)
        {
            if (!guns.Contains(model)) return false;
            guns.Remove(model);
            return true;

        }

        public IGun FindByName(string name)
        {
            var gun = guns.FirstOrDefault(g => g.Name == name);
            return gun;
        }
    }
}