using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Enemies
{
    public class Route : MonoBehaviour
    {
        public event Action<Rigidbody, float> ActorIntersected = null;

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

        public float Length => (RightHand - LeftHand).magnitude;

        public Vector3 Rotation(float _) => Vector3.Cross(RightHand - LeftHand, Vector3.up);

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
                    ActorIntersected?.Invoke(body, position);
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

        public Vector3 Lerp(float position) => Vector3.Lerp(LeftHand, RightHand, position);

        private void FixedUpdate()
        {
            CheckIntersection();
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawIcon(LeftHand, "arrowLeft.png", false, Color.white);
            Gizmos.DrawIcon(RightHand, "arrowRight.png", false, Color.white);
            Gizmos.DrawLine(LeftHand, RightHand);
        }

        public void Refresh()
        {
                foreach (RouteFollower follower in GetComponentsInChildren<RouteFollower>())
                {
                    follower.transform.position = Lerp(follower.Position);
                    follower.transform.rotation = Quaternion.LookRotation(Rotation(follower.Position));
                }
        }
    }

    [CustomEditor(typeof(Route)), CanEditMultipleObjects]
    public sealed class RouteEditor : Editor
    {
        private void OnSceneGUI()
        {
            Route route = (Route) target;

            EditorGUI.BeginChangeCheck();
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