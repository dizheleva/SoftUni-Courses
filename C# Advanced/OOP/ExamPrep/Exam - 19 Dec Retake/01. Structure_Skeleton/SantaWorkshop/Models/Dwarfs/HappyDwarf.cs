namespace SantaWorkshop.Models.Dwarfs
{
    public class HappyDwarf : Dwarf
    {
        private const int DefaultEnergy = 100;
        public HappyDwarf(string name, int energy = DefaultEnergy) : base(name, energy)
        {
        }
    }
}