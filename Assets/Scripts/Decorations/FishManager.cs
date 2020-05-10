using System;
using System.Collections;
using NaughtyAttributes;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Decorations
{
    public class FishManager : MonoBehaviour
    {
        [Serializable]
        private class Fish
        {
            [SerializeField] private GameObject m_prefab;
            [SerializeField] private float m_speed;
            [SerializeField] private float m_wobbleRange;
            [SerializeField] private float m_wobbleSpeed;
            [SerializeField] private float m_height;
            [SerializeField] private bool m_flip;
            [SerializeField] private int m_minShoal;
            [SerializeField] private int m_maxShoal;

            public GameObject Prefab => m_prefab;
            public float Speed => m_speed;
            public float Height => m_height;
            public bool Flip => m_flip;
            public float WobbleSpeed => m_wobbleSpeed;
            public float WobbleRange => m_wobbleRange;
            public int MinShoal => m_minShoal;
            public int MaxShoal => m_maxShoal;
        }

        
        private const float WobblePosFactor = 180;

        
        [SerializeField, ReorderableList] private Fish[] m_objects = null;
        [SerializeField] private float m_frequency = 5.0f;
        [SerializeField] private float m_shoalFrequency = 1f;
        [SerializeField] private float m_xLeft = -5;
        [SerializeField] private float m_xRight = 5;
        [SerializeField] private float m_zMin = 10;
        [SerializeField] private float m_zMax = 10;


        private IEnumerator Start()
        {
            while (true)
            {
                Fish fish = m_objects[Random.Range(0, m_objects.Length)];
                int shoal = Random.Range(fish.MinShoal, fish.MaxShoal);
                bool left = Random.Range(0, 2) >= 1;
                for (int i = 0; i < shoal; i++)
                {
                    float speed = left ? fish.Speed : -fish.Speed;
                    float wobbleSpeed = fish.WobbleSpeed;
                    float wobbleRange = fish.WobbleRange;

                    GameObject instance = Create(fish, left);
                    StartCoroutine(FishAnimation(instance, speed, wobbleRange, wobbleSpeed));
                    
                    yield return new WaitForSeconds(m_shoalFrequency);
                }
                yield return new WaitForSeconds(m_frequency);
            }
        }


        private GameObject Create(Fish fish, bool left)
        {
            float x = left ? m_xLeft : m_xRight;
            float y = fish.Height;
            float z = Random.Range(m_zMin, m_zMax);
            Vector3 position = new Vector3(x, y, z);
            Quaternion rotation = Quaternion.identity * Quaternion.Euler(0, 180, 0);
            Vector3 scale = Vector3.one;
            if (left ^ fish.Flip) scale.x *= -1;
            GameObject instance = Instantiate(fish.Prefab, position, rotation);
            instance.transform.localScale = scale;
            return instance;
        }

        private IEnumerator FishAnimation(GameObject instance, float speed, float wobbleRange, float wobbleSpeed)
        {
            Transform t = instance.transform;
            Vector3 wobbleDirection;

            Func<bool> isOver;
            if (speed > 0)
            {
                isOver = () => t.position.x >= m_xRight;
                wobbleDirection = Vector3.forward;
            }
            else
            {
                isOver = () => t.position.x <= m_xLeft;
                wobbleDirection = Vector3.back;
            }

            float startTime = Time.time;
            while (!isOver())
            {
                float period = Mathf.Sin((Time.time - startTime) * wobbleSpeed);
                float wobble = (wobbleRange * Time.deltaTime * period);
                t.Rotate(t.up, wobble);
                t.position += Vector3.right * (speed * Time.deltaTime)
                    + wobbleDirection * (period * Time.deltaTime * wobbleRange / WobblePosFactor);
                
                yield return null;
            }
            Destroy(instance);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawLine(
                new Vector3(m_xLeft, 1.5f, m_zMin),
                new Vector3(m_xLeft, 1.5f, m_zMax));
            Gizmos.DrawLine(
                new Vector3(m_xRight, 1.5f, m_zMin),
                new Vector3(m_xRight, 1.5f, m_zMax));
        }
    }
}