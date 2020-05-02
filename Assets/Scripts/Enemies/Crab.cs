using UnityEngine;

namespace Enemies
{
    public class Crab : RouteFollower, WeakSpot.IListener
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