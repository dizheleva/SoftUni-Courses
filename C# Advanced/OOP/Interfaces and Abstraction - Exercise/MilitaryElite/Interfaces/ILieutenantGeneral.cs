namespace MilitaryElite
{
    using System.Collections.Generic;

    public interface ILieutenantGeneral : IPrivate
    {
        ICollection<IPrivate> Privates { get; }
    }
}
