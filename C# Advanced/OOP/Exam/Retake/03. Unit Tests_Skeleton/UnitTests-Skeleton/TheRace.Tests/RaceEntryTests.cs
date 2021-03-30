using System;
using NUnit.Framework;
using TheRace;

namespace TheRace.Tests
{
    public class RaceEntryTests
    {
        private static readonly string ExistingDriver = $"Driver {testName} is already added.";
        private const string DriverInvalid = "Driver cannot be null.";
        private static readonly string DriverAdded = $"Driver {testName} added in race.";
        private const string RaceInvalid = "The race cannot start with less than 2 participants.";
        private const string testModel = "model";
        private const int testHorsePower = 120;
        private const double testCubicCentimeters = 55;
        private const string testName = "Koko";
        private UnitCar testCar;
        private UnitDriver testDriver;

        private RaceEntry testRaceEntry;

        [SetUp]
        public void Setup()
        {
            testCar = new UnitCar(testModel, testHorsePower, testCubicCentimeters);
            testDriver = new UnitDriver(testName, testCar);
            testRaceEntry = new RaceEntry();
        }

        [Test]
        public void ConstructorShouldCreateObject()
        {
            Assert.AreEqual(0, testRaceEntry.Counter);
        }

        [Test]
        public void CountShouldGiveCorrectCountWhenDriverAdded()
        {
            var expectedCount = 1;

            testRaceEntry.AddDriver(testDriver);

            Assert.AreEqual(expectedCount, testRaceEntry.Counter);
        }
        [Test]
        public void CountShouldGiveCorrectResultWhenDriverAdded()
        {
            var expectedResult = DriverAdded;
            
            Assert.AreEqual(expectedResult, testRaceEntry.AddDriver(testDriver));
        }

        [Test]
        public void AddShouldThrowExceptionWithNullValue()
        {
            Assert.Throws< InvalidOperationException> (() => testRaceEntry.AddDriver(null), DriverInvalid);
        }

        [Test]
        public void AddMethodShouldThrowExceptionWithExistingComputer()
        {
            testRaceEntry.AddDriver(testDriver);

            Assert.Throws<InvalidOperationException>(() => testRaceEntry.AddDriver(testDriver), ExistingDriver);
        }

        [Test]
        public void CalculateAverageHorsePowerMethodShouldThrowExceptionWithInvalidCount()
        {
            testRaceEntry.AddDriver(testDriver);

            Assert.Throws<InvalidOperationException>(() => testRaceEntry.CalculateAverageHorsePower(), RaceInvalid);
        }

        [Test]
        public void CalculateAverageHorsePowerMethodShouldGiveCorrectResult()
        {
            var driverOne = new UnitDriver("nameOne", testCar);
            var driverTwo = new UnitDriver("nameTwo", testCar);
            testRaceEntry.AddDriver(testDriver);
            testRaceEntry.AddDriver(driverOne);
            testRaceEntry.AddDriver(driverTwo);

            Assert.AreEqual(testHorsePower, testRaceEntry.CalculateAverageHorsePower());
        }
    }
}