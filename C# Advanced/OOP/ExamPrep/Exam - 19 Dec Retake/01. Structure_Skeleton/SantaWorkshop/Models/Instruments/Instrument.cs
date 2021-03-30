using SantaWorkshop.Models.Instruments.Contracts;

namespace SantaWorkshop.Models.Instruments
{
    public class Instrument : IInstrument
    {
        private int _power;
        public Instrument(int power)
        {
            this.Power = power;
        }
        public int Power 
        {
            get => this._power;
            private set
            {
                if (value < 0)
                {
                    value = 0;
                }

                this._power = value;
            }
        }
        public void Use()
        {
            this.Power -= 10;
        }

        public bool IsBroken() => this.Power == 0;
    }
}