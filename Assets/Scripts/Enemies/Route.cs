using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Enemies
{
    public class Route : BaseRoute
    {
        [SerializeField] private Vector3 m_leftHand = Vector3.right;
        [SerializeField] private Vector3 m_rightHand = Vector3.right;

        private readonly RaycastHit[] raycastHits = new RaycastHit[64];
        private readonly HashSet<Rigidbody> lastingIntersections = new HashSet<Rigidbody>();

        public Vector3 LeftHand
        {
            get => m_leftHand;
#if UNITY_EDITOR
            set
            {
                m_leftHand = value;
                Refresh();
            }
#endif
        }

        public Vector3 RightHand
        {
            get => m_rightHand;
#if UNITY_EDITOR
            set
            {
                m_rightHand = value;
                Refresh();
            }
#endif
        }

        public override float Length => (RightHand - LeftHand).magnitude;

        public override Vector3 Rotation(float _) => Vector3.Cross(RightHand - LeftHand, Vector3.up);

        public override (bool, float) WillIntersect(Ray trajectory)
        {
            Vector3 ownStart = LeftHand;
            Vector3 ownDirection = (RightHand - LeftHand).normalized;
            Vector3 perpendicular = Vector3.Cross(ownDirection, Vector3.up).normalized;

            if (trajectory.direction.normalized == ownDirection) return (false, 0);

            Vector3 distance = trajectory.origin - ownStart;

            float yDiff = Vector3.Dot(trajectory.direction.normalized, ownDirection);
            float xDiff = Vector3.Dot(trajectory.direction.normalized, perpendicular);
            float dy = yDiff / xDiff;

            float xDistance = Vector3.Dot(distance, -perpendicular);
            float yDistance = Vector3.Dot(distance, ownDirection);

            float intersectionPoint = (yDistance + xDistance * dy) / Length;
            bool will = intersectionPoint >= 0 && intersectionPoint <= 1;

            return (will, intersectionPoint);
        }

        private void CheckIntersection()
        {
            Ray ray = new Ray(LeftHand, RightHand - LeftHand);
            Physics.RaycastNonAlloc(ray, raycastHits, Length);
            foreach (RaycastHit hit in raycastHits
                .Where(h => h.rigidbody != null))
            {
                Rigidbody body = hit.rigidbody;
                if (!lastingIntersections.Contains(body))
                {
                    float position = hit.distance / Length;
                    OnActorIntersected(body, position);
                }
            }
            lastingIntersections.Clear();
            foreach (Rigidbody body in raycastHits
                .Select(h => h.rigidbody)
                .Where(r => r != null))
            {
                lastingIntersections.Add(body);
            }
        }

        public override Vector3 Lerp(float position) => Vector3.Lerp(LeftHand, RightHand, position);

        private void FixedUpdate()
        {
            CheckIntersection();
        }
    }

    [CustomEditor(typeof(Route)), CanEditMultipleObjects]
    public sealed class RouteEditor : Editor
    {
        private void OnSceneGUI()
        {
            Route route = (Route) target;

            EditorGUI.BeginChangeCheck();
            const float steps = 32;
            for (int i = 0; i < steps; i++)
            {
                Handles.color = Color.Lerp(Color.white, Color.red, i / steps);
                Handles.DrawLine(route.Lerp(i / steps), route.Lerp((i + 1) / steps));
            }
            Vector3 left = Handles.PositionHandle(route.LeftHand, Quaternion.identity);
            Vector3 right = Handles.PositionHandle(route.RightHand, Quaternion.identity);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(route, "Change Look At Target Position");
                route.LeftHand = left;
                route.RightHand = right;
            }
        }
    }
}