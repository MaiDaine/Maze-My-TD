using UnityEngine.AI;
using UnityEngine;

namespace MazeMyTD
{
    [RequireComponent(typeof(PathRenderer))]
    public class GameRules : MonoBehaviour
    {
        public CreepSpawn creepSpawn;
        public Core core;

        public void CreateWavePath()
        {
            NavMeshPath path = new NavMeshPath();

            NavMesh.CalculatePath(creepSpawn.spawnPoint.position, core.creepTarget.position, NavMesh.AllAreas, path);
            GetComponent<PathRenderer>().UpdateRenderedPath(path, creepSpawn.transform.position);
            creepSpawn.currentCreepPath = path;
            creepSpawn.SpawnCreep();
        }
    }
}