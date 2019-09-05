using UnityEngine.AI;
using UnityEngine;

namespace MazeMyTD
{
    [RequireComponent(typeof(PathRenderer))]
    public class GameRules : MonoBehaviour
    {
        public CreepSpawn creepSpawn;
        public Core core;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                CreateWavePath();
                creepSpawn.SpawnCreep();
            }
        }

        public void CreateWavePath()
        {
            NavMeshPath path = new NavMeshPath();

            NavMesh.CalculatePath(creepSpawn.spawnPoint.position, core.creepTarget.position, NavMesh.AllAreas, path);
            GetComponent<PathRenderer>().UpdateRenderedPath(path, creepSpawn.transform.position);
            creepSpawn.currentCreepPath = path;
        }
    }
}