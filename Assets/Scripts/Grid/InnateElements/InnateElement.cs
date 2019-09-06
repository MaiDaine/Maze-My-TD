using UnityEngine;

namespace MazeMyTD
{
    public class InnateElement : MonoBehaviour
    {
        public Tile occupiedTile;

        private void Start()
        {
            SetInvalidStatusOnTile();
        }

        public void SetInvalidStatusOnTile()
        {
            occupiedTile.tileState = Tile.TileState.Invalid;
        }
    }
}