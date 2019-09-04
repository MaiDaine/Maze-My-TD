using UnityEditor;
using UnityEngine;

namespace MazeMyTD
{
    public class GridManager : MonoBehaviour
    {
        public GridData gd;

        public void SpawnGrid()
        {
#if UNITY_EDITOR
            DestroyGrid();
            gd.tiles = new GameObject[gd.height * gd.width];
            for (int z = 0; z < gd.height; z++)
                for (int x = 0; x < gd.width; x++)
                {
                    GameObject tile = (GameObject)PrefabUtility.InstantiatePrefab(gd.tileRef as GameObject, this.transform);
                    gd.tiles[z * gd.width + x] = tile;
                    tile.transform.position = new Vector3(transform.position.x + gd.tileOffset * x, transform.position.y, transform.position.z + gd.tileOffset * z);
                    tile.GetComponent<Tile>().gridPos = new Vector2Int(x, z);
                }
#endif
        }

        public void UpdateTilePosition()
        {
            if (gd.tiles != null && gd.tiles.Length != 0)
                foreach (GameObject tileObj in gd.tiles)
                {
                    Tile tile = tileObj.GetComponent<Tile>();
                    tileObj.transform.position = new Vector3(transform.position.x + gd.tileOffset * tile.gridPos.x, transform.position.y, transform.position.z + gd.tileOffset * tile.gridPos.y);
                }
            else
            {
                Tile[] tiles = FindObjectsOfType<Tile>();
                if (tiles.Length == 0)
                {
                    Debug.Log("No tiles found, please re spawn the grid");
                    return;
                }

                gd.tiles = null;
                gd.tiles = new GameObject[tiles.Length];
                foreach (Tile tile in tiles)
                    gd.tiles[tile.gridPos.y * gd.width + tile.gridPos.x] = tile.gameObject;
            }
        }

        public void DestroyGrid()
        {
            if (gd.tiles != null && gd.tiles.Length != 0)
                for (int z = 0; z < gd.height; z++)
                    for (int x = 0; x < gd.width; x++)
                        DestroyImmediate(gd.tiles[z * gd.width + x]);
            gd.tiles = null;
        }
    }
}