using Jellyfish;
using UnityEngine;

namespace Enemies
{
    public class Deadly : MonoBehaviour
    {
        private void OnCollisionEnter(Collision other)
        {
            Vulnerable victim = other.gameObject.GetComponent<Vulnerable>();
            if (victim == null) return;
            victim.Perish();
        }
    }
}