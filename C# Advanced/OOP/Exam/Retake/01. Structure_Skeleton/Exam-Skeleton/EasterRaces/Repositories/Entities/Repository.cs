using EasterRaces.Repositories.Contracts;
using System.Collections.Generic;

namespace EasterRaces.Repositories.Entities
{
    public abstract class Repository<T> : IRepository<T> //where T : ICar, IDriver, IRace
    {
        private List<T> models;
        protected Repository()
        {
            this.models = new List<T>();
        }

        public abstract void Add(T model);

        public abstract System.Collections.Generic.IReadOnlyCollection<T> GetAll();
        public abstract T GetByName(string name);

        public abstract bool Remove(T model);
    }
}