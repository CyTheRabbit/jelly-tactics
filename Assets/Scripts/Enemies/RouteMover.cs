using System;
using UnityEngine;

namespace Enemies
{
    public class RouteMover : MonoBehaviour
    {
        private event Action ReachedTarget = null;

        [Range(0, 1)] [SerializeField] private float m_speed = 1f;
        
        private RouteFollower follower = null;

        private float target = 0.5f;

        public float Target
        {
            get => target;
            set
            {
                target = value;
                enabled = true;
            }
        }

        public bool Halt => !enabled;

        private void Awake()
        {
            follower = GetComponent<RouteFollower>();
        }

        private void FixedUpdate()
        {
            float maxDiff = m_speed * Time.fixedDeltaTime;
            float diff = Mathf.Clamp(Target - follower.Position, -maxDiff, maxDiff);
            if (Mathf.Approximately(diff, 0))
            {
                ReachedTarget?.Invoke();
                enabled = false;
                return;
            }
            follower.Position += diff;
        }
    }
}