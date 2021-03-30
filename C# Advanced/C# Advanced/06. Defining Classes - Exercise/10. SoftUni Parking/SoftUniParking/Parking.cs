using System.Collections.Generic;
using System.Linq;

namespace SoftUniParking
{
    public class Parking
    {
        private List<Car> cars;
        private int capacity;
        public Parking(int capacity)
        {
            this.capacity = capacity;
            this.cars = new List<Car>();
        }
        public int Count => this.cars.Count;
        public string AddCar(Car car)
        {
            if (this.cars.Any(c => c.RegistrationNumber == car.RegistrationNumber))
            {
                return "Car with that registration number, already exists!";
            }

            if (this.cars.Count == this.capacity)
            {
                return "Parking is full!";
            }

            this.cars.Add(car);
            return $"Successfully added new car {car.Make} {car.RegistrationNumber}";
        }
        public string RemoveCar(string registrationNumber)
        {
            if (this.cars.All(c => c.RegistrationNumber != registrationNumber))
            {
                return "Car with that registration number, doesn't exist!";
            }

            this.cars = this.cars.Where(c => c.RegistrationNumber != registrationNumber).ToList();

            return $"Successfully removed {registrationNumber}";
        }

        public Car GetCar(string registrationNumber)
        {
            Car car = this.cars.FirstOrDefault(c => c.RegistrationNumber == registrationNumber);
            return car;
        }

        public void RemoveSetOfRegistrationNumber(List<string> registrationNumbers)
        {
            foreach (var registrationNumber in registrationNumbers)
            {
                Car car = this.cars.FirstOrDefault(c => c.RegistrationNumber == registrationNumber);
                if (car != null)
                {
                    this.cars.Remove(car);
                }
            }
        }
    }
}
