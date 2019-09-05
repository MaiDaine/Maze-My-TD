using UnityEngine.AI;
using UnityEngine;

namespace MazeMyTD
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class UnitMovement : MonoBehaviour
    {
        public float currentSpeed;

        private NavMeshAgent agent;

        public void Initialize(float unitSpeed)
        {
            currentSpeed = unitSpeed;
            agent = GetComponent<NavMeshAgent>();
            agent.speed = unitSpeed;
        }

        public void SetPath(NavMeshPath path)
        {
            agent.enabled = true;
            agent.path = path;
        }

        public void SetPath(NavMeshPath path, Vector3 wrapPosition)
        {
            agent.Warp(wrapPosition);
            agent.enabled = true;
            agent.path = path;
        }
    }
}