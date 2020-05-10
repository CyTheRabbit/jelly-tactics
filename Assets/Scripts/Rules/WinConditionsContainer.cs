using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Rules
{
    public class WinConditionsContainer : MonoBehaviour
    {
        public event Action OnWin = null;
        public event Action OnLoose = null;


        public IEnumerable<WinCondition> Optionals => GetComponentsInChildren<WinCondition>()
            .Where(cond => cond.Outcome == WinCondition.Type.Optional);


        public void Win()
        {
            OnWin?.Invoke();
        }

        public void Loose()
        {
            OnLoose?.Invoke();
        }
    }
}