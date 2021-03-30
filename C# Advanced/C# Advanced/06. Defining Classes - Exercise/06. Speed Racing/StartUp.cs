using System;
using System.Collections.Generic;
using System.Linq;

namespace DefiningClasses3
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var cars = new HashSet<Car>();

            var count = int.Parse(Console.ReadLine());

            for (var i = 0; i < count; i++)
            {
                var carInfo = Console.ReadLine().Split();

                var model = carInfo[0];
                var fuelAmount = double.Parse(carInfo[1]);
                var fuelConsuptionForKM = double.Parse(carInfo[2]);

                cars.Add(new Car()
                    {
                        Model = model,
                        FuelAmount = fuelAmount,
                        FuelConsumptionPerKilometer = fuelConsuptionForKM
                    });
            }
            var input = string.Empty;

            while ((input = Console.ReadLine()) != "End")
            {
                var driveInfo = input.Split();

                var carModel = driveInfo[1];
                var amountOfKM = double.Parse(driveInfo[2]);

                foreach (var car in cars.Where(car => car.Model.Equals(carModel)))
                {
                    car.Drive(amountOfKM);
                }
            }

            foreach (var car in cars)
            {
                Console.WriteLine($"{car.Model} {car.FuelAmount:F2} {car.TravelledDistance:F0}");
            }
        }
    }
}
