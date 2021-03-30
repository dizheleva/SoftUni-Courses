using System;

namespace DefiningClasses3
{
    class Car
    {
        public string Model { get; set; }
        public double FuelAmount { get; set; }
        public double FuelConsumptionPerKilometer { get; set; }
        public double TravelledDistance { get; set; }

        public void Drive(double distance)
        {
            var fuelIsEnough = distance * this.FuelConsumptionPerKilometer <= FuelAmount;
            if (fuelIsEnough)
            {
                this.FuelAmount -= distance * this.FuelConsumptionPerKilometer;
                this.TravelledDistance += distance;
            }
            else
            {
                Console.WriteLine("Insufficient fuel for the drive");
            }
        }
    }
}
