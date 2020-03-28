using UnityEngine;

namespace Animations
{
    [CreateAssetMenu(fileName = "Animation", menuName = "Configs/Animation", order = 0)]
    public class AnimationConfig : ScriptableObject
    {
        public AnimationCurve m_curve;
        public float m_duration;
        public bool m_loop;
        public bool m_allowRestart;
    }
}