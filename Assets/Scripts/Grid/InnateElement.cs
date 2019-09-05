using UnityEngine;

namespace MazeMyTD
{
    public class InnateElement : MonoBehaviour
    {
        public Tile occupiedTile;

        public void SetInvalidStatusOnTile()
        {
            occupiedTile.tileState = Tile.TileState.Invalid;
        }
    }
}