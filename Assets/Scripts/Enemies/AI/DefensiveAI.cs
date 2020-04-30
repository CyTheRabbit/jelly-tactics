using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Enemies.AI
{
    public class DefensiveAI : MonoBehaviour
    {
        [SerializeField] private Route m_route = null;
        [SerializeField] private RouteFollower m_follower = null;
        [SerializeField] private RouteMover m_mover = null;
        [SerializeField] private float m_safeDistance = 2;

        private readonly List<Ray> rays = new List<Ray>();

        private struct Location
        {
            public float Position;
            public float SpaceBefore;
        }


        private IEnumerator Start()
        {
            while (true)
            {
                rays.Clear();
                if (!IsSafe())
                {
                    Debug.Log("unsafe");
                    // Play panic
                    ChangeLocation();
                    yield return new WaitUntil(() => m_mover.Halt);
                }
                else
                {
                    Debug.Log("safe");
                    yield return new WaitForSeconds(0.2f);
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            foreach (Ray ray in rays)
            {
                Gizmos.DrawRay(ray);
            }
        }


        private Location? CalcLocation(float t)
        {
            Vector3 pos = m_route.Lerp(t);
            Vector3 dir = m_route.Rotation(t);
            rays.Add(new Ray(pos, dir));
            int mask = 1 << LayerMask.NameToLayer("Default");
            if (Physics.Raycast(pos, dir, out RaycastHit hit, m_safeDistance, mask))
            {
                Debug.Log(hit.collider.name);
                return new Location {Position = t, SpaceBefore = hit.distance};
            }
            return null;
        }

        private void CalcLocations(int frequency, ICollection<Location> result)
        {
            if (frequency == 0) return;

            result.Clear();
            float step = 1f / frequency;
            float progress = 0;
            while (progress <= 1)
            {
                Location? loc = CalcLocation(progress);
                if (loc.HasValue)
                {
                    Debug.Log(loc.Value);
                    result.Add(loc.Value);
                }
                progress += step;
            }
        }

        private bool IsSafe(Location location)
        {
            return location.SpaceBefore <= m_safeDistance;
        }

        private bool IsSafe()
        {
            Location? current = CalcLocation(m_follower.Position);
            return current.HasValue && IsSafe(current.Value);
        }

        private void ChangeLocation()
        {
            List<Location> locations = new List<Location>();
            CalcLocations(16, locations);
            if (locations.Count == 0)
            {
                // Play panic
                return;
            }
            int Comparer(Location a, Location b) { return (a.SpaceBefore < b.SpaceBefore) ? -1 : 1; }
            locations.Sort(Comparer);
            if (!locations.Any(IsSafe))
            {
                // Play panic
                return;
            }
            Debug.Log(locations.Count);
            m_mover.Target = locations.Where(IsSafe).First().Position;
        }
    }
}