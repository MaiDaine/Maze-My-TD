using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MazeMyTD
{
    public class MessageLog : MonoBehaviour
    {
        private const int maxMessages = 4;

#pragma warning disable 0649 //Field "" is never assigned to, and will always have its default value null
        [SerializeField]
        private GameObject messageBox;
        [SerializeField]
        private GameObject messageRef;
#pragma warning restore 0649

        private Queue<GameObject> activeChatMessage = new Queue<GameObject>();
        private Color warningColor = new Color(220, 53, 69, 255);

        public void AddMessage(string message, bool warning = false)
        {
            GameObject tmp;

            if (activeChatMessage.Count == 0)
                messageBox.SetActive(true);

            if (activeChatMessage.Count > maxMessages)
            {
                tmp = activeChatMessage.Dequeue();
                Destroy(tmp);
            }

            tmp = Instantiate(messageRef, messageBox.GetComponentInChildren<GridLayoutGroup>().gameObject.transform);
            tmp.GetComponent<Text>().text = message;
            if (warning)
                tmp.GetComponent<Text>().color = warningColor;
            activeChatMessage.Enqueue(tmp);
        }
    }
}
