using System;
using System.Collections.Generic;
using System.Linq;

namespace DefiningClasses
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var engines = new HashSet<Engine>();

            var cars = new HashSet<Car>();

            AddEngines(engines);
            AddCars(engines, cars);

            foreach (var car in cars)
            {
                Console.WriteLine(car.PrintCar());
            }
        }

        private static void AddCars(HashSet<Engine> engines, HashSet<Car> cars)
        {
            var n = int.Parse(Console.ReadLine());

            for (var i = 0; i < n; i++)
            {
                var carInfo = Console.ReadLine()
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries);

                var model = carInfo[0];
                var engineModel = carInfo[1];

                var engine = engines.FirstOrDefault(x=>x.Model==engineModel);

                var car = new Car(model, engine);

                if (carInfo.Length>2)
                {
                    if (int.TryParse(carInfo[2], out var weight))
                    {
                        car.Weight = weight;
                        if (carInfo.Length == 4)
                        {
                            var color = carInfo[3];
                            car.Color = color;
                        }
                    }
                    else
                    {
                        var color = carInfo[2];
                        car.Color = color;
                    }
                }

                cars.Add(car);
            }
        }

        private static void AddEngines(HashSet<Engine> engines)
        {
            var n = int.Parse(Console.ReadLine());

            for (var i = 0; i < n; i++)
            {
                var engineInfo = Console.ReadLine()
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries);

                var model = engineInfo[0];
                var power = int.Parse(engineInfo[1]);

                var engine = new Engine(model, power);

                if (engineInfo.Length > 2)
                {
                    if (int.TryParse(engineInfo[2], out var displacement))
                    {
                        engine.Displacement = displacement;
                        if (engineInfo.Length == 4)
                        {
                            var efficiency = engineInfo[3];
                            engine.Efficiency = efficiency;
                        }
                    }
                    else
                    {
                        var efficiency = engineInfo[2];
                        engine.Efficiency = efficiency;
                    }
                }

                engines.Add(engine);
            }
        }
    }
}
