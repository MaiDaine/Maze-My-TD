using UnityEngine;

namespace MazeMyTD
{
    public class RayCastHelper : MonoBehaviour
    {
        [SerializeField]
        private int gridObjectLayer = 9;

        private Camera mainCamera;

        private void Start()
        {
            mainCamera = GetComponent<Camera>();
        }

        public Tile GetNearestTileFromCursor()
        {
            Tile tile = null;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, (1 << gridObjectLayer)))
                tile = hit.collider.gameObject.GetComponentInParent<Tile>();

            return tile;
        }
    }
}