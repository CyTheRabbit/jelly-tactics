using UnityEngine;
using UnityEngine.EventSystems;

namespace Enemies
{
    public class WeakSpot : MonoBehaviour
    {
        public interface IListener : IEventSystemHandler
        {
            void OnWeakSpotHit(Collider collision);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            ExecuteEvents.ExecuteHierarchy<IListener>(transform.parent.gameObject, null,
                (listener, _) => listener.OnWeakSpotHit(other));
        }
    }
}