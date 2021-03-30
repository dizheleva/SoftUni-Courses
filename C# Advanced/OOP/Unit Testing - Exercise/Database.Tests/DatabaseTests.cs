using NUnit.Framework;
using System;
namespace Tests
{
    [TestFixture]
    public class DatabaseTest
    {
        private Database database;
        private readonly int[] initialData = new int[] { 1, 2 };
        const int TestElement = 7;

        [SetUp]
        public void Setup()
        {
            database = new Database(initialData);
        }
        [Test]
        public void EmptyConstructorShouldReturnCountZero()
        {
            var emptyDatabase = new Database();

            var expectedCount = 0;

            Assert.That(emptyDatabase.Count, Is.EqualTo(expectedCount));
        }
        [Test]
        public void ConstructorShouldReturnCount()
        {
            var expectedCount = 2;

            Assert.That(database.Count, Is.EqualTo(expectedCount));
        }

        [Test]
        public void ConstructorShouldThrowException()
        {
            Assert.Throws<InvalidOperationException>(() => new Database(new int[17]));
        }

        [Test]
        public void AddMethodShouldThrowException()
        {
            

            for (var i = database.Count; i < 16; i++)
            {
                database.Add(TestElement);
            }

            Assert.Throws<InvalidOperationException>(() => database.Add(TestElement));
        }

        [Test]
        public void AddMethodShouldAddElement()
        {
            database.Add(TestElement);

            Assert.AreEqual(3, database.Count);
        }

        [Test]
        public void RemoveMethodShouldThrowException()
        {
            for (var i = database.Count - 1; i >= 0; i--)
            {
                database.Remove();
            }

            Assert.Throws<InvalidOperationException>(() => database.Remove());
        }

        [Test]
        public void RemoveMethodShouldRemoveLastItem()
        {
            database.Remove();
            CollectionAssert.AreEqual(new int[1] { 1 }, database.Fetch());
        }

        [Test]
        public void FetchMethodShouldReturnArray()
        {
            CollectionAssert.AreEqual(initialData, database.Fetch());
        }
    }
}