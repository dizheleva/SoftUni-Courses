namespace Vehicles
{
    public interface IVehicle
    {
        double FuelQuantity { get; }
        double FuelConsumption { get; }
        bool AirConditionerIsOn { get; }
        double AirConditionerConsumption { get; }
        public double RefuelPercentage { get; }
        double TankCapacity { get; }
        string Drive(double distance);
        void Refuel(double litters);
        void SwitchConditionerOn();
        void SwitchConditionerOff();
    }
}
