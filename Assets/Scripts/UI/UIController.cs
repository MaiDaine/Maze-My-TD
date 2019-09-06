using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MazeMyTD
{
    public class UIController : MonoBehaviour
    {
#pragma warning disable 0649 //Field "" is never assigned to, and will always have its default value null
        [SerializeField]
        PlayerData playerData;
        [SerializeField]
        private Text playerHealthText;
        [SerializeField]
        private Text playerRessourcesText;
        [SerializeField]
        private ActionBarSlot actionBarSlotRef;
        [SerializeField]
        private RectTransform sellIcone;
        [SerializeField]
        private Text waveTimer;
#pragma warning restore 0649

        private void Start()
        {
            OnPlayerHealthChange();
            OnPlayerRessourcesChange();

            float slotSize = actionBarSlotRef.GetComponent<RectTransform>().sizeDelta.x;
            float padding = slotSize / 4f;
            float startPos = -playerData.availableBuildings.Length * (slotSize + padding) / 4f;

            for (int i = 0; i < playerData.availableBuildings.Length; i++)
            {
                ActionBarSlot slot = Instantiate(actionBarSlotRef, transform);
                RectTransform rect = slot.GetComponent<RectTransform>();
                rect.position = new Vector2(transform.position.x + startPos + i * (slotSize + padding), rect.position.y);
                slot.SetSlotData(playerData.availableBuildings[i].buildingIcone, playerData.availableBuildings[i].ressourceCost.ToString(), i);
            }
            sellIcone.position = new Vector2(transform.position.x + startPos - sellIcone.sizeDelta.x, sellIcone.position.y);
        }

        public void OnPlayerHealthChange()
        {
            playerHealthText.text = playerData.health.ToString();
        }

        public void OnPlayerRessourcesChange()
        {
            playerRessourcesText.text = playerData.ressources.ToString();
        }

        public void SetWaveTimer(int timer)
        {
            if (timer <= 0)
                waveTimer.text = "";
            else
                waveTimer.text = timer.ToString();
        }

    }
}
