using System;
using NUnit.Framework;
using CarManager;

namespace Tests
{
    public class CarTests
    {
        private const string testMake = "Skoda";
        private const string testModel = "Octavia";
        private const double testFuelConsumption = 5.0;
        private const double testFuelCapacity = 100.0;
        private Car testCar;

        [SetUp]
        public void Setup()
        {
            testCar = new Car(testMake, testModel, testFuelConsumption, testFuelCapacity);
        }
        [Test]
        public void TestConstructor()
        {
            Assert.AreEqual(testMake, testCar.Make);
            Assert.AreEqual(testModel, testCar.Model);
            Assert.AreEqual(testFuelConsumption, testCar.FuelConsumption);
            Assert.AreEqual(testFuelCapacity, testCar.FuelCapacity);
        }

        [TestCase(null, testModel, testFuelConsumption, testFuelCapacity)]
        [TestCase("", testModel, testFuelConsumption, testFuelCapacity)]
        [TestCase(testMake, null, testFuelConsumption, testFuelCapacity)]
        [TestCase(testMake, "", testFuelConsumption, testFuelCapacity)]
        [TestCase(testMake, testModel, 0, testFuelCapacity)]
        [TestCase(testMake, testModel, -1, testFuelCapacity)]
        [TestCase(testMake, testModel, testFuelConsumption, 0)]
        [TestCase(testMake, testModel, testFuelConsumption, -1)]
        public void TestAllMethodsForExceptions(
            string make, string model, double fuelConsumption, double fuelCapacity)
        {
            Assert.Throws<ArgumentException>(() =>
                new Car(make, model, fuelConsumption, fuelCapacity));
        }

        [Test]
        [TestCase(0)]
        [TestCase(-20)]
        public void TestRefuelMethodWithNegativeOrZero(double fuel)
        {
            Assert.Throws<ArgumentException>(() => testCar.Refuel(fuel));
        }

        [Test]
        public void TestRefuelMethodWithCorrectData()
        {
            testCar.Refuel(50);

            Assert.AreEqual(50, testCar.FuelAmount);
        }

        [Test]
        public void TestRefuelOverflow()
        {
            testCar.Refuel(120);

            Assert.AreEqual(testFuelCapacity, testCar.FuelAmount);
        }

        [Test]
        public void TestDriveMethodNormally()
        {
            var distance = 200.0;
            var expectedResult = 90;

            testCar.Refuel(testFuelCapacity);
            testCar.Drive(distance);

            Assert.AreEqual(expectedResult, testCar.FuelAmount);
        }

        [Test]
        public void TestTryDriveMoreThanFuelCapacity()
        {
            var veryLongDistance = 2000.0;

            Assert.Throws<InvalidOperationException>(() => testCar.Drive(veryLongDistance));
        }
    }
}