using System.Collections.Generic;
using UnityEngine;

namespace MazeMyTD
{
    [RequireComponent(typeof(SphereCollider))]
    public class Tower : ABuilding
    {
        public int atkDamage;
        public float atkCoolDown;
        public float atkRange;

        private float atkReloadTimer = 0f;
        private List<UnitHealth> targets = new List<UnitHealth>();

        private void Awake()
        {
            GetComponent<SphereCollider>().radius = atkRange;
        }

        public override void Initialize()
        {
            base.Initialize();
            GetComponent<SphereCollider>().enabled = true;
        }

        private void FixedUpdate()
        {
            if (atkReloadTimer <= 0f)
            {
                if (targets.Count > 0 && targets[0].alive && targets[0].TakeDamage(atkDamage))
                    targets.RemoveAt(0);
                atkReloadTimer = atkCoolDown;
            }
            atkReloadTimer -= Time.deltaTime;
        }

        private void OnTriggerEnter(Collider other)
        {
            UnitHealth target = other.GetComponent<UnitHealth>();
            if (target != null)
                targets.Add(target);
        }

        private void OnTriggerExit(Collider other)
        {
            UnitHealth target = other.GetComponent<UnitHealth>();
            if (target != null)
                targets.Remove(target);
        }
    }
}