using System.Collections.Generic;
using System.Text;

namespace MilitaryElite.Classes
{
    public class Commando : SpecialisedSoldier, ICommando
    {
        public Commando(int id, string firstName, string lastName, decimal salary, string crops,
            ICollection<IMission> missions)
            : base(id, firstName, lastName, salary, crops)
        {
            this.Missions = missions;
        }

        public ICollection<IMission> Missions { get; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(base.ToString());
            sb.AppendLine($"Corps: {this.Crops}");
            sb.AppendLine("Missions:");

            foreach (var mission in Missions)
            {
                sb.AppendLine($"  {mission}");
            }

            return sb.ToString().TrimEnd();

        }
    }
}