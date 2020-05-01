using System;
using System.Collections;
using NaughtyAttributes;
using UnityEngine;

namespace Enemies.AI
{
    public class PatrolAI : MonoBehaviour
    {
        [SerializeField] private RouteMover m_mover = null;
        
        [Serializable]
        private class Place
        {
            [SerializeField] private float m_position = 0.5f;
            [SerializeField] private float m_wait = 0.5f;

            public float Position => m_position;
            public float Wait => m_wait;
        }

        [SerializeField, ReorderableList] private Place[] m_places = null;

        private IEnumerator MoveCoroutine()
        {
            while (true)
            {
                foreach (Place place in m_places)
                {
                    m_mover.Target = place.Position;
                    yield return new WaitUntil(() => m_mover.Halt);
                    yield return new WaitForSeconds(place.Wait);
                }
            }
        }

        private void Start()
        {
            StartCoroutine(MoveCoroutine());
        }
    }
}