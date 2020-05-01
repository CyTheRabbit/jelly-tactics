using System;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;

namespace Jellyfish
{
    [CreateAssetMenu(fileName = "Jellyfish", menuName = "Configs/Jellyfish", order = 0)]
    public class JellyfishConfig : ScriptableObject
    {
        [Serializable]
        public class ImpulseDescription
        {
            [SerializeField] private float m_impulse = 0;
            [SerializeField] private float m_delay = 1f;
            [SerializeField] private float m_drag = 2f;
            
            public YieldInstruction Wait => new WaitForSeconds(m_delay);
            public float Power => m_impulse;
            public float Draw => m_drag;
        }
        
        [SerializeField] private string m_typeName = null;
        [SerializeField, ReorderableList] private ImpulseDescription[] m_impulses = null;

        public string Name => m_typeName;

        public IEnumerable<ImpulseDescription> Impulses => m_impulses;
    }
}