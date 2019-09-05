using UnityEngine;
using UnityEngine.UI;

namespace MazeMyTD
{
    public class HealthBarHandler : MonoBehaviour
    {
#pragma warning disable 0649 //Field "" is never assigned to, and will always have its default value null
        [SerializeField]
        private Image healthFillBar;
#pragma warning restore 0649

        private Quaternion iniRot;

        private void Awake()
        {
            iniRot = gameObject.transform.rotation;
        }
        private void LateUpdate()
        {
            gameObject.transform.rotation = iniRot;
        }

        public void SetHealthFillAmount(float value)
        {
            healthFillBar.fillAmount = value;
        }
    }
}
