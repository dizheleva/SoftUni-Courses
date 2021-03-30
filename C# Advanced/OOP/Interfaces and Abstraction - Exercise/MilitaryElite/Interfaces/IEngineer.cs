namespace MilitaryElite
{
    using System.Collections.Generic;

    public interface IEngineer : ISpecialisedSoldier
    {
        ICollection<IRepair> Repairs { get; }
    }
}
