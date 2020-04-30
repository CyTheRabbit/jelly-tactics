using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(Rigidbody))]
    public class RouteFollower : MonoBehaviour
    {
        [Range(0, 1)] [SerializeField] private float m_startingPosition = 0.5f;
        
        private float position = 0.5f;

        public float Position
        {
            get => position;
            set
            {
                position = value;
                GetComponentInParent<Route>().Refresh();
            }
        }

        private void OnValidate()
        {
            Position = m_startingPosition;
        }
    }
}