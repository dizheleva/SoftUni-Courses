namespace Vehicles
{
    public class Car : Vehicle
    {
        private const double DefaultAirConditionerConsumption = 0.9;
        private const double DefaultRefuelPercentage = 1;
        public Car(double quantity, double consumption, double tankCapacity) 
            : base(quantity, consumption, tankCapacity)
        {
        }
        public override double AirConditionerConsumption => DefaultAirConditionerConsumption;
        public override double RefuelPercentage => DefaultRefuelPercentage;
    }
}
