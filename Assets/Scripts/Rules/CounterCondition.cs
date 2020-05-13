using UnityEngine;

namespace Rules
{
    public class CounterCondition : WinCondition
    {
        [Space]
        [SerializeField] private int m_threshold = 5;


        private int collected = 0;


        public bool Activated => collected >= m_threshold;


        public void Add()
        {
            if (!Activated & ++collected >= m_threshold) Trigger();
        }

        public void AddMultiple(int count)
        {
            if (!Activated & (collected += count) >= m_threshold) Trigger();
        }

        public void Subtract()
        {
            collected--;
        }
    }
}