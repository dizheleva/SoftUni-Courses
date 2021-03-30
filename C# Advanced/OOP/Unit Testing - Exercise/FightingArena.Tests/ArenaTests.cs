using System;
using System.Collections.Generic;
using NUnit.Framework;
using FightingArenaDemo;

namespace Tests
{
    public class ArenaTests
    {
        private const string testAttackerName = "Koko";
        private const int testAttackerDamage = 100;
        private const int testAttackerHp = 50;
        private const string defenderTestName = "Shmoko";
        private const int defenderTestDamage = 40;
        private const int defenderTestHp = 40;
        private Warrior attackerTestWarrior;
        private Warrior defenderTestWarrior;
        private Arena testArena;

        [SetUp]
        public void Setup()
        {
            attackerTestWarrior = new Warrior(testAttackerName, testAttackerDamage, testAttackerHp);
            defenderTestWarrior = new Warrior(defenderTestName, defenderTestDamage, defenderTestHp);
            testArena = new Arena();
            testArena.Enroll(attackerTestWarrior);
        }

        [Test]
        public void ConstructorShouldCreateNewList()
        {
            var arena = new Arena();

            Assert.AreEqual(0, arena.Count);
        }

        [Test]
        public void EnrollMethodShouldThrowExceptionIfTryToAddWarriorWithSameName()
        {
            var duplicateNameWarrior = new Warrior(testAttackerName, 30, 60);

            Assert.Throws<InvalidOperationException>(() => testArena.Enroll(duplicateNameWarrior));
        }

        [Test]
        public void EnrollMethodShouldAddWarriors()
        {
            var expectedListWarriors = new List<Warrior>();
            expectedListWarriors.Add(attackerTestWarrior);

            Assert.AreEqual(1, testArena.Count);
            CollectionAssert.AreEqual(expectedListWarriors, testArena.Warriors);
        }

        [Test]
        public void FightMethodShouldThrowExceptionIfAttackerIsNotInArena()
        {
            Assert.Throws<InvalidOperationException>(()
                => testArena.Fight(defenderTestName, testAttackerName));
        }

        [Test]
        public void FightMethodShouldThrowExceptionIfDefenderIsNotContainsInArena()
        {
            Assert.Throws<InvalidOperationException>(()
                => testArena.Fight(testAttackerName, defenderTestName));
        }
        

        [TestCase(5, 20, 20, 50)]
        [TestCase(10, 40, 41, 50)]

        public void AttackMethodShouldThrowExceptionIfHpIsLessThanMinAttackHp(
            int attackerDamage, int attackerHp, int defenderDamage, int defenderHp)
        {
            var warrior1 = new Warrior(testAttackerName, attackerDamage, attackerHp);
            var warrior2 = new Warrior(defenderTestName, defenderDamage, defenderHp);

            var arena = new Arena();
            arena.Enroll(warrior1);
            arena.Enroll(warrior2);

            Assert.Throws<InvalidOperationException>(()
                => arena.Fight(testAttackerName, defenderTestName));
        }

        [Test]
        public void AttackWhenThisDamageIsMoreFromAttackerHp()
        {
            var warrior1 = new Warrior(testAttackerName, 46, 50);
            var warrior2 = new Warrior(defenderTestName, 20, 45);

            var arena = new Arena();
            arena.Enroll(warrior1);
            arena.Enroll(warrior2);

            arena.Fight(testAttackerName, defenderTestName);

            Assert.AreEqual(30, warrior1.HP);
            Assert.AreEqual(0, warrior2.HP);
        }

        [Test]
        public void AttackWhenAttackerDamageIsMoreFromThisHp()
        {
            var warrior1 = new Warrior(testAttackerName, 30, 50);
            var warrior2 = new Warrior(defenderTestName, 20, 45);

            var arena = new Arena();
            arena.Enroll(warrior1);
            arena.Enroll(warrior2);

            arena.Fight(testAttackerName, defenderTestName);

            Assert.AreEqual(30, warrior1.HP);
            Assert.AreEqual(15, warrior2.HP);
        }
    }
}
