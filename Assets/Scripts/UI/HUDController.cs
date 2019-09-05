using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MazeMyTD
{
    public class HUDController : MonoBehaviour
    {
#pragma warning disable 0649 //Field "" is never assigned to, and will always have its default value null
        [SerializeField]
        private Text playerHealthText;
        [SerializeField]
        private Text playerRessourcesText;
        [SerializeField]
        PlayerData playerData;
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

    }
}
