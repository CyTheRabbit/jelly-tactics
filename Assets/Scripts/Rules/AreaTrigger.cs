using UnityEngine;
using UnityEngine.Events;

namespace Rules
{
    public class AreaTrigger : MonoBehaviour
    {
        [SerializeField] private UnityEvent m_onEnter = null;


        private void OnTriggerEnter(Collider other)
        {
            m_onEnter?.Invoke();
        }
    }
}