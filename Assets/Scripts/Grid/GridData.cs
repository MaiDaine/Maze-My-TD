using UnityEngine;

namespace MazeMyTD
{
    [CreateAssetMenu(menuName = "SharedState/GridData")]
    public class GridData : ScriptableObject
    {
        public int height = 10;
        public int width = 10;
        public GameObject[] tiles;

        public GameObject tileRef;
        public float tileOffset = 1.1f;
    }
}