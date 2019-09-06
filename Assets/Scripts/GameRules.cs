using UnityEngine.AI;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

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
        private UIController UIController;
        [SerializeField]
        private PlayerData playerData;
        [SerializeField]
        private GameEvent OnGameOver;
        [SerializeField]
        private GameEvent OnPlayerHealthChange;
        [SerializeField]
        private GameEvent OnPlayerRessourceChange;
        [SerializeField]
        private MessageLog messageLog;
#pragma warning restore 0649

        private float waveTimer = delayBetweenWaves;
        private int waveLevel = 1;

        private void Start()
        {
            StartCoroutine(LateStart());
        }

        //Have to wait for NavMeshObstacles to be registered
        private IEnumerator LateStart()
        {
            yield return new WaitForSeconds(1f);
            RefreshWavePath();
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
                    messageLog.AddMessage("Level " + waveLevel.ToString() + " start");
                }
                else
                    UIController.SetWaveTimer((int)waveTimer);
            }
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
            {
                OnGameOver.Raise();
                Time.timeScale = 0f;
                messageLog.AddMessage("Gameover at level " + waveLevel.ToString());
                messageLog.AddMessage("Restarting at level 1");
                RestartLevel();
            }
            OnPlayerHealthChange.Raise();
        }

        public void OnWaveEnd()
        {
            waveStatus = false;
            waveTimer = delayBetweenWaves;
            int gain = ressourceGain * waveLevel;
            playerData.ressources += gain;
            messageLog.AddMessage("You gain: " + gain.ToString() + " ressources");
            OnPlayerRessourceChange.Raise();
            waveLevel++;
        }

        public void RestartLevel()
        {
            playerData.health = 20;
            playerData.ressources = 100;
            Time.timeScale = 1f;
            SceneManager.LoadScene(0);
        }
    }
}