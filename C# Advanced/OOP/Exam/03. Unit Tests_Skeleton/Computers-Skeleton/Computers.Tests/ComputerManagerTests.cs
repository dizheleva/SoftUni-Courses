using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Computers.Tests
{
    public class Tests
    {
        private const string CanNotBeNullMessage = "Can not be null!";
        private const string testManufacturer = "manufacturer";
        private const string testModel = "model";
        private const decimal testPrice = 100;
        private Computer testComputer;

        private ComputerManager testComputerManager;

        [SetUp]
        public void Setup()
        {

            testComputer = new Computer(testManufacturer, testModel, testPrice);
            testComputerManager = new ComputerManager();
        }

        [Test]
        public void ConstructorShouldCreateObject()
        {
            Assert.AreEqual(0, testComputerManager.Count);
        }

        [Test] 
        public void CountShouldGiveCorrectCountWhenCountAdded()
        {
            var expectedCount = 1;

            testComputerManager.AddComputer(testComputer);

            Assert.AreEqual(expectedCount, testComputerManager.Count);
        }

        [Test]
        public void AddShouldThrowExceptionWithNullValue()
        {
            Assert.Throws<ArgumentNullException>(() => testComputerManager.AddComputer(null), CanNotBeNullMessage);
        }

        [Test] 
        public void AddMethodShouldThrowExceptionWithExistingComputer()
        {
            testComputerManager.AddComputer(testComputer);

            Assert.Throws<ArgumentException>(() => testComputerManager.AddComputer(testComputer), "This computer already exists.");
        }

        [Test] 
        public void RemoveMethodShouldDecreaseCount()
        {
            testComputerManager.AddComputer(testComputer);
            testComputerManager.RemoveComputer(testManufacturer, testModel);

            Assert.AreEqual(0, testComputerManager.Count);
        }

        [Test]
        public void RemoveMethodShouldRemoveComputer()
        {
            testComputerManager.AddComputer(testComputer);

            Assert.AreEqual(testComputer, testComputerManager.RemoveComputer(testManufacturer, testModel));
        }

        [Test] 
        public void RemoveMethodShouldThrowNullException()
        {
            testComputerManager.AddComputer(testComputer);

            Assert.Throws<ArgumentNullException>(() => testComputerManager.RemoveComputer(null, testModel), CanNotBeNullMessage);
        }
        [Test]
        public void RemoveMethodShouldThrowNullExceptionWithModel()
        {
            testComputerManager.AddComputer(testComputer);

            Assert.Throws<ArgumentNullException>(() => testComputerManager.RemoveComputer(testManufacturer, null), CanNotBeNullMessage);
        }

        [Test] 
        public void GetComputerMethodShouldThrowExceptionWithNullManufacturer()
        {
            testComputerManager.AddComputer(testComputer);

            Assert.Throws<ArgumentNullException>(() => testComputerManager.GetComputer(null, testModel), CanNotBeNullMessage);
        }

        [Test]
        public void GetComputerMethodShouldThrowExceptionWithNullModel()
        {
            testComputerManager.AddComputer(testComputer);

            Assert.Throws<ArgumentNullException>(() => testComputerManager.GetComputer(testManufacturer, null), CanNotBeNullMessage);
        }

        [Test]
        public void GetComputerMethodShouldThrowExceptionWithInvalidComputer()
        {
            Assert.Throws<ArgumentException>(() => testComputerManager.GetComputer(testManufacturer, testModel), "There is no computer with this manufacturer and model.");
        }

        [Test]
        public void GetComputerMethodShouldGiveComputer()
        {
            testComputerManager.AddComputer(testComputer);

            Assert.AreEqual(testComputer, testComputerManager.GetComputer(testManufacturer, testModel));
        }

        [Test]
        public void GetComputersByManufacturerMethodShouldThrowExceptionWithNullManufacturer()
        {
            testComputerManager.AddComputer(testComputer);

            Assert.Throws<ArgumentNullException>(() => testComputerManager.GetComputersByManufacturer(null), CanNotBeNullMessage);
        }

        [Test]
        public void GetComputersByManufacturerShouldGiveCorrectComputersList()
        {
            var testComputerTwoSameManufacturer = new Computer(testManufacturer, "test2", testPrice);
            var testComputerThree = new Computer("Test3", "test3", testPrice);
            var testList = new List<Computer> { testComputer, testComputerTwoSameManufacturer};

            testComputerManager.AddComputer(testComputer);
            testComputerManager.AddComputer(testComputerTwoSameManufacturer);
            testComputerManager.AddComputer(testComputerThree);

            Assert.AreEqual(testList, testComputerManager.GetComputersByManufacturer(testManufacturer));
        }

        [Test]
        public void ComputersShouldGiveCorrectComputersList()
        {
            var testComputerTwo = new Computer("Test2", "test2", testPrice);
            var testComputerThree = new Computer("Test3", "test3", testPrice);
            var testList = new List<Computer> {testComputer, testComputerTwo, testComputerThree};

            testComputerManager.AddComputer(testComputer);
            testComputerManager.AddComputer(testComputerTwo);
            testComputerManager.AddComputer(testComputerThree);

            Assert.AreEqual(testList, testComputerManager.Computers);
        }
    }
}