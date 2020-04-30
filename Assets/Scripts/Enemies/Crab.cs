using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(RouteFollower))]
    public class Crab : MonoBehaviour, WeakSpot.IListener
    {
        private void Perish()
        {
            // TODO: Add animation
            gameObject.SetActive(false);
            Destroy(this);
        }

        public void OnWeakSpotHit(Collider other)
        {
            Perish();
        }
    }
}