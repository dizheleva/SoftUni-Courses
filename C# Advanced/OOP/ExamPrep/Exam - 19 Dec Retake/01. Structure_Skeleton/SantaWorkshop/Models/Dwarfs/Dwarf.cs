using SantaWorkshop.Models.Dwarfs.Contracts;
using SantaWorkshop.Models.Instruments.Contracts;
using SantaWorkshop.Utilities.Messages;
using System;
using System.Collections.Generic;

namespace SantaWorkshop.Models.Dwarfs
{
    public abstract class Dwarf : IDwarf
    {
        private string _name;
        private int _energy;
        protected Dwarf(string name, int energy)
        {
            this.Name = name;
            this.Energy = energy;
            this.Instruments = new List<IInstrument>();
        }
        public string Name 
        {
            get => this._name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidDwarfName);
                }

                this._name = value;
            }
        }
        public int Energy 
        {
            get => this._energy;
            protected set
            {
                if (value < 0)
                {
                    value = 0;
                }

                this._energy = value;
            }
        }

        public ICollection<IInstrument> Instruments { get; }

        public virtual void Work()
        {
            this.Energy -= 10;
        }

        public void AddInstrument(IInstrument instrument)
        {
            this.Instruments.Add(instrument);
        }
    }
}