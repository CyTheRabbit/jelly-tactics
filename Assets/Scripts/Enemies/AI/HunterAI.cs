using UnityEngine;

namespace Enemies.AI
{
    public class HunterAI : MonoBehaviour
    {
        [SerializeField] private int m_priority = 0;
        [SerializeField] private BaseRoute m_route = null;
        [SerializeField] private RouteFollower m_follower = null;
        [SerializeField] private EventManager m_events = null;

        private void OnIntersected(Rigidbody other, float position)
        {
            if (other.CompareTag("Player"))
            {
                m_follower.SetTarget(position, m_priority);
            }
        }

        private void OnEnable()
        {
            m_events.JellyfishFlicked += CheckFlick;
            m_route.ActorIntersected += OnIntersected;
        }

        private void OnDisable()
        {
            m_events.JellyfishFlicked -= CheckFlick;
            m_route.ActorIntersected -= OnIntersected;
        }


        private void CheckFlick(Ray flick)
        {
            (bool will, float position) = m_route.WillIntersect(flick);
            if (will)
            {
                m_follower.SetTarget(position, m_priority);
            }
        }
    }
}