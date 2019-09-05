using UnityEngine.AI;
using UnityEngine;

namespace MazeMyTD
{
    public class EnemySpawner : InnateElement
    {
        public Transform spawnPoint;
        public NavMeshPath currentCreepPath;

        public Unit creepRef;
        public UnitStats creepStats;

        public void Start()
        {
            NavMeshHit navMeshHit;
            if (NavMesh.FindClosestEdge(spawnPoint.position, out navMeshHit, NavMesh.AllAreas))
                spawnPoint.position = navMeshHit.position;
            else
                Debug.LogError("SpawnPoint is off the Grid");
        }

        //Tmp function, should only used for testing purpose
        public void SpawnCreep()
        {
            Unit unit = Instantiate(creepRef, spawnPoint.position, spawnPoint.rotation);
            unit.Initialize(creepStats);
            unit.GetComponent<UnitMovement>().SetPath(currentCreepPath, spawnPoint.position);
        }
    }
}