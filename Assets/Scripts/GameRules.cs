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
        public bool waveStatus = false;

        private const float delayBetweenWaves = 60f;
        private const int ressourceGain = 50;

#pragma warning disable 0649 //Field "" is never assigned to, and will always have its default value null
        [SerializeField]
        private HUDController hUDController;
        [SerializeField]
        private PlayerData playerData;
        [SerializeField]
        private GameEvent OnGameOver;
        [SerializeField]
        private GameEvent OnPlayerHealthChange;
        [SerializeField]
        private GameEvent OnPlayerRessourceChange;

#pragma warning restore 0649

        private float waveTimer = delayBetweenWaves;
        private int waveLevel = 1;

        private void Start()
        {
            playerData.health = 25;//TODO REMOVE
            playerData.ressources = 100;
            StartCoroutine(LateStart());
        }

        //Have to wait for NavMeshObstacles to be registered
        private IEnumerator LateStart()
        {
            yield return new WaitForSeconds(1f);
            RefreshWavePath();
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

        public void OnWaveEnd()
        {
            waveStatus = false;
            waveTimer = delayBetweenWaves;
            playerData.ressources += ressourceGain * waveLevel;
            OnPlayerRessourceChange.Raise();
            waveLevel++;
        }

        private void FixedUpdate()
        {
            if (!waveStatus)
            {
                waveTimer -= Time.deltaTime;
                if (waveTimer <= 0)
                {
                    waveStatus = true;
                    enemySpawner.StartWave(waveLevel);
                }
                else
                    hUDController.SetWaveTimer((int)waveTimer);
            }
        }
    }
}