using UnityEngine.AI;
using UnityEngine;

namespace MazeMyTD
{
    [RequireComponent(typeof(LineRenderer))]
    public class PathRenderer : MonoBehaviour
    {
        private LineRenderer lineRenderer;

        private void Awake()
        {
            lineRenderer = GetComponent<LineRenderer>();
        }

        public void UpdateRenderedPath(NavMeshPath path, Vector3 startOffsetPosition)
        {
            lineRenderer.positionCount = path.corners.Length + 1;
            lineRenderer.SetPosition(0, new Vector3(startOffsetPosition.x, path.corners[0].y, startOffsetPosition.z));
            for (int i = 0; i < path.corners.Length; i++)
                lineRenderer.SetPosition(i + 1, new Vector3(path.corners[i].x, path.corners[i].y, path.corners[i].z));
        }
    }
}