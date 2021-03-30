namespace MilitaryElite.Classes
{
    public abstract class SpecialisedSoldier : Private, ISpecialisedSoldier
    {
        protected SpecialisedSoldier(int id, string firstName, string lastName, decimal salary, string crops)
            : base(id, firstName, lastName, salary)
        {
            this.Crops = crops;
        }

        public string Crops { get; }
    }
}