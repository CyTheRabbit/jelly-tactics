using System;
using System.Collections;
using System.Linq;
using Animations;
using UnityEngine;

namespace Jellyfish
{
    [RequireComponent(typeof(Rigidbody))]
    public class Pawn : MonoBehaviour
    {
        public event Action Stopped = null;

        [SerializeField] private JellyfishConfig m_config = null;

        private new Rigidbody rigidbody = null;
        private Routine flickRoutine = null;

        public bool Freeze
        {
            set => rigidbody.constraints = value ?
                RigidbodyConstraints.FreezeAll :
                RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
        }

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            flickRoutine = new Routine(this);
            Freeze = true;
        }

        public void Flick(Vector3 direction)
        {
            // rigidbody.AddForce(direction * m_config.Impulse, ForceMode.VelocityChange);
            flickRoutine.Start(ImpulsesCoroutine(direction));
        }

        public void Boost()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        private IEnumerator ImpulsesCoroutine(Vector3 direction)
        {
            JellyfishConfig.ImpulseDescription firstImpulse = m_config.Impulses.First();
            rigidbody.AddForce(direction * firstImpulse.Power, ForceMode.VelocityChange);
            rigidbody.drag = firstImpulse.Draw;
            foreach (JellyfishConfig.ImpulseDescription impulse in m_config.Impulses.Skip(1))
            {
                yield return impulse.Wait;
                direction = rigidbody.velocity.normalized;
                rigidbody.AddForce(direction * impulse.Power, ForceMode.VelocityChange);
                rigidbody.drag = impulse.Draw;
            }
            Stopped?.Invoke();
        }
    }
}