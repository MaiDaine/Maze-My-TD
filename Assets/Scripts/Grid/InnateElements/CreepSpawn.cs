using UnityEngine.AI;
using UnityEngine;

namespace MazeMyTD
{
    public class CreepSpawn : InnateElement
    {
        public Transform spawnPoint;
        public NavMeshPath currentCreepPath;

        public GameObject creepRef;

        public void Start()
        {
            NavMeshHit navMeshHit;
            if (NavMesh.FindClosestEdge(spawnPoint.position, out navMeshHit, NavMesh.AllAreas))
                spawnPoint.position = navMeshHit.position;
            else
                Debug.LogError("SpawnPoint is off the Grid");
        }

        //Tmp function use to test creep path
        public void SpawnCreep()
        {
            NavMeshAgent agent = Instantiate(creepRef, spawnPoint.position, spawnPoint.rotation).GetComponent<NavMeshAgent>();
            agent.Warp(spawnPoint.position);
            agent.enabled = true;
            agent.path = currentCreepPath;
        }
    }
}