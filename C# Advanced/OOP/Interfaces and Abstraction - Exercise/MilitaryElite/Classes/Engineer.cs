using System.Collections.Generic;
using System.Text;

namespace MilitaryElite.Classes
{
    public class Engineer : SpecialisedSoldier, IEngineer
    {
        public Engineer(int id, string firstName, string lastName, decimal salary, string crops, ICollection<IRepair> repairs)
            : base(id, firstName, lastName, salary, crops)
        {
            Repairs = repairs;
        }

        public ICollection<IRepair> Repairs { get; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(base.ToString());
            sb.AppendLine($"Corps: {this.Crops}");
            sb.AppendLine("Repairs:");

            foreach (var repair in Repairs)
            {
                sb.AppendLine($"  {repair}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}