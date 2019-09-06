using UnityEngine.AI;
using UnityEngine;
using System.Collections;

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

        private void Start()
        {
            StartCoroutine(LateStart());
        }

        //Have to wait for NavMeshObstacle to be registered
        private IEnumerator LateStart()
        {
            yield return new WaitForSeconds(1f);
            RefreshWavePath();
            //       enemySpawner.SpawnCreep();

        }

        public bool RefreshWavePath()
        {
            NavMeshPath path = new NavMeshPath();

            if (NavMesh.CalculatePath(enemySpawner.spawnPoint.position, core.creepTarget.position, NavMesh.AllAreas, path)
                && path.status == NavMeshPathStatus.PathComplete)
            {
                GetComponent<PathRenderer>().UpdateRenderedPath(path, enemySpawner.transform.position);
                enemySpawner.currentCreepPath = path;
                return true;
            }
            return false;
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