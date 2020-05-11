using System;
using UnityEditor;
using UnityEngine;

namespace Enemies
{
    public class CircleRoute : BaseRoute
    {
        [SerializeField] private float m_radius = 1f;

        public float Radius => m_radius;

        private const float Tau = Mathf.PI * 2;

        public override float Length => m_radius * Tau;

        public override Vector3 Lerp(float t)
        {
            return transform.position
                   + Vector3.right * (Mathf.Sin(t * Tau) * Radius)
                   + Vector3.forward * (Mathf.Cos(t * Tau) * Radius);
        }

        public override Vector3 Rotation(float t)
        {
            return transform.position - Lerp(t);
        }

        public override (bool, float) WillIntersect(Ray trajectory)
        {
            return (false, 0);
        }

        private void OnValidate()
        {
            Refresh();
        }
    }

    [CustomEditor(typeof(CircleRoute))]
    public class CircleRouteEditor : Editor
    {
        private void OnSceneGUI()
        {
            CircleRoute route = (CircleRoute) target;
            Handles.color = Color.green;
            const float steps = 32;
            for (int i = 0; i < steps; i++)
            {
                Handles.DrawLine(route.Lerp(i / steps), route.Lerp((i + 1) / steps));
            }
        }
    }
}