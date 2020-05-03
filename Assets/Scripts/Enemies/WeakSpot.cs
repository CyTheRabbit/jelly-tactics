using UnityEngine;

namespace Enemies
{
    public class WeakSpot : MonoBehaviour
    {
        public interface IListener
        {
            void OnWeakSpotHit(Collider collision);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            GetComponentInParent<IListener>().OnWeakSpotHit(other);
        }
    }
}