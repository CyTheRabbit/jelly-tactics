using System;
using System.Collections;
using UnityEngine;

namespace Animations
{
    public class Routine
    {

        public Action OnEnd = null;

        private Coroutine routine;
        private readonly MonoBehaviour owner;

        public bool Active => routine != null;

        public Routine(MonoBehaviour owner)
        {
            this.owner = owner;
        }
        
        public void Start(IEnumerator enumerator)
        {
            Stop();
            routine = owner.StartCoroutine(Coroutine(enumerator));
        }

        private IEnumerator Coroutine(IEnumerator enumerator)
        {
            yield return enumerator;
            OnEnd?.Invoke();
            Stop();
        }

        public void Stop()
        {
            if (routine != null) owner.StopCoroutine(routine);
            routine = null;
        }
    }
}