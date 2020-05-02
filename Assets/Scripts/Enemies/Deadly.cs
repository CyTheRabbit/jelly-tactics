using UnityEngine;

namespace Enemies
{
    public class Deadly : MonoBehaviour
    {
        private void OnCollisionEnter(Collision other)
        {
            Destroy(other.gameObject);
        }
    }
}