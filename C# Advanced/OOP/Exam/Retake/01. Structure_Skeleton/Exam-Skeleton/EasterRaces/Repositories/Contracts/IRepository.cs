using EasterRaces.Models.Cars.Contracts;
using EasterRaces.Models.Drivers.Contracts;
using EasterRaces.Models.Races.Contracts;

namespace EasterRaces.Repositories.Contracts
{
    using System.Collections.Generic;

    public interface IRepository<T>
    {
        T GetByName(string name);

        IReadOnlyCollection<T> GetAll();

        void Add(T model);

        bool Remove(T model);
    }
}
