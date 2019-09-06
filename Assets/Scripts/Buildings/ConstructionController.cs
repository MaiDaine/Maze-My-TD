using UnityEngine;

namespace MazeMyTD
{
    public class ConstructionController : MonoBehaviour
    {
        public enum ConstructionState { Off, Positioning, Edit };

        public static ConstructionController instance;

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(this);
        }

#pragma warning disable 0649 //Field "" is never assigned to, and will always have its default value null
        [SerializeField]
        private PlayerData playerData;
        [SerializeField]
        private Vector3 defaultSpawnPosition;
#pragma warning restore 0649

        private ConstructionState constructionState = ConstructionState.Off;
        private ABuilding selectedBuilding = null;
        private RayCastHelper rayCast;

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
                        PlaceBuilding();
                }
            }
        }

        public void SpawnBuilding(int index)
        {
            if (index >= playerData.availableBuildings.Length) //some buildings may not be available at start
                return;
            if (playerData.ressources < playerData.availableBuildings[index].ressourceCost)
            {
                Debug.Log("Not enough ressources");//TODO: ErrorMsg
                return;
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
        }

        public void PlaceBuilding()
        {
            playerData.ressources -= selectedBuilding.ressourceCost;
            selectedBuilding.Init();
            selectedBuilding = null;
            constructionState = ConstructionState.Off;
        }
    }
}