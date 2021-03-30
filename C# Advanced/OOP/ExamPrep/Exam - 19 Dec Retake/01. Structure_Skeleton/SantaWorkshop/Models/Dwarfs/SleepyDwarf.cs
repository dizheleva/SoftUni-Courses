namespace SantaWorkshop.Models.Dwarfs
{
    public class SleepyDwarf : Dwarf
    {
        private const int DefaultEnergy = 50;
        public SleepyDwarf(string name, int energy = DefaultEnergy) : base(name, energy)
        {
        }

        public override void Work()
        {
            base.Work();
            this.Energy -= 5;
        }
    }
}