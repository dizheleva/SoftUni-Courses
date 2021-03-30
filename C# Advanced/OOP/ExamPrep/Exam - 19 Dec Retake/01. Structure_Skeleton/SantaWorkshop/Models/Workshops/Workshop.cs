using SantaWorkshop.Models.Dwarfs.Contracts;
using SantaWorkshop.Models.Presents.Contracts;
using SantaWorkshop.Models.Workshops.Contracts;
using System.Linq;

namespace SantaWorkshop.Models.Workshops
{
    public class Workshop : IWorkshop
    {
        public Workshop()
        {
        }
        public void Craft(IPresent present, IDwarf dwarf)
        {
            while (!present.IsDone() && dwarf.Energy > 0 && dwarf.Instruments.Any(i => i.IsBroken() == false))
            {
                dwarf.Work();
                dwarf.Instruments.First(i => i.IsBroken() == false).Use();
                present.GetCrafted();
            }
        }
    }
}