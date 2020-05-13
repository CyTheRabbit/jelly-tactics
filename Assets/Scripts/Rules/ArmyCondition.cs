using System.Collections;
using Animations;
using Player;
using UnityEngine;

namespace Rules
{
    public class ArmyCondition : WinCondition
    {
        [SerializeField] private EventManager m_events = null;
        [SerializeField] private Army m_army = null;
        [SerializeField] private float m_checkTimer = 6;


        private Routine checkRoutine;


        public bool Activated => m_army.transform.childCount == 0;


        private void Awake()
        {
            checkRoutine = new Routine(this);
        }


        private void OnEnable()
        {
            m_events.JellyfishFlicked += Check;
        }

        private void OnDisable()
        {
            m_events.JellyfishFlicked -= Check;
        }


        private void Check()
        {
            if (Activated) checkRoutine.Start(CheckCoroutine());
        }
        private void Check(Ray obj) => Check();

        private IEnumerator CheckCoroutine()
        {
            yield return new WaitForSeconds(m_checkTimer);
            if (Activated) Trigger();
        }
    }
}