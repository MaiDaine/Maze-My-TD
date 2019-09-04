using UnityEngine;

namespace MazeMyTD
{
    public class Tile : MonoBehaviour
    {
        public enum TileState { Invalid, Empty, Occupied};

        public TileState tileState = TileState.Empty;
        public Vector2Int gridPos;
        public Transform tileCenter;
    }
}
