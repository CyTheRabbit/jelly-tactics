using System.Collections;
using UnityEngine;

namespace Enemies.AI
{
    public class PatrolAI : MonoBehaviour
    {
        [SerializeField] private RouteMover m_mover = null;

        private IEnumerator MoveCoroutine()
        {
            while (true)
            {
                m_mover.Target = 0;
                yield return new WaitUntil(() => m_mover.Halt);
                m_mover.Target = 1;
                yield return new WaitUntil(() => m_mover.Halt);
            }
        }

        private void Start()
        {
            StartCoroutine(MoveCoroutine());
        }
    }
}