using Jellyfish;
using JetBrains.Annotations;
using UnityEngine;

namespace Player
{
    public class Army : MonoBehaviour
    {
        [SerializeField] private FlickController m_flicker = null;

        private void Start()
        {
            m_flicker.Flicked += Dispatch;
        }

        private void Dispatch(Vector2 direction2d)
        {
            Vector3 direction = ProjectDirection(direction2d);
            Pawn furthest = FindFurthest(direction);
            if (furthest == null) return;
            furthest.Freeze = false;
            furthest.transform.SetParent(null, true);
            furthest.Flick(direction);
        }
        
        [CanBeNull]
        private Pawn FindFurthest(Vector3 direction)
        {
            Pawn[] pawns = GetComponentsInChildren<Pawn>();
            if (pawns.Length == 0)
            {
                // No pawns
                return null;
            }
            
            Pawn furthest = pawns[0];
            Vector3 furthestPosition = furthest.transform.position;
            foreach (Pawn pawn in pawns)
            {
                if (!FurtherThan(direction, pawn.transform.position, furthestPosition)) continue;
                furthest = pawn;
                furthestPosition = furthest.transform.position;
            }

            return furthest;
        }

        private static Vector3 ProjectDirection(Vector2 direction)
        {
            return new Vector3(direction.x, 0, direction.y);
        }

        /// <summary>
        /// Returns whether point <i>a</i> is further in given <i>direction</i> than point <i>b</i>
        /// </summary>
        private static bool FurtherThan(Vector3 direction, Vector3 a, Vector3 b)
        {
            return Vector3.Dot(direction, b - a) < 0;
        }
    }
}