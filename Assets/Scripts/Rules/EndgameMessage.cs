using UI;
using UnityEngine;

namespace Rules
{
    public class EndgameMessage : MonoBehaviour
    {
        [SerializeField] private EventManager m_events = null;
        [SerializeField] private TaskUI m_ui = null;
        [SerializeField, Multiline] private string m_message = null;


        private void OnEnable()
        {
            m_events.GameCompleted += ShowMessage;
        }

        private void OnDisable()
        {
            m_events.GameCompleted -= ShowMessage;
        }

        private void ShowMessage()
        {
            m_ui.SetSkin("Decor");
            m_ui.DisplayMessage(m_message);
        }
    }
}