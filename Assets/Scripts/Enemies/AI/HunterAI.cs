using UnityEngine;

namespace Enemies.AI
{
    public class HunterAI : MonoBehaviour
    {
        [SerializeField] private BaseRoute m_route = null;
        [SerializeField] private RouteFollower m_follower = null;

        private void OnIntersected(Rigidbody other, float position)
        {
            if (other.CompareTag("Player"))
            {
                m_follower.SetTarget(position);
            }
        }

        private void OnEnable()
        {
            m_route.ActorIntersected += OnIntersected;
        }

        private void OnDisable()
        {
            m_route.ActorIntersected -= OnIntersected;
        }
    }
}