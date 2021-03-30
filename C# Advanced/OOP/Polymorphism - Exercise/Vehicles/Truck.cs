namespace Vehicles
{
    using System;
    public class Truck : Vehicle
    {
        private const double DefaultAirConditionerConsumption = 1.6;
        private const double DefaultRefuelPercentage = 0.95;
        public Truck(double quantity, double consumption, double tankCapacity) 
            : base(quantity, consumption, tankCapacity)
        {
        }
        public override double AirConditionerConsumption => DefaultAirConditionerConsumption;
        public override double RefuelPercentage => DefaultRefuelPercentage;
    }
}
