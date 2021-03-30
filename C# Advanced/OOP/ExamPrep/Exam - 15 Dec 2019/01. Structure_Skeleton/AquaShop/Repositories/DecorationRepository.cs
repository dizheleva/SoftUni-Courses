using AquaShop.Models.Decorations.Contracts;
using AquaShop.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace AquaShop.Repositories
{
    public class DecorationRepository : IRepository<IDecoration>
    {
        private readonly HashSet<IDecoration> _models;
        public DecorationRepository()
        {
            this._models = new HashSet<IDecoration>();
        }

        public IReadOnlyCollection<IDecoration> Models => this._models;
        public void Add(IDecoration model)
        {
            _models.Add(model);
        }

        public bool Remove(IDecoration model) => this._models.Remove(model);

        public IDecoration FindByType(string type)
        {
            var decoration = _models.FirstOrDefault(d => d.GetType().Name == type);
            return decoration;
        }
    }
}