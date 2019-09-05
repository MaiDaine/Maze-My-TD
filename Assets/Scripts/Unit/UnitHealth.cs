using UnityEngine;

namespace MazeMyTD
{
    [RequireComponent(typeof(Unit))]
    public class UnitHealth : MonoBehaviour
    {
        [HideInInspector]
        public bool alive = false;
        [HideInInspector]
        public int currentHealth;

        private int maxHealth;

        public void Initialize(int maxHealth)
        {
            this.maxHealth = maxHealth;
            currentHealth = maxHealth;
            alive = true;
        }

        public bool TakeDamage(int damage)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                alive = false;
                GetComponent<Unit>().OnDeath();
                return true;
            }
            //TODO: SetHealthBar
            return false;
        }
    }
}
