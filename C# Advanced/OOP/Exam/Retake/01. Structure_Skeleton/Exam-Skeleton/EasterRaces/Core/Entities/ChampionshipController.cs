using EasterRaces.Core.Contracts;
using EasterRaces.Models.Cars.Contracts;
using EasterRaces.Models.Cars.Entities;
using EasterRaces.Models.Drivers.Entities;
using EasterRaces.Models.Races.Entities;
using EasterRaces.Repositories.Entities;
using EasterRaces.Utilities.Messages;
using System;
using System.Linq;
using System.Text;

namespace EasterRaces.Core.Entities
{
    public class ChampionshipController : IChampionshipController
    {
        private CarRepository carModels;
        private DriverRepository driverModels;
        private RaceRepository raceModels;
        public ChampionshipController()
        {
            this.carModels = new CarRepository();
            this.driverModels = new DriverRepository();
            this.raceModels = new RaceRepository();
        }
        public string CreateDriver(string driverName)
        {
            var driver = this.driverModels.GetByName(driverName);
            if (driver!=null)
            {
                var message = string.Format(ExceptionMessages.DriversExists, driverName);
                throw new ArgumentException(message);
            }

            driver = new Driver(driverName);
            this.driverModels.Add(driver);

            var result = string.Format(OutputMessages.DriverCreated, driverName);
            return result;
        }

        public string CreateCar(string type, string model, int horsePower)
        {
            var car = carModels.GetByName(model);
            if (car != null)
            {
                var message = string.Format(ExceptionMessages.CarExists, model);
                throw new ArgumentException(message);
            }

            car = type switch
            {
                "Muscle" => (ICar)new MuscleCar(model, horsePower),
                "Sports" => new SportsCar(model, horsePower),
                _ => throw new ArgumentException(ExceptionMessages.CarInvalid)
            };

            this.carModels.Add(car);

            var result = string.Format(OutputMessages.CarCreated, car.GetType().Name, model);
            return result;
        }

        public string CreateRace(string name, int laps)
        {
            var race = this.raceModels.GetByName(name);
            if (race != null)
            {
                var message = string.Format(ExceptionMessages.RaceExists, name);
                throw new InvalidOperationException(message);
            }

            race = new Race(name, laps);
            raceModels.Add(race);

            var result = string.Format(OutputMessages.RaceCreated, name);
            return result;
        }

        public string AddDriverToRace(string raceName, string driverName)
        {
            var race = this.raceModels.GetByName(raceName);
            if (race == null)
            {
                var message = string.Format(ExceptionMessages.RaceNotFound, raceName);
                throw new InvalidOperationException(message);
            }

            var driver = this.driverModels.GetByName(driverName);
            if (driver == null)
            {
                var message = string.Format(ExceptionMessages.DriverNotFound, driverName);
                throw new InvalidOperationException(message);
            }

            race.AddDriver(driver);

            var result = string.Format(OutputMessages.DriverAdded, driverName, raceName);
            return result;
        }

        public string AddCarToDriver(string driverName, string carModel)
        {
            var driver = this.driverModels.GetByName(driverName);
            if (driver == null)
            {
                var message = string.Format(ExceptionMessages.DriverNotFound, driverName);
                throw new InvalidOperationException(message);
            }

            var car = this.carModels.GetByName(carModel);
            if (car == null)
            {
                var message = string.Format(ExceptionMessages.CarNotFound, carModel);
                throw new InvalidOperationException(message);
            }

            driver.AddCar(car);

            var result = string.Format(OutputMessages.CarAdded, driverName, carModel);
            return result;
        }

        public string StartRace(string raceName)
        {
            var race = this.raceModels.GetByName(raceName);
            if (race == null)
            {
                var message = string.Format(ExceptionMessages.RaceNotFound, raceName);
                throw new InvalidOperationException(message);
            }

            var minParticipants = 3;
            if (race.Drivers.Count<minParticipants)
            {
                var message = string.Format(ExceptionMessages.RaceInvalid, raceName, minParticipants);
                throw new InvalidOperationException(message);
            }

            var winners = this.driverModels
                .GetAll()
                .OrderByDescending(p => p.Car.CalculateRacePoints(race.Laps))
                .Take(3)
                .ToList();

            this.raceModels.Remove(race);

            var result = new StringBuilder();
            result.AppendLine(String.Format(OutputMessages.DriverFirstPosition, winners.First().Name, raceName))
                .AppendLine(String.Format(OutputMessages.DriverSecondPosition, winners.Skip(1).First().Name, raceName))
                .AppendLine(String.Format(OutputMessages.DriverThirdPosition, winners.Last().Name, raceName));

            return result.ToString().TrimEnd();
        }
    }
}