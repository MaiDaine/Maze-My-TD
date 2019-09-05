using UnityEngine;

namespace MazeMyTD
{
    [RequireComponent(typeof(UnitHealth))]
    [RequireComponent(typeof(UnitMovement))]
    [RequireComponent(typeof(UnitDamage))]
    [RequireComponent(typeof(Rigidbody))]
    public class Unit : MonoBehaviour
    {
        [HideInInspector]
        public UnitStats currentStats;

        public void Initialize(UnitStats stats)
        {
            currentStats = ScriptableObject.CreateInstance("UnitStats") as UnitStats;
            currentStats.Assign(stats);
            GetComponent<UnitHealth>().Initialize(currentStats.health);
            GetComponent<UnitMovement>().Initialize(currentStats.moveSpeed);
            GetComponent<UnitDamage>().Initialize(currentStats.coreDmg);
        }

        public void OnDeath()
        {
            //TODO DeathAnimation
            Destroy(gameObject);
        }
    }
}