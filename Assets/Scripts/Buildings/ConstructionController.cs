using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace MazeMyTD
{
    public class ConstructionController : MonoBehaviour
    {
        public enum ConstructionState { Off, Positioning, Edit };

        public static ConstructionController instance;

#pragma warning disable 0649 //Field "" is never assigned to, and will always have its default value null
        [SerializeField]
        private PlayerData playerData;
        [SerializeField]
        private GameRules gameRules;
        [SerializeField]
        private GameEvent onPlayerRessourceChange;
        [SerializeField]
        private MessageLog messageLog;
#pragma warning restore 0649

        private ConstructionState constructionState = ConstructionState.Off;
        private ABuilding selectedBuilding = null;
        private RayCastHelper rayCast = null;
        private Vector3 defaultSpawnPosition;

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(this);
        }

        private void Start()
        {
            rayCast = Camera.main.GetComponent<RayCastHelper>();
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                SpawnBuilding(0);
            if (Input.GetKeyDown(KeyCode.Alpha2))
                SpawnBuilding(1);

            if (constructionState == ConstructionState.Positioning)
            {
                Tile tile = rayCast.GetNearestTileFromCursor();
                if (tile != null)
                {
                    selectedBuilding.Move(tile.tileCenter.transform.position);
                    if (Input.GetMouseButtonDown(0) && tile.tileState == Tile.TileState.Empty)
                        StartCoroutine(PlaceBuilding());
                }
            }
        }

        public bool CheckBuildingValidity(int index)
        {
            if (index >= playerData.availableBuildings.Length
                || playerData.availableBuildings[index] == null) //some buildings may not be available at start
                return false;
            return true;
        }

        public bool CheckRessourcesAvailability(int index)
        {
            if (playerData.ressources < playerData.availableBuildings[index].ressourceCost)
            {
                messageLog.AddMessage("Not enough ressources", true);
                return false;
            }
            return true;
        }

        public void SpawnBuilding(int index)
        {
            if (gameRules.waveStatus
                || !CheckBuildingValidity(index)
                || !CheckRessourcesAvailability(index))
                return;

            if (constructionState == ConstructionState.Positioning)
                Destroy(selectedBuilding);
            else
                constructionState = ConstructionState.Positioning;

            Tile tile = rayCast.GetNearestTileFromCursor();
            if (tile != null)
                defaultSpawnPosition = tile.tileCenter.position;

            selectedBuilding = Instantiate(playerData.availableBuildings[index]);
            selectedBuilding.Move(defaultSpawnPosition);
            return;
        }

        public IEnumerator PlaceBuilding()
        {
            selectedBuilding.GetComponent<NavMeshObstacle>().enabled = true;
            yield return null;
            if (gameRules.RefreshWavePath())
            {
                playerData.ressources -= selectedBuilding.ressourceCost;
                onPlayerRessourceChange.Raise();
                selectedBuilding.Initialize();
                selectedBuilding = null;
                constructionState = ConstructionState.Off;
            }
            else
            {
                selectedBuilding.GetComponent<NavMeshObstacle>().enabled = false;
                messageLog.AddMessage("Do not block the path", true);
            }
        }

        public void SetPlayerData(PlayerData playerData) { this.playerData = playerData; }
    }
}