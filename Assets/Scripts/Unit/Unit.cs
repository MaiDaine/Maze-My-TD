using UnityEngine;

namespace MazeMyTD
{
    [RequireComponent(typeof(UnitMovement))]
    [RequireComponent(typeof(UnitDamage))]
    [RequireComponent(typeof(Rigidbody))]
    public class Unit : MonoBehaviour
    {
        [HideInInspector]
        public UnitStats currentStats;

        public virtual void Initialize(UnitStats stats)
        {
            currentStats = ScriptableObject.CreateInstance("UnitStats") as UnitStats;
            currentStats.Assign(stats);
            GetComponent<UnitMovement>().Initialize(currentStats.moveSpeed);
            GetComponent<UnitDamage>().Initialize(currentStats.coreDmg);
        }
    }
}