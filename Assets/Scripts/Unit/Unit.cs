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

#pragma warning disable 0649 //Field "" is never assigned to, and will always have its default value null
        [SerializeField]
        private GameEvent onUnitDeath;
#pragma warning restore 0649


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
            onUnitDeath.Raise();
            Destroy(gameObject);
        }
    }
}