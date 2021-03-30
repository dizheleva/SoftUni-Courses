using System;
using EasterRaces.Models.Cars.Contracts;
using EasterRaces.Utilities.Messages;

namespace EasterRaces.Models.Cars.Entities
{
    public abstract class Car : ICar
    {
        private string model;
        private int horsePower;
        private int minHorsePower;
        private int maxHorsePower;

        protected Car(string model, int horsePower, double cubicCentimeters, int minHorsePower, int maxHorsePower)
        {
            this.Model = model;
            this.HorsePower = horsePower;
            this.CubicCentimeters = cubicCentimeters;
            this.minHorsePower = minHorsePower;
            this.maxHorsePower = maxHorsePower;
        }
        public string Model 
        {
            get => this.model;
            private set
            {
                var minLength = 4;
                if (string.IsNullOrWhiteSpace(value) || value.Length<minLength)
                {
                    var message = string.Format(ExceptionMessages.InvalidModel, value, minLength);
                    throw new ArgumentException(message);
                }

                this.model = value;
            }
        }
        public virtual int HorsePower 
        {
            get => this.horsePower;
            private set
            {
                if (value < this.minHorsePower && value > this.maxHorsePower)
                {
                    var message = string.Format(ExceptionMessages.InvalidHorsePower, value);
                    throw new ArgumentException(message);
                }

                this.horsePower = value;
            }
        }
        public double CubicCentimeters { get; }
        public double CalculateRacePoints(int laps)
        {
            var result = this.CubicCentimeters / this.HorsePower * laps;
            return result;
        }
    }
}