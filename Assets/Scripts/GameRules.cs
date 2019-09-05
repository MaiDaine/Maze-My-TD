using UnityEngine.AI;
using UnityEngine;

namespace MazeMyTD
{
    [RequireComponent(typeof(PathRenderer))]
    public class GameRules : MonoBehaviour
    {
        public CreepSpawn creepSpawn;
        public Core core;

        private NavMeshPath path;

        private void Awake()
        {
            path = new NavMeshPath();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
                CreateWavePath();
        }

        public void CreateWavePath()
        {
            NavMesh.CalculatePath(creepSpawn.spawnPoint.position, core.creepTarget.position, NavMesh.AllAreas, path);
            GetComponent<PathRenderer>().UpdateRenderedPath(path, creepSpawn.transform.position);
        }
    }
}
