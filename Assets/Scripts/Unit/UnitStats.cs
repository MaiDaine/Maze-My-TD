using UnityEngine;

namespace MazeMyTD
{
    [CreateAssetMenu]
    public class UnitStats : ScriptableObject
    {
        public int health = 0;
        public int coreDmg = 0;
        public float moveSpeed = 0;

        public void Assign(UnitStats other)
        {
            this.health = other.health;
            this.coreDmg = other.coreDmg;
            this.moveSpeed = other.moveSpeed;
        }
    }
}