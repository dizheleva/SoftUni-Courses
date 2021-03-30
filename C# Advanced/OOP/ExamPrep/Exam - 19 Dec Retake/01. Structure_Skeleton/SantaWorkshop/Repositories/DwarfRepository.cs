using SantaWorkshop.Models.Dwarfs.Contracts;
using SantaWorkshop.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace SantaWorkshop.Repositories
{
    public class DwarfRepository : IRepository<IDwarf>
    {
        private readonly HashSet<IDwarf> _dwarfs;

        public DwarfRepository()
        {
            this._dwarfs = new HashSet<IDwarf>();
        }
        public IReadOnlyCollection<IDwarf> Models => _dwarfs;
        public void Add(IDwarf model)
        {
            this._dwarfs.Add(model);
        }

        public bool Remove(IDwarf model)
        {
            return this._dwarfs.Remove(model);
        }

        public IDwarf FindByName(string name)
        {
            var dwarf = this._dwarfs.FirstOrDefault(d => d.Name == name);
            return dwarf;
        }
    }
}