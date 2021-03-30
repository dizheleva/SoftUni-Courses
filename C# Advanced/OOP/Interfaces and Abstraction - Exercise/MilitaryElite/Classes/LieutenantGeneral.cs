using System.Collections.Generic;
using System.Text;

namespace MilitaryElite.Classes
{
    public class LieutenantGeneral : Private, ILieutenantGeneral
    {
        public LieutenantGeneral(int id, string firstName, string lastName, decimal salary, ICollection<IPrivate> privatesId)
            : base(id, firstName, lastName, salary)
        {
            this.Privates = privatesId;
        }

        public ICollection<IPrivate> Privates { get; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(base.ToString());
            sb.AppendLine("Privates:");
            foreach (var currentPrivate in Privates)
            {
                sb.AppendLine("  " + currentPrivate.ToString());
            }
            return sb.ToString().TrimEnd();
        }
    }
}