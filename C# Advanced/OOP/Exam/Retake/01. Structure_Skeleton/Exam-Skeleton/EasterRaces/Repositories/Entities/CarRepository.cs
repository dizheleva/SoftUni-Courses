using EasterRaces.Models.Cars.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace EasterRaces.Repositories.Entities
{
    public class CarRepository : Repository<ICar>
    {
        private List<ICar> models;

        public CarRepository()
        {
            this.models = new List<ICar>();
        }

        public override void Add(ICar model)
        {
            this.models.Add(model);
        }

        public override bool Remove(ICar model)
        {
            return this.models.Remove(model);
        }

        public override IReadOnlyCollection<ICar> GetAll()
        {
            return this.models.AsReadOnly();
        }

        public override ICar GetByName(string name)
        {
            return this.models.FirstOrDefault(m => m.Model == name);
        }
    }
}