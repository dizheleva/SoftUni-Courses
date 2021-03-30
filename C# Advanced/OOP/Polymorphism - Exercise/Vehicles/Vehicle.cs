namespace Vehicles
{
    using System;
    public abstract class Vehicle : IVehicle
    {
        private const double DefaultFuelQuantity = 0.0;
        private double fuelQuantity;

        protected Vehicle(double fuelQuantity, double fuelConsumption, double tankCapacity, bool airConditionerIsOn = true)
        {
            this.TankCapacity = tankCapacity;
            this.FuelQuantity = fuelQuantity;
            this.AirConditionerIsOn = airConditionerIsOn;
            this.FuelConsumption = fuelConsumption;
        }
        public double FuelQuantity
        {
            get => this.fuelQuantity;
            protected set => this.fuelQuantity = value > this.TankCapacity ? DefaultFuelQuantity : value;
        }
        public double FuelConsumption { get; }
        public bool AirConditionerIsOn { get; private set; }
        public abstract double AirConditionerConsumption { get; }
        public double TankCapacity { get; }
        public abstract double RefuelPercentage { get; }

        public void SwitchConditionerOn()
        {
            this.AirConditionerIsOn = true;
        }
        public void SwitchConditionerOff()
        {
            this.AirConditionerIsOn = false;
        }
        public string Drive(double distance)
        {
            var totalFuelConsumption = this.AirConditionerIsOn ? this.FuelConsumption + this.AirConditionerConsumption : this.FuelConsumption;
            if (this.FuelQuantity - distance * totalFuelConsumption < 0)
            {
                throw new ArgumentException($"{this.GetType().Name} needs refueling");
            }
            this.FuelQuantity -= distance * totalFuelConsumption;
            return $"{this.GetType().Name} travelled {distance} km";
        }

        public virtual void Refuel(double litters)
        {
            if (litters <= 0)
            {
                throw new ArgumentException("Fuel must be a positive number");
            }
            if (litters + this.FuelQuantity > this.TankCapacity)
            {
                throw new ArgumentException($"Cannot fit {litters} fuel in the tank");
            }
            this.FuelQuantity += litters * this.RefuelPercentage;
        }

        public string Report()
        {
            return $"{this.GetType().Name}: {this.FuelQuantity:f2}";
        }
    }
}
