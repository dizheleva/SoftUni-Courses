using EasterRaces.Models.Races.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace EasterRaces.Repositories.Entities
{
    public class RaceRepository : Repository<IRace>
    {
        private List<IRace> models;

        public RaceRepository()
        {
            this.models = new List<IRace>();
        }

        public override void Add(IRace model)
        {
            this.models.Add(model);
        }

        public override bool Remove(IRace model)
        {
            return this.models.Remove(model);
        }

        public override IReadOnlyCollection<IRace> GetAll()
        {
            return this.models.AsReadOnly();
        }

        public override IRace GetByName(string name)
        {
            return this.models.FirstOrDefault(m => m.Name == name);
        }
    }
}