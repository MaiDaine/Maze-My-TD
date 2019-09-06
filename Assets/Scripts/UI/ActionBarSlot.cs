using UnityEngine;
using UnityEngine.UI;

namespace MazeMyTD
{
    public class ActionBarSlot : MonoBehaviour
    {
#pragma warning disable 0649 //Field "" is never assigned to, and will always have its default value null
        [SerializeField]
        private RawImage icone;
        [SerializeField]
        private Text cost;
#pragma warning restore 0649

        private int slotIndex;

        public void SetSlotData(Texture texture, string cost, int slotIndex)
        {
            this.cost.text = cost;
            this.icone.texture = texture;
            this.slotIndex = slotIndex;
        }

        public void OnSlotClick()
        {
            ConstructionController.instance.SpawnBuilding(slotIndex);
        }
    }
}
