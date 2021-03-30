using System;
using System.Collections.Generic;
using System.Linq;

namespace DefiningClasses4
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var cars = new HashSet<Car>();

            var count = int.Parse(Console.ReadLine());

            for (var i = 0; i < count; i++)
            {
                var carInfo = Console.ReadLine().Split();

                var model = carInfo[0];
                var engineSpeed = int.Parse(carInfo[1]);
                var enginePower = int.Parse(carInfo[2]);
                var engine = new Engine(engineSpeed, enginePower);

                var cargoWeight = int.Parse(carInfo[3]);
                var cargoType = carInfo[4];
                var cargo = new Cargo(cargoWeight, cargoType);

                var tires = new Tire[4];

                for (var j = 5; j < carInfo.Length; j += 2)
                {
                    var tirePressure = double.Parse(carInfo[j]);
                    var tireAge = int.Parse(carInfo[j + 1]);
                    tires[(j - 5) / 2] = new Tire(tirePressure, tireAge);
                }

                var car = new Car(model, engine, cargo, tires);

                cars.Add(car);
            }

            var command = Console.ReadLine();

            cars = command switch
            {
                "fragile" => cars.Where(x => x.Cargo.Type == "fragile" && x.Tires.Any(t => t.Pressure < 1)).ToHashSet(),
                "flamable" => cars.Where(x => x.Cargo.Type == "flamable" && x.Engine.Power > 250).ToHashSet(),
                _ => cars
            };

            foreach (var car in cars)
            {
                Console.WriteLine(car.Model);
            }
        }
    }
}
