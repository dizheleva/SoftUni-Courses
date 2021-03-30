using SantaWorkshop.Models.Presents.Contracts;
using SantaWorkshop.Utilities.Messages;
using System;

namespace SantaWorkshop.Models.Presents
{
    public class Present : IPresent
    {
        private string _name;
        private int _energyRequired;
        public Present(string name, int energyRequired)
        {
            this.Name = name;
            this.EnergyRequired = energyRequired;
        }
        public string Name 
        {
            get => this._name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    var message = ExceptionMessages.InvalidPresentName;
                    throw new ArgumentException(message);
                }

                this._name = value;
            }
        }
        public int EnergyRequired 
        {
            get => this._energyRequired;
            private set
            {
                if (value < 0)
                {
                    value = 0;
                }

                this._energyRequired = value;
            }
        }
        public void GetCrafted()
        {
            this.EnergyRequired -= 10;
        }

        public bool IsDone() => this.EnergyRequired == 0;
    }
}