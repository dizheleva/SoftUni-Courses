namespace Aquariums.Tests
{
    using NUnit.Framework;
    using System;

    public class ComputerTests
    {
        private const string testSubjectName = "MyTestSubject";
        private Fish testSubject;

        private const string testObjectName = "MyTestObject";
        private const int testObjectCapacity = 2;

        private Aquarium testObject;

        [SetUp]
        public void Setup()
        {

            testSubject = new Fish(testSubjectName);
            testObject = new Aquarium(testObjectName, testObjectCapacity);
        }

        [Test]
        public void ConstructorShouldCreateObject()
        {
            Assert.AreEqual(0,testObject.Count);
            Assert.AreEqual(testObjectCapacity, testObject.Capacity);
            Assert.AreEqual(testObjectName, testObject.Name);
        }

        [TestCase(null)]
        [TestCase("")]
        public void NamePropertyShouldThrowExceptionWithNullOrEmpty(string incorrectName)
        {
            Assert.Throws<ArgumentNullException>(() => new Aquarium(incorrectName, testObjectCapacity), "Invalid aquarium name!");
        }

        [Test] 
        public void NegativeCapacityShouldThrowException()
        {
            var negativeTestCapacity = -10;

            Assert.Throws<ArgumentException>(() => new Aquarium(testObjectName, negativeTestCapacity), "Invalid aquarium capacity!");
        }

        [Test] 
        public void WhenAddMethodUsedCountShouldGiveCorrectValue()
        {
            var expectedValue = 1;

            testObject.Add(testSubject);

            Assert.AreEqual(expectedValue, testObject.Count);
        }

        [Test] 
        public void AddMethodShouldThrowExceptionForReachingCapacity()
        {
            for (int i = 0; i < testObjectCapacity; i++)
            {
                var fish = new Fish($"{i}");
                testObject.Add(fish);
            }

            Assert.Throws<InvalidOperationException>(() => testObject.Add(testSubject), "Aquarium is full!");
        }

        [Test]
        public void RemoveMethodShouldRemovePresent()
        {
            var expectedValue = 0;
            testObject.Add(testSubject);

            testObject.RemoveFish(testSubjectName);

            Assert.AreEqual(expectedValue, testObject.Count);
        }

        [Test] 
        public void RemoveMethodShouldThrowException()
        {
            Assert.Throws<InvalidOperationException>(() => testObject.RemoveFish(testSubjectName), $"Fish with the name {testSubjectName} doesn't exist!");
        }

        [Test]
        public void SellFishShouldGiveCorrectValue()
        {
            testObject.Add(testSubject);

            Assert.AreEqual(testSubject, testObject.SellFish(testSubjectName));
        }

        [Test]
        public void SellFishShouldChangeSubjectPropertyValue()
        {
            var expectedSubjectProperty = false;
            testObject.Add(testSubject);

            testObject.SellFish(testSubjectName);

            Assert.AreEqual(expectedSubjectProperty, testSubject.Available);
        }

        [Test]
        public void SellFishMethodShouldThrowException()
        {
            Assert.Throws<InvalidOperationException>(() => testObject.SellFish(testSubjectName), $"Fish with the name {testSubjectName} doesn't exist!");
        }

        [Test]
        public void ReportShouldGiveCorrectValue()
        {
            var expectedValue = $"Fish available at {testObjectName}: {testSubjectName}";

            testObject.Add(testSubject);

            Assert.AreEqual(expectedValue, testObject.Report());
        }
    }
}
