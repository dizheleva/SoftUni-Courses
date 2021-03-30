using System;
using NUnit.Framework;
using ExtendedDatabaseDemo;

namespace Tests
{
    public class ExtendedDatabaseTests
    {
        private ExtendedDatabase database;
        private string testUsername;
        private long testId;
        private Person testPersonOne;
        private Person testPersonTwo;
        private Person testPersonThree;
        private Person[] testPersonData;

        [SetUp]
        public void Setup()
        {
            testUsername = "testUserOne";
            testId = 123;
            testPersonOne = new Person(123, "testUserOne");
            testPersonTwo = new Person(456, "testUserTwo");
            testPersonThree = new Person(789, "testUserThree");
            testPersonData = new Person[] {testPersonOne, testPersonTwo};
            database = new ExtendedDatabase(testPersonData);
        }
        [Test]
        public void EmptyConstructorShouldReturnCountZero()
        {
            var emptyDatabase = new ExtendedDatabase();

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
        public void ConstructorShouldThrowExceptionForAddRange()
        {
            var newPersonData = new Person[17];
            for (var i = 0; i < 17; i++)
            {
                newPersonData[i] = new Person(i, $"{i}");
            }

            Assert.Throws<ArgumentException>(() => new ExtendedDatabase(newPersonData));
        }

        [Test]
        public void AddMethodShouldThrowExceptionIfMaxCount()
        {
            var newPersonData = new Person[16];
            for (var i = 0; i < 16; i++)
            {
                newPersonData[i]=new Person(i, $"{i}");
            }
            var newDatabase = new ExtendedDatabase(newPersonData);

            Assert.Throws<InvalidOperationException>(() => newDatabase.Add(testPersonThree));
        }
        [Test]
        public void AddMethodShouldThrowExceptionIfSameId()
        {
            var testPersonWithSameId = new Person(testId, "newTestPerson");

            Assert.Throws<InvalidOperationException>(() => database.Add(testPersonWithSameId));
        }
        [Test]
        public void AddMethodShouldThrowExceptionIfSameUsername()
        {
            var testPersonWithSameUsername = new Person(123456, testUsername);

            Assert.Throws<InvalidOperationException>(() => database.Add(testPersonWithSameUsername));
        }
        [Test]
        public void AddMethodShouldAddElement()
        {
            database.Add(testPersonThree);

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
            var resultPersonData = new Person[database.Count];

            for (var i = 0; i < database.Count; i++)
            {
                resultPersonData[i] = testPersonData[i];
            }
            CollectionAssert.AreEqual(new Person[] { testPersonOne }, resultPersonData);
        }
        [TestCase(null)]
        [TestCase("")]
        public void FindByUsernameMethodShouldThrowExceptionForNullParameter(string username)
        {
            Assert.Throws<ArgumentNullException>(()=> database.FindByUsername(username));
        }
        [Test]
        public void FindByUsernameMethodShouldThrowExceptionForWrongParameter()
        {
            var wrongTestUsername = "noSuchUsername";

            Assert.Throws<InvalidOperationException>(() => database.FindByUsername(wrongTestUsername));
        }
        [Test]
        public void FindByUsernameMethodShouldReturnPerson()
        {
            Assert.AreEqual(testPersonOne, database.FindByUsername(testUsername));
        }
        [Test]
        public void FindByIdMethodShouldThrowExceptionForNegativeParameter()
        {
            var negativeTestUsername = -10;

            Assert.Throws<ArgumentOutOfRangeException>(() => database.FindById(negativeTestUsername));
        }
        [Test]
        public void FindByIdMethodShouldThrowExceptionForWrongParameter()
        {
            var wrongTestId = 9876543;

            Assert.Throws<InvalidOperationException>(() => database.FindById(wrongTestId));
        }
        [Test]
        public void FindByIdMethodShouldReturnPerson()
        {
            Assert.AreEqual(testPersonOne, database.FindById(testId));
        }
        
    }
}