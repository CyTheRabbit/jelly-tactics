using UnityEngine;
using UnityEngine.Events;

namespace Enemies
{
    public class Breakable : MonoBehaviour
    {
        [SerializeField] private float m_threshold = 1.0f;
        [SerializeField] private UnityEvent m_onBreak = null;


        private bool dead = false;


        private void OnCollisionEnter(Collision other)
        {
            if (other.impulse.magnitude > m_threshold)
            {
                dead = true;
                m_onBreak?.Invoke();
            }
        }

        private void OnCollisionExit()
        {
            if (!dead) return;
            foreach (Renderer component in GetComponents<Renderer>()) component.enabled = false;
            foreach (Collider component in GetComponents<Collider>()) component.enabled = false;
        }
    }
}