using UnityEngine;

namespace MazeMyTD
{
    [RequireComponent(typeof(Unit))]
    [RequireComponent(typeof(HealthBarHandler))]
    public class UnitHealth : MonoBehaviour
    {
        [HideInInspector]
        public bool alive = false;
        [HideInInspector]
        public int currentHealth;

#pragma warning disable 0649 //Field "" is never assigned to, and will always have its default value null
        [SerializeField]
        private HealthBarHandler healthBar;
#pragma warning restore 0649

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
            SetHealthBarFillAmount();
            return false;
        }

        private void SetHealthBarFillAmount()
        {
            healthBar.SetHealthFillAmount((float)currentHealth / (float)maxHealth);
        }
    }
}
