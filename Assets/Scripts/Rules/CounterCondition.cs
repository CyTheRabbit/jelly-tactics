using UnityEngine;

namespace Rules
{
    public class CounterCondition : WinCondition
    {
        [Space]
        [SerializeField] private int m_threshold = 5;


        private int collected = 0;


        public void Add()
        {
            if (++collected >= m_threshold) Trigger();
        }

        public void AddMultiple(int count)
        {
            if ((collected += count) >= m_threshold) Trigger();
        }

        public void Subtract()
        {
            collected--;
        }
    }
}