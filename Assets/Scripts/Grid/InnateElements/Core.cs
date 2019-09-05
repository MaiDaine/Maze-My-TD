using UnityEngine;

namespace MazeMyTD
{
    public class Core : InnateElement
    {
        public Transform creepTarget;

#pragma warning disable 0649 //Field "" is never assigned to, and will always have its default value null
        [SerializeField]
        private GameRules gameRules;
#pragma warning restore 0649

        public void TakeDamage(int damage)
        {
            //TODO: Visual Effect
            gameRules.CoreDamage(damage);
        }
    }
}