using System;
using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(Rigidbody))]
    public class RouteFollower : MonoBehaviour
    {
        private event Action ReachedTarget = null;


        [Range(0, 1)] [SerializeField] private float m_startingPosition = 0.5f;
        [Range(0, 1)] [SerializeField] private float m_speed = 1f;
        
        
        private float position = 0.5f;
        private float target = 0.5f;


        public float Position
        {
            get => position;
            set
            {
                position = value;
                GetComponentInParent<Route>().Refresh();
            }
        }

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


        private void FixedUpdate()
        {
            float maxDiff = m_speed * Time.fixedDeltaTime;
            float diff = Mathf.Clamp(Target - Position, -maxDiff, maxDiff);
            if (Mathf.Approximately(diff, 0))
            {
                ReachedTarget?.Invoke();
                enabled = false;
                return;
            }

            Position += diff;
        }
        private void OnValidate()
        {
            Position = m_startingPosition;
        }
    }
}