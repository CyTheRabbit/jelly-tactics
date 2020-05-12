using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Rules
{
    public class WinConditionsContainer : MonoBehaviour
    {
        [SerializeField] private UnityEvent m_onWin = null;
        [SerializeField] private UnityEvent m_onLoose = null;


        public IEnumerable<WinCondition> Optionals => GetComponentsInChildren<WinCondition>()
            .Where(cond => cond.Outcome == WinCondition.Type.Optional);


        public void Win()
        {
            m_onWin?.Invoke();
        }

        public void Loose()
        {
            m_onLoose?.Invoke();
        }
    }
}