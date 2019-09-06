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
        private GameEvent OnPlayerRessourceChange;
#pragma warning restore 0649

        private ConstructionState constructionState = ConstructionState.Off;
        private ABuilding selectedBuilding = null;
        private RayCastHelper rayCast;
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
            if (Input.GetKeyDown(KeyCode.Alpha1))//TODO: Inputs
                SpawnBuilding(0);

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

        public bool SpawnBuilding(int index)
        {
            if (gameRules.waveStatus)
                return false;

            if (index >= playerData.availableBuildings.Length
                || playerData.availableBuildings[index] == null) //some buildings may not be available at start
                return false;

            if (playerData.ressources < playerData.availableBuildings[index].ressourceCost)
            {
                Debug.Log("Not enough ressources");//TODO: ErrorMsg
                return false;
            }

            if (constructionState == ConstructionState.Positioning)
                Destroy(selectedBuilding);
            else
                constructionState = ConstructionState.Positioning;

            Tile tile = rayCast.GetNearestTileFromCursor();
            if (tile != null)
                defaultSpawnPosition = tile.tileCenter.position;

            selectedBuilding = Instantiate(playerData.availableBuildings[index]);
            selectedBuilding.Move(defaultSpawnPosition);
            return true;
        }

        public IEnumerator PlaceBuilding()
        {
            selectedBuilding.GetComponent<NavMeshObstacle>().enabled = true;
            yield return null;
            if (gameRules.RefreshWavePath())
            {
                playerData.ressources -= selectedBuilding.ressourceCost;
                OnPlayerRessourceChange.Raise();
                selectedBuilding.Initialize();
                selectedBuilding = null;
                constructionState = ConstructionState.Off;
            }
            else
            {
                selectedBuilding.GetComponent<NavMeshObstacle>().enabled = false;
                Debug.Log("Do not block the path");//TODO: ErrorMsg
            }
        }

        public void SetPlayerData(PlayerData playerData) { this.playerData = playerData; }
    }
}