namespace Presents.Tests
{
    using NUnit.Framework;
    using System;

    public class ComputerTests
    {
        private const string TestPresentName = "MyPresent";
        private const double TestMagic = 100;
        private Present _testPresent;
        private const string AnotherPresentName = "AnotherPresent";
        private const double AnotherMagic = 50;
        private Present _anotherPresent;
        private Bag _testBag;

        [SetUp]
        public void Setup()
        {

            _testPresent = new Present(TestPresentName, TestMagic);
            _anotherPresent = new Present(AnotherPresentName, AnotherMagic);
            _testBag = new Bag();
        }

        [Test]
        public void ConstructorShouldCreateABag()
        {
            Assert.AreEqual(0, _testBag.GetPresents().Count);
        }

        [Test]
        public void CreateMethodShouldAddPresent()
        {
            var result = $"Successfully added present {_testPresent.Name}.";

            Assert.AreEqual(result, _testBag.Create(_testPresent));
        }

        [Test]
        public void CreateMethodShouldThrowExceptionWithNullValue()
        {
            Present nullPresent = null;

            Assert.Throws<ArgumentNullException>(() => _testBag.Create(nullPresent), "Present is null");
        }

        [Test]
        public void CreateMethodShouldThrowExceptionWithExistingValue()
        {
            _testBag.Create(_testPresent);

            Assert.Throws<InvalidOperationException>(() => _testBag.Create(_testPresent), "This present already exists!");
        }

        [Test]
        public void RemoveMethodShouldRemovePresent()
        {
            _testBag.Create(_testPresent);
            var result = true;

            Assert.AreEqual(result, _testBag.Remove(_testPresent));
        }

        [Test]
        public void GetPresentWithLeastMagicMethodShouldGetCorrectPresent()
        {
            _testBag.Create(_testPresent);
            _testBag.Create(_anotherPresent);
            var result = _anotherPresent;

            Assert.AreEqual(result, _testBag.GetPresentWithLeastMagic());
        }

        [Test]
        public void GetPresentShouldGiveCorrectPresent()
        {
            _testBag.Create(_testPresent);

            Assert.AreEqual(_testPresent, _testBag.GetPresent(TestPresentName));
        }
    }
}