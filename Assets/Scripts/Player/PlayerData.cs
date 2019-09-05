using UnityEngine;

namespace MazeMyTD
{
    [CreateAssetMenu(menuName = "SharedState/PlayerData")]
    public class PlayerData : ScriptableObject
    {
        public ABuilding[] availableBuildings;
        public int health;
        public int ressources;
    }
}