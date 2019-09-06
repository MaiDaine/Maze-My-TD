using UnityEngine.AI;
using UnityEngine;

namespace MazeMyTD
{
    [RequireComponent(typeof(PathRenderer))]
    public class GameRules : MonoBehaviour
    {
        public EnemySpawner enemySpawner;
        public Core core;

#pragma warning disable 0649 //Field "" is never assigned to, and will always have its default value null
        [SerializeField]
        private PlayerData playerData;
        [SerializeField]
        private GameEvent OnGameOver;
        [SerializeField]
        private GameEvent OnPlayerHealthChange;
#pragma warning restore 0649

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                CreateWavePath();
                enemySpawner.SpawnCreep();
            }
        }

        public void CreateWavePath()
        {
            NavMeshPath path = new NavMeshPath();

            NavMesh.CalculatePath(enemySpawner.spawnPoint.position, core.creepTarget.position, NavMesh.AllAreas, path);
            GetComponent<PathRenderer>().UpdateRenderedPath(path, enemySpawner.transform.position);
            enemySpawner.currentCreepPath = path;
        }

        public void CoreDamage(int damage)
        {
            playerData.health -= damage;
            if (playerData.health <= 0)
                OnGameOver.Raise();
            OnPlayerHealthChange.Raise();
        }
    }
}