using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "Input Config", menuName = "Configs/Input")]
    public class InputConfig : ScriptableObject
    {
        [Header("Flick")]
        [SerializeField] private float m_minDistance = 0;
        [SerializeField] private float m_maxDuration = 0;


        public bool IsFlick(Vector2 delta, float duration)
        {
            return delta.magnitude >= m_minDistance
                   && duration <= m_maxDuration;
        }
    }
}