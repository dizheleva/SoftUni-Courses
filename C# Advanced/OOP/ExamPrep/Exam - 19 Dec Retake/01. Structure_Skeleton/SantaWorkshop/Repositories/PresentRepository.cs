using SantaWorkshop.Models.Presents.Contracts;
using SantaWorkshop.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace SantaWorkshop.Repositories
{
    public class PresentRepository : IRepository<IPresent>
    {
        private readonly HashSet<IPresent> _presents;

        public PresentRepository()
        {
            this._presents=new HashSet<IPresent>();
        }
        public IReadOnlyCollection<IPresent> Models => _presents;
        public void Add(IPresent model)
        {
            this._presents.Add(model);
        }

        public bool Remove(IPresent model)
        {
            return this._presents.Remove(model);
        }

        public IPresent FindByName(string name)
        {
            var dwarf = this._presents.FirstOrDefault(d => d.Name == name);
            return dwarf;
        }
    }
}