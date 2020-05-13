using UnityEngine;

namespace Jellyfish
{
    [RequireComponent(typeof(Pawn))]
    public class EventBinder : MonoBehaviour
    {
        [SerializeField] private float m_threshold = 0.1f;
        [SerializeField] private EventManager m_events = null;

        private void OnCollisionEnter(Collision other)
        {
            if (other.impulse.magnitude > m_threshold)
            {
                m_events.OnJellyfishBumped(other.contacts[0].point);
            }
        }
    }
}