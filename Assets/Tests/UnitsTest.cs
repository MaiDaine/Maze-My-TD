using System.Collections;
using System.Collections.Generic;
using MazeMyTD;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TestTools;

namespace Tests
{
    public class UnitsTest
    {
        public GameObject CreateDefaultEnemy()
        {
            return MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Units/DefaultEnemy/DefaultEnemy"));
        }

        public GameObject CreateDefaultEnemyWithStats(UnitStats stats)
        {
            GameObject unit = CreateDefaultEnemy();
            unit.GetComponent<Unit>().Initialize(stats);
            return unit.gameObject;
        }

        public UnitStats CreateUnitStats()
        {
            return ScriptableObject.CreateInstance("UnitStats") as UnitStats;
        }

        public UnitStats CreateUnitStats(int health, int coreDmg, float moveSpeed)
        {
            UnitStats unitStats = ScriptableObject.CreateInstance("UnitStats") as UnitStats;
            unitStats.health = health;
            unitStats.coreDmg = coreDmg;
            unitStats.moveSpeed = moveSpeed;
            return unitStats;
        }

        //General
        [Test]
        public void DefaultEnemyIsSuccesfullyCreated()
        {
            GameObject defaultEnemy = CreateDefaultEnemy();
            Assert.IsNotNull(defaultEnemy);
            Assert.IsNotNull(defaultEnemy.GetComponent<Unit>());
        }

        [Test]
        public void UnitStatsIsSuccesfullyCreated()
        {
            UnitStats stats = CreateUnitStats();
            Assert.IsNotNull(stats);
        }

        //UnitHealth
        [Test]
        public void UnitHealthSucessfullyInitialize()
        {
            int maxHealth = Random.Range(2, 100);
            GameObject unit = CreateDefaultEnemyWithStats(CreateUnitStats(maxHealth, 1, 1.5f));
            UnitHealth unitHealth = unit.GetComponent<UnitHealth>();

            Assert.AreEqual(maxHealth, unitHealth.currentHealth);
            Assert.AreEqual(unitHealth.alive, true);
        }

        [Test]
        public void UnitSuccesfullyTakesDamage()
        {
            int maxHealth = Random.Range(2, 10);
            GameObject unit = CreateDefaultEnemyWithStats(CreateUnitStats(maxHealth, 1, 1.5f));
            UnitHealth unitHealth = unit.GetComponent<UnitHealth>();

            unitHealth.TakeDamage(1);
            Assert.GreaterOrEqual(unitHealth.currentHealth, 1);

            unitHealth.TakeDamage(maxHealth);
            Assert.Less(unitHealth.currentHealth, 0);
            Assert.AreEqual(unitHealth.alive, false);
        }

        [Test]
        public void InvalidValueForDamage()
        {
            int maxHealth = Random.Range(1, 100);
            GameObject unit = CreateDefaultEnemyWithStats(CreateUnitStats(maxHealth, 1, 1.5f));
            UnitHealth unitHealth = unit.GetComponent<UnitHealth>();

            unitHealth.TakeDamage(0);
            Assert.AreEqual(unitHealth.currentHealth, maxHealth);

            unitHealth.TakeDamage((int)Random.Range(-1000, 0));
            Assert.AreEqual(unitHealth.currentHealth, maxHealth);
        }

        [Test]
        public void UnitKill()
        {
            int maxHealth = Random.Range(1, 100);
            GameObject unit = CreateDefaultEnemyWithStats(CreateUnitStats(maxHealth, 1, 1.5f));
            UnitHealth unitHealth = unit.GetComponent<UnitHealth>();

            bool status = unitHealth.TakeDamage((int)Random.Range(maxHealth, 1000));
            Assert.LessOrEqual(unitHealth.currentHealth, 0);
            Assert.AreEqual(status, true);
            Assert.AreEqual(unitHealth.alive, false);
        }

        //UnitMovement
        [Test]
        public void UnitMovementSucessfullyInitialize()
        {
            float speed = Random.Range(1f, 100f);
            GameObject unit = CreateDefaultEnemyWithStats(CreateUnitStats(100, 1, speed));

            Assert.AreEqual(speed, unit.GetComponent<UnitMovement>().currentSpeed);
            Assert.AreEqual(speed, unit.GetComponent<NavMeshAgent>().speed);
        }

        //UnitDamage
        [Test]
        public void UnitDamageSucessfullyInitialize()
        {
            int damage = Random.Range(1, 100);
            GameObject unit = CreateDefaultEnemyWithStats(CreateUnitStats(100, damage, 1.5f));

            Assert.AreEqual(damage, unit.GetComponent<UnitDamage>().coreDamage);
        }
    }
}
