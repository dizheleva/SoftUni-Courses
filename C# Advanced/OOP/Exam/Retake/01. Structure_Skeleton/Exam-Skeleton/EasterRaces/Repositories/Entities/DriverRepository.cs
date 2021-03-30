using EasterRaces.Models.Drivers.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace EasterRaces.Repositories.Entities
{
    public class DriverRepository : Repository<IDriver>
    {
        private List<IDriver> models;

        public DriverRepository()
        {
            this.models = new List<IDriver>();
        }

        public override void Add(IDriver model)
        {
            this.models.Add(model);
        }

        public override bool Remove(IDriver model)
        {
            return this.models.Remove(model);
        }

        public override IReadOnlyCollection<IDriver> GetAll()
        {
            return this.models.AsReadOnly();
        }

        public override IDriver GetByName(string name)
        {
            return this.models.FirstOrDefault(m => m.Name == name);
        }
    }
}