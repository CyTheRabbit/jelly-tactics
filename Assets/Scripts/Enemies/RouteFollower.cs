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
        [SerializeField] private bool m_loopRoute = false;
        
        
        private float position = 0.5f;
        private float target = 0.5f;
        private int LapCount => Mathf.FloorToInt(position);


        public float Position
        {
            get => Mathf.Repeat(position, 1);
            set
            {
                position = value;
                GetComponentInParent<BaseRoute>().Refresh();
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
            float diff = Mathf.Clamp(Target - position, -maxDiff, maxDiff);
            if (Mathf.Approximately(diff, 0))
            {
                ReachedTarget?.Invoke();
                enabled = false;
                return;
            }

            Position = position + diff;
        }
        private void OnValidate()
        {
            Position = m_startingPosition;
        }

        public void SetTarget(float pos)
        {
            if (m_loopRoute)
            {
                float left = LapCount + pos;
                float right = LapCount + pos;
                if (Position < pos)
                    left--;
                else
                    right++;

                Target = (position - left < right - position) ? left : right;
            }
            else
            {
                Target = pos;
            }
        }
    }
}