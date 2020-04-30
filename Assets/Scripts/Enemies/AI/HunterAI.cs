using UnityEngine;

namespace Enemies.AI
{
    public class HunterAI : MonoBehaviour
    {
        [SerializeField] private Route m_route = null;
        [SerializeField] private RouteMover m_mover = null;

        private void OnIntersected(Rigidbody other, float position)
        {
            if (other.CompareTag("Player"))
            {
                m_mover.Target = position;
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