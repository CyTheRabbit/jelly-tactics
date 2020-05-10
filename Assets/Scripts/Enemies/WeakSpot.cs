using UnityEngine;
using UnityEngine.Events;

namespace Enemies
{
    public class WeakSpot : MonoBehaviour
    {
        [SerializeField] private UnityEvent m_onHit = null;

        public interface IListener
        {
            void OnWeakSpotHit(Collider collision);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            GetComponentInParent<IListener>().OnWeakSpotHit(other);
            m_onHit?.Invoke();
        }
    }
}