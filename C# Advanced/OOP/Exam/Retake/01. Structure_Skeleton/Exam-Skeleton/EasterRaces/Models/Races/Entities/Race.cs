using EasterRaces.Models.Drivers.Contracts;
using EasterRaces.Models.Races.Contracts;
using EasterRaces.Utilities.Messages;
using System;
using System.Collections.Generic;

namespace EasterRaces.Models.Races.Entities
{
    public class Race : IRace
    {
        private string name;
        private int laps;
        private readonly List<IDriver> drivers;

        public Race(string name, int laps)
        {
            this.Name = name;
            this.Laps = laps;
            this.drivers=new List<IDriver>();
        }
        public string Name
        {
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
        public int Laps 
        {
            get => this.laps;
            private set
            {
                var minCount = 1;
                if (value < minCount)
                {
                    var message = string.Format(ExceptionMessages.InvalidNumberOfLaps, minCount);
                    throw new ArgumentException(message);
                }

                this.laps = value;
            }
        }

        public IReadOnlyCollection<IDriver> Drivers => this.drivers.AsReadOnly();
        public void AddDriver(IDriver driver)
        {
            if (driver==null)
            {
                var message = string.Format(ExceptionMessages.DriverInvalid);
                throw new ArgumentNullException(message);
            }

            if (!driver.CanParticipate)
            {
                var message = string.Format(ExceptionMessages.DriverNotParticipate, driver.Name);
                throw new ArgumentException(message);
            }

            if (this.drivers.Contains(driver))
            {
                var message = string.Format(ExceptionMessages.DriverAlreadyAdded, driver.Name, this.Name);
                throw new ArgumentNullException(message);
            }

            this.drivers.Add(driver);
        }
    }
}