using System;
using EasterRaces.Models.Cars.Contracts;
using EasterRaces.Models.Drivers.Contracts;
using EasterRaces.Utilities.Messages;

namespace EasterRaces.Models.Drivers.Entities
{
    public class Driver : IDriver
    {
        private string name;

        public Driver(string name)
        {
            this.Name = name;
            this.Car = null;
            this.NumberOfWins = 0;
        }
        public string Name {
            get => this.name;
            private set
            {
                var minLength = 5;
                if (string.IsNullOrWhiteSpace(value) || value.Length < minLength)
                {
                    var message = string.Format(ExceptionMessages.InvalidName, value, minLength);
                    throw new ArgumentException(message);
                }

                this.name = value;
            }
        }

        public ICar Car { get; private set; }

        public int NumberOfWins { get; private set; }

        public bool CanParticipate => this.Car != null;
        public void WinRace()
        {
            this.NumberOfWins++;
        }

        public void AddCar(ICar car)
        {
            if (car==null)
            {
                var message = string.Format(ExceptionMessages.CarInvalid);
                throw new ArgumentNullException(message);
            }

            this.Car = car;
        }
    }
}