using System;
using NUnit.Framework;
using FightingArenaDemo;

namespace Tests
{
    public class WarriorTests
    {
        private const string testName = "Koko";
        private const int testDamage = 100;
        private const int testHp = 50;
        private const string attackedTestName = "Shmoko";
        private const int attackedTestDamage = 40;
        private const int attackedTestHp = 40;
        private Warrior testWarrior;
        private Warrior attackedTestWarrior;

        [SetUp]
        public void Setup()
        {
            testWarrior = new Warrior(testName, testDamage, testHp);
            attackedTestWarrior = new Warrior(attackedTestName, attackedTestDamage, attackedTestHp);
        }

        [Test]
        public void ConstructorShouldCreateAWarriorWithCorrectParameters()
        {
            Assert.AreEqual(testName, testWarrior.Name);
            Assert.AreEqual(testDamage, testWarrior.Damage);
            Assert.AreEqual(testHp, testWarrior.HP);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void NamePropertyShouldThrowExceptionWithNullOrEmptyOrWhitespace(string incorrectName)
        {
            Assert.Throws<ArgumentException>(() => new Warrior(incorrectName, testDamage, testHp));
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void DamagePropertyShouldThrowExceptionWithZeroOrNegativeValue(int incorrectDamage)
        {
            Assert.Throws<ArgumentException>(() => new Warrior(testName, incorrectDamage, testHp));
        }

        [Test]
        public void HpPropertyShouldThrowExceptionWithNegativeValue()
        {
            Assert.Throws<ArgumentException>(() => new Warrior(testName, testDamage, -1));
        }

        [TestCase(10)]
        [TestCase(30)]
        public void AttackMethodShouldThrowExceptionIfAttackerHpIsLessThanNeeded(int lowHp)
        {
            Assert.Throws<InvalidOperationException>(() => new Warrior(testName,testDamage, lowHp).Attack(attackedTestWarrior));
        }

        [Test]
        public void AttackMethodShouldThrowExceptionIfAttackedHpIsLessThanNeeded()
        {
            var lowHp = 10;
            var weakAttackedWarrior = new Warrior(attackedTestName, attackedTestDamage, lowHp);
            Assert.Throws<InvalidOperationException>(() => testWarrior.Attack(weakAttackedWarrior));
        }

        [Test]
        public void AttackWhenThisDamageIsMoreFromAttackerHp()
        {
            testWarrior.Attack(attackedTestWarrior);

            Assert.AreEqual(10, testWarrior.HP);
            Assert.AreEqual(0, attackedTestWarrior.HP);
        }

        [Test]
        public void AttackWhenAttackerDamageIsMoreFromThisHp()
        {
            var strongerAttackedWarrior = new Warrior(attackedTestName, attackedTestDamage, 120);

            testWarrior.Attack(strongerAttackedWarrior);

            Assert.AreEqual(10, testWarrior.HP);
            Assert.AreEqual(20, strongerAttackedWarrior.HP);
        }
    }
}