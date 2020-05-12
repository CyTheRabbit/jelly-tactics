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
        public event Action Perished = null;

        [SerializeField] private JellyfishConfig m_config = null;

        private Vector3 startPosition = Vector3.zero;
        private Transform startParent = null;
        private new Rigidbody rigidbody = null;
        private Routine moveRoutine = null;

        public bool Freeze
        {
            set => rigidbody.constraints = value ?
                RigidbodyConstraints.FreezeAll :
                RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
        }

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            moveRoutine = new Routine(this);
            Freeze = true;
        }

        private void Start()
        {
            startPosition = rigidbody.position;
            startParent = transform.parent;
        }

        private void OnDisable()
        {
            Perished?.Invoke();
        }

        public void Flick(Vector3 direction)
        {
            // rigidbody.AddForce(direction * m_config.Impulse, ForceMode.VelocityChange);
            moveRoutine.Start(ImpulsesCoroutine(direction));
        }

        public void Boost()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            moveRoutine.Stop();
        }

        public void JumpReset()
        {
            moveRoutine.Start(JumpCoroutine(startPosition, m_config.JumpTime));
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

        private IEnumerator JumpCoroutine(Vector3 to, float duration)
        {
            RigidbodyConstraints prevConstraints = rigidbody.constraints;
            rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            rigidbody.velocity = Vector3.zero;

            Vector3 from = rigidbody.position;
            float beginTime = Time.time;
            float endTime = beginTime + duration;

            while (Time.time < endTime)
            {
                float progress = (Time.time - beginTime) / duration;
                Vector3 pos = Vector3.Lerp(from, to, progress);
                pos.y += m_config.JumpAt(progress);
                
                rigidbody.MovePosition(pos);
                yield return null;
            }
            rigidbody.MovePosition(to);
            yield return null;
            Freeze = true;
            transform.SetParent(startParent);
        }
    }
}