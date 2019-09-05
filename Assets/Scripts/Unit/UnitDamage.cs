using UnityEngine;

namespace MazeMyTD
{
    [RequireComponent(typeof(SphereCollider))]
    public class UnitDamage : MonoBehaviour
    {
        [HideInInspector]
        public int coreDamage;

        public void Initialize(int coreDamage)
        {
            this.coreDamage = coreDamage;
        }

        private void OnTriggerEnter(Collider other)
        {
            Core core = other.GetComponent<Core>();
            if (core != null)
            {
            Debug.Log(other.name);
                core.TakeDamage(coreDamage);
                GetComponent<Unit>().OnDeath();
            }
        }
    }
}
