using NUnit.Framework;

namespace Robots.Tests
{
    using System;

    public class RobotsTests
    {
        private const string testRobotName = "KokoRobot";
        private const int testRobotBattery = 100;
        
        private const string anotherName = "AnotherRobot";
        private const int anotherBattery = 50;

        private const string job = "someJob";
        private const int usage = 20;

        private Robot testRobot;
        private Robot anotherRobot;
        private RobotManager testRobotManager;

        [SetUp]
        public void Setup()
        {
            testRobot = new Robot(testRobotName, testRobotBattery);
            anotherRobot = new Robot(anotherName, anotherBattery);
            testRobotManager = new RobotManager(1);
        }

        [Test] //test 3,4
        public void ConstructorShouldCreateARobotManager()
        {
            Assert.That(testRobotManager.Count, Is.EqualTo(0));
            Assert.That(testRobotManager.Capacity, Is.EqualTo(1));
        }

        [Test] //test 5
        public void NegativeCapacityShouldThrowException()
        {
            var negativeTestCapacity = -10;

            Assert.Throws<ArgumentException>(() => testRobotManager = new RobotManager(negativeTestCapacity), "Invalid capacity!");
        }

        [Test] //test 4
        public void CountShouldGiveCorrectCountWhenRobotAdded()
        {
            testRobotManager.Add(testRobot);
            var expectedCount = 1;
        
            Assert.AreEqual(expectedCount, testRobotManager.Count);
        }

        [Test] // test 6!!!
        public void AddMethodShouldThrowExceptionWithExistingName()
        {
            var newRB = new RobotManager(2);
            newRB.Add(testRobot);

            Assert.Throws<InvalidOperationException>(() => newRB.Add(testRobot), $"There is already a robot with name {testRobotName}!");
        }

        [Test] // test 3
        public void AddMethodShouldThrowExceptionForReachingCapacity()
        {
            testRobotManager.Add(testRobot);

            Assert.Throws<InvalidOperationException>(() => testRobotManager.Add(anotherRobot), "Not enough capacity!");
        }

        [Test] //test 4
        public void RemoveMethodShouldRemoveRobot()
        {
            testRobotManager.Add(testRobot);
            testRobotManager.Remove(testRobot.Name);
        
            Assert.AreEqual(0, testRobotManager.Count);
        }

        [Test] // test 0
        public void WorkMethodShouldThrowExceptionWithInvalidName()
        {
            Assert.Throws<InvalidOperationException>(() => testRobotManager.Work(anotherName, job, usage), $"Robot with the name {anotherName} doesn't exist!");
        }

        [Test] // test 7
        public void RemoveMethodShouldThrowNullException()
        {
            Assert.Throws<InvalidOperationException>(() => testRobotManager.Remove(anotherName), $"Robot with the name {anotherName} doesn't exist!");
        }

        [Test] // test 8
        public void WorkMethodShouldReduceBattery()
        {
            var expectedBattery = testRobotBattery - usage;
            testRobotManager.Add(testRobot);

            testRobotManager.Work(testRobot.Name, job, usage);

            Assert.AreEqual(expectedBattery, testRobot.Battery);
        }

        [Test] // test 9
        public void WorkMethodShouldThrowExceptionWithInvalidUsage()
        {
            testRobotManager.Add(testRobot);
            var invalidUsage = 120;

            Assert.Throws<InvalidOperationException>(() => testRobotManager.Work(testRobotName, job, invalidUsage), $"{testRobotName} doesn't have enough battery!");
        }

        [Test] //test 0
        public void ChargeMethodShouldThrowExceptionWithInvalidName()
        {
            Assert.Throws<InvalidOperationException>(() => testRobotManager.Charge(anotherName), $"Robot with the name {anotherName} doesn't exist!");
        }

        [Test] //test 10
        public void ChargeMethodShouldChargeBattery()
        {
            testRobotManager.Add(testRobot);
            testRobotManager.Work(testRobot.Name, job, usage);

            testRobotManager.Charge(testRobotName);

            Assert.AreEqual(testRobotBattery, testRobot.Battery);
        }
    }
}
