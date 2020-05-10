using UnityEngine;

namespace Enemies
{
    public class Breakable : MonoBehaviour
    {
        [SerializeField] private float m_threshold = 1.0f;


        private bool dead = false;


        private void OnCollisionEnter(Collision other)
        {
            if (other.impulse.magnitude > m_threshold) dead = true;
        }

        private void OnCollisionExit()
        {
            if (dead)
            {
                GameObject o;
                (o = gameObject).SetActive(false);
                Destroy(o);
            }
        }
    }
}