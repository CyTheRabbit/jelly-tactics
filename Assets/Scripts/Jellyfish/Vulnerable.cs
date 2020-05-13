using UnityEngine;
using UnityEngine.Events;

namespace Jellyfish
{
    public class Vulnerable : MonoBehaviour
    {
        [SerializeField] private UnityEvent m_onPerish = null;

        public void Perish()
        {
            m_onPerish?.Invoke();
        }
    }
}