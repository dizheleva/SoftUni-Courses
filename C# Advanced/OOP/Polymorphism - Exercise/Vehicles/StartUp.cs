namespace Vehicles
{
    using System;
    public class StartUp
    {
        static void Main()
        {
            var carInput = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var car = new Car(double.Parse(carInput[1]), double.Parse(carInput[2]), double.Parse(carInput[3]));

            var truckInput = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var truck = new Truck(double.Parse(truckInput[1]), double.Parse(truckInput[2]), double.Parse(truckInput[3]));

            var busInput = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var bus = new Bus(double.Parse(busInput[1]), double.Parse(busInput[2]), double.Parse(busInput[3]));

            var n = int.Parse(Console.ReadLine());

            for (var i = 0; i < n; i++)
            {
                var commandLine = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var command = commandLine[0];
                var vehicle = commandLine[1];
                var parameter = double.Parse(commandLine[2]);
                try
                {
                    switch (command)
                    {
                        case "Drive":
                            switch (vehicle)
                            {
                                case "Car":
                                    Console.WriteLine(car.Drive(parameter));
                                    break;
                                case "Truck":
                                    Console.WriteLine(truck.Drive(parameter));
                                    break;
                                case "Bus":
                                    Console.WriteLine(bus.Drive(parameter));
                                    break;
                            }
                            break;
                        case "DriveEmpty":
                            bus.SwitchConditionerOff();
                            Console.WriteLine(bus.Drive(parameter));
                            bus.SwitchConditionerOn();
                            break;
                        case "Refuel":
                            switch (vehicle)
                            {
                                case "Car":
                                    car.Refuel(parameter);
                                    break;
                                case "Truck":
                                    truck.Refuel(parameter);
                                    break;
                                case "Bus":
                                    bus.Refuel(parameter);
                                    break;
                            }
                            break;
                    }
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                }
                
            }

            Console.WriteLine(car.Report());
            Console.WriteLine(truck.Report());
            Console.WriteLine(bus.Report());
        }
    }
}
