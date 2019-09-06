using System.Collections.Generic;
using UnityEngine;

namespace MazeMyTD
{
    [RequireComponent(typeof(SphereCollider))]
    [RequireComponent(typeof(LineRenderer))]
    public class Tower : ABuilding
    {
        public int atkDamage;
        public float atkCoolDown;
        public float atkRange;

        private float atkReloadTimer = 0f;
        private List<UnitHealth> targets = new List<UnitHealth>();
        private LineRenderer lineRenderer;

        private void Awake()
        {
            GetComponent<SphereCollider>().radius = atkRange;
            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.positionCount = 361;
            Vector3[] positions = new Vector3[361];
            for (int i = 0; i < 361; i++)
            {
                float rad = Mathf.Deg2Rad * i;
                positions[i] = new Vector3(Mathf.Sin(rad) * atkRange - 3f * positionOffset.x, 0, Mathf.Cos(rad) * atkRange - 3f * positionOffset.z);
            }
            lineRenderer.SetPositions(positions);
        }

        public override void Initialize()
        {
            base.Initialize();
            GetComponent<SphereCollider>().enabled = true;
            lineRenderer.enabled = false;
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