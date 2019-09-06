using UnityEngine.AI;
using UnityEngine;

namespace MazeMyTD
{
    public class EnemySpawner : InnateElement
    {
        public Transform spawnPoint;
        public NavMeshPath currentCreepPath;

        public Unit unitRef;
        public UnitStats unitStatsRef;

        private const float delayBetweenUnits = 1f;

#pragma warning disable 0649 //Field "" is never assigned to, and will always have its default value null
        [SerializeField]
        private GameEvent OnWaveEnd;
#pragma warning restore 0649

        private int activeUnitCount = 0;
        private UnitStats currentStats;
        private int unitStock = 0;
        private float spawnTimer = 0F;

        public void Start()
        {
            NavMeshHit navMeshHit;
            if (NavMesh.FindClosestEdge(spawnPoint.position, out navMeshHit, NavMesh.AllAreas))
                spawnPoint.position = navMeshHit.position;
            else
                Debug.LogError("SpawnPoint is off the Grid");
        }

        public void OnUnitDeath()
        {
            if (unitStock == 0 && activeUnitCount <= 0)
                OnWaveEnd.Raise();
        }

        public void StartWave(int level)
        {
            currentStats = ScriptableObject.CreateInstance("UnitStats") as UnitStats;
            currentStats.health = unitStatsRef.health + level;
            currentStats.moveSpeed = unitStatsRef.moveSpeed + level / 10f;
            currentStats.coreDmg = unitStatsRef.coreDmg + level / 2;
            unitStock = 5 + level / 3;
        }

        private void FixedUpdate()
        {
            if (unitStock > 0)
            {
                spawnTimer -= Time.deltaTime;
                if (spawnTimer <= 0f)
                {
                    SpawnCreep();
                    spawnTimer = delayBetweenUnits;
                }
            }
        }

        private void SpawnCreep()
        {
            Unit unit = Instantiate(unitRef, spawnPoint.position, spawnPoint.rotation);
            unit.Initialize(currentStats);
            unit.GetComponent<UnitMovement>().SetPath(currentCreepPath, spawnPoint.position);
            activeUnitCount++;
        }
    }
}