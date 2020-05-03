using System;
using UnityEngine;

namespace Enemies
{
    public abstract class BaseRoute : MonoBehaviour
    {
        public abstract float Length { get; }
        
        public abstract Vector3 Lerp(float t);
        public abstract Vector3 Rotation(float t);

        public event Action<Rigidbody, float> ActorIntersected;

        public void Refresh()
        {
            foreach (RouteFollower follower in GetComponentsInChildren<RouteFollower>())
            {
                follower.transform.position = Lerp(follower.Position);
                follower.transform.rotation = Quaternion.LookRotation(Rotation(follower.Position));
            }
        }

        protected void OnActorIntersected(Rigidbody actor, float position)
        {
            ActorIntersected?.Invoke(actor, position);
        }
    }
}