using SantaWorkshop.Core.Contracts;
using SantaWorkshop.Models.Dwarfs;
using SantaWorkshop.Models.Dwarfs.Contracts;
using SantaWorkshop.Models.Instruments;
using SantaWorkshop.Models.Presents;
using SantaWorkshop.Models.Workshops;
using SantaWorkshop.Repositories;
using SantaWorkshop.Utilities.Messages;
using System;
using System.Linq;
using System.Text;

namespace SantaWorkshop.Core
{
    public class Controller : IController
    {
        private readonly DwarfRepository _dwarfs;
        private readonly PresentRepository _presents;
        private readonly Workshop _workshop;

        public Controller()
        {
            this._dwarfs = new DwarfRepository();
            this._presents = new PresentRepository();
            this._workshop = new Workshop();
        }
        public string AddDwarf(string dwarfType, string dwarfName)
        {
            var dwarf = dwarfType switch
            {
                nameof(HappyDwarf) => (IDwarf)new HappyDwarf(dwarfName),
                nameof(SleepyDwarf) => new SleepyDwarf(dwarfName),
                _ => throw new InvalidOperationException(ExceptionMessages.InvalidDwarfType)
            };
            _dwarfs.Add(dwarf);

            return string.Format(OutputMessages.DwarfAdded, dwarfType, dwarfName);
        }

        public string AddInstrumentToDwarf(string dwarfName, int power)
        {
            var dwarf = _dwarfs.FindByName(dwarfName);
            if (dwarf==null)
            {
                throw  new InvalidOperationException(ExceptionMessages.InexistentDwarf);
            }
            var instrument = new Instrument(power);
            dwarf.AddInstrument(instrument);

            return string.Format(OutputMessages.InstrumentAdded, power, dwarfName);
        }

        public string AddPresent(string presentName, int energyRequired)
        {
            var present = new Present(presentName, energyRequired);
            _presents.Add(present);

            return string.Format(OutputMessages.PresentAdded, presentName);
        }

        public string CraftPresent(string presentName)
        {
            var suitableDwarfs = _dwarfs.Models.Where(d => d.Energy >= 50).ToList();

            if (!suitableDwarfs.Any())
            {
                throw new InvalidOperationException(ExceptionMessages.DwarfsNotReady);
            }

            foreach (var dwarf in suitableDwarfs.OrderByDescending(d=>d.Energy))
            {
                this._workshop.Craft(_presents.FindByName(presentName), dwarf);
                if (dwarf.Energy==0)
                {
                    _dwarfs.Remove(dwarf);
                }
            }

            return string.Format(
                _presents.FindByName(presentName).IsDone()
                    ? OutputMessages.PresentIsDone
                    : OutputMessages.PresentIsNotDone, presentName);
        }

        public string Report()
        {
            var craftedPresents = _presents.Models.Count(p => p.IsDone());

            var result = new StringBuilder();
            result.AppendLine($"{craftedPresents} presents are done!");
            result.AppendLine("Dwarfs info:");

            foreach (var dwarf in _dwarfs.Models)
            {
                var instrumentsCount = dwarf.Instruments.Count(i => i.IsBroken() == false);

                result.AppendLine($"Name: {dwarf.Name}"); 
                result.AppendLine($"Energy: {dwarf.Energy}"); 
                result.AppendLine($"Instruments: {instrumentsCount} not broken left"); 
            }

            return result.ToString().TrimEnd();
        }
    }
}