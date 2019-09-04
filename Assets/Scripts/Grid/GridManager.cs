using UnityEditor;
using UnityEngine;

namespace MazeMyTD
{
    public class GridManager : MonoBehaviour
    {
#pragma warning disable 0649 //Field "" is never assigned to, and will always have its default value null
        [SerializeField]
        private GridData gridData;
        [SerializeField]
        private GameObject gridWireFrame;
#pragma warning restore 0649

        public void SpawnGrid()
        {
#if UNITY_EDITOR
            DestroyGrid();
            gridData.tiles = new GameObject[gridData.height * gridData.width];
            for (int z = 0; z < gridData.height; z++)
                for (int x = 0; x < gridData.width; x++)
                {
                    GameObject tile = (GameObject)PrefabUtility.InstantiatePrefab(gridData.tileRef as GameObject, this.transform);
                    gridData.tiles[z * gridData.width + x] = tile;
                    tile.transform.position = new Vector3(transform.position.x + gridData.tileOffset * x, transform.position.y, transform.position.z + gridData.tileOffset * z);
                    tile.GetComponent<Tile>().gridPos = new Vector2Int(x, z);
                }
            gridWireFrame.GetComponent<MeshRenderer>().sharedMaterial.SetVector("Tiling", new Vector4(gridData.width, gridData.height));
#endif
        }

        public void UpdateTilePosition()
        {
            if (gridData.tiles != null && gridData.tiles.Length != 0)
                foreach (GameObject tileObj in gridData.tiles)
                {
                    Tile tile = tileObj.GetComponent<Tile>();
                    tileObj.transform.position = new Vector3(transform.position.x + gridData.tileOffset * tile.gridPos.x, transform.position.y, transform.position.z + gridData.tileOffset * tile.gridPos.y);
                }
            else
            {
                Tile[] tiles = FindObjectsOfType<Tile>();
                if (tiles.Length == 0)
                {
                    Debug.Log("No tiles found, please re spawn the grid");
                    return;
                }

                gridData.tiles = null;
                gridData.tiles = new GameObject[tiles.Length];
                foreach (Tile tile in tiles)
                    gridData.tiles[tile.gridPos.y * gridData.width + tile.gridPos.x] = tile.gameObject;
            }
        }

        public void DestroyGrid()
        {
            if (gridData.tiles != null && gridData.tiles.Length != 0)
                for (int z = 0; z < gridData.height; z++)
                    for (int x = 0; x < gridData.width; x++)
                        DestroyImmediate(gridData.tiles[z * gridData.width + x]);
            gridData.tiles = null;
        }
    }
}