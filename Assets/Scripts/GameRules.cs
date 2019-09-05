using UnityEngine.AI;
using UnityEngine;

namespace MazeMyTD
{
    [RequireComponent(typeof(PathRenderer))]
    public class GameRules : MonoBehaviour
    {
        public CreepSpawn creepSpawn;
        public Core core;

#pragma warning disable 0649 //Field "" is never assigned to, and will always have its default value null
        [SerializeField]
        private PlayerData playerData;
#pragma warning restore 0649

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

        public void CoreDamage(int damage)
        {
            playerData.health -= damage;
            if (playerData.health <= 0)
                Application.Quit();//TODO: GameOver
        }
    }
}