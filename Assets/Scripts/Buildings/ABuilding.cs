﻿using UnityEngine;
using UnityEngine.AI;

namespace MazeMyTD
{
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(NavMeshObstacle))]
    public abstract class ABuilding : MonoBehaviour
    {
        public Texture buildingIcone;
        public Tile occupiedTile;
        public int ressourceCost;

#pragma warning disable 0649 //Field "" is never assigned to, and will always have its default value null
        [SerializeField]
        private Material finalMaterial;
        [SerializeField]
        protected Vector3 positionOffset;
#pragma warning restore 0649


        public virtual void Initialize()
        {
            GetComponent<MeshRenderer>().sharedMaterial = finalMaterial;
        }

        public void Move(Vector3 position)
        {
            transform.position = new Vector3(
                position.x + positionOffset.x,
                position.y + positionOffset.y,
                position.z + positionOffset.z);
        }
    }
}