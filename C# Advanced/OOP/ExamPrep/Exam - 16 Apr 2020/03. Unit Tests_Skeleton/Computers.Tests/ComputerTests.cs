using System;
using System.Collections.Generic;

namespace Computers.Tests
{
    using NUnit.Framework;

    public class ComputerTests
    {
        private const string testComputerName = "KokoPC";
        private const string testPartName = "Desktop";
        private const decimal testPartPrice = 100;
        private Part testPart;
        private const string anotherPartName = "Mouse";
        private  const decimal anotherPartPrice = 50;
        private Part anotherPart;
        private List<Part> testPartList;
        private Computer testComputer;

        [SetUp]
        public void Setup()
        {

            testPart = new Part(testPartName, testPartPrice);
            anotherPart = new Part(anotherPartName, anotherPartPrice);
            testPartList = new List<Part>();
            testPartList.Add(testPart);
            testPartList.Add(anotherPart);
            testComputer = new Computer(testComputerName);
            testComputer.AddPart(testPart);
            testComputer.AddPart(anotherPart);
        }

        [Test]
        public void ConstructorShouldCreateAComputer()
        {
            var newComputer = new Computer(testComputerName);

            Assert.AreEqual(testComputerName, newComputer.Name);
            Assert.AreEqual(0, newComputer.Parts.Count);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void NamePropertyShouldThrowExceptionWithNullOrEmptyOrWhitespace(string incorrectName)
        {
            Assert.Throws<ArgumentNullException>(() => new Computer(incorrectName));
        }

        [Test]
        public void AddPartMethodShouldAddParts()
        {
            Assert.AreEqual(2, testComputer.Parts.Count);
            CollectionAssert.AreEqual(testPartList, testComputer.Parts);
        }

        [Test]
        public void AddPartMethodShouldThrowExceptionWithNullValue()
        {
            Part nullPart = null;
            Assert.Throws<InvalidOperationException>(() => testComputer.AddPart(nullPart));
        }

        [Test]
        public void TotalPricePropertyShouldGiveCorrectSum()
        {
            var expectedPrice = testPartPrice + anotherPartPrice;

            Assert.AreEqual(expectedPrice, testComputer.TotalPrice);
        }

        [Test]
        public void RemovePartMethodShouldRemoveParts()
        {
            testPartList.Remove(anotherPart);

            testComputer.RemovePart(anotherPart);

            Assert.AreEqual(1, testComputer.Parts.Count);
            CollectionAssert.AreEqual(testPartList, testComputer.Parts);
        }

        [Test]
        public void GetPartPropertyShouldGiveCorrectPart()
        {
            Assert.AreEqual(testPart, testComputer.GetPart(testPartName));
        }
    }
}