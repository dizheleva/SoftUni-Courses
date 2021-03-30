namespace Vehicles
{
    public class Bus : Vehicle
    {
        private const double DefaultAirConditionerConsumption = 1.4;
        private const double DefaultRefuelPercentage = 1;
        public Bus(double quantity, double consumption, double tankCapacity) 
            : base(quantity, consumption, tankCapacity)
        {
        }
        public override double AirConditionerConsumption => DefaultAirConditionerConsumption;
        public override double RefuelPercentage => DefaultRefuelPercentage;
    }
}
