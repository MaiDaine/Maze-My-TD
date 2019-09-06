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
        private Text waveTimer;
#pragma warning restore 0649

        private void Start()
        {
            OnPlayerHealthChange();
            OnPlayerRessourcesChange();
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
