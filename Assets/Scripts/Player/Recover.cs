using System.Collections;
using Jellyfish;
using UnityEngine;

namespace Player
{
    public class Recover : MonoBehaviour
    {
        [SerializeField] private float m_noRecoveryDuration = 0.1f;

        private bool shouldRecover = false;

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(m_noRecoveryDuration);
            shouldRecover = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            Pawn pawn = other.GetComponent<Pawn>();
            if (pawn == null) return;
            if (!shouldRecover) return;
            pawn.JumpReset();
        }
    }
}