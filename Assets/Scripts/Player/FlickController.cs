using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class FlickController : MonoBehaviour
    {
        public event Action<Vector2> Flicked = null;


        [SerializeField] private InputConfig m_config = null;
        [SerializeField] private bool m_normalize = true;
        [SerializeField] private InputAction m_moveAction = null;
        [SerializeField] private InputAction m_flickAction = null;

        
        private Vector2 startPosition = Vector2.zero;
        private Vector2 endPosition = Vector2.zero;
        private Vector2 currentPosition = Vector2.zero;
        private float flickDuration = 0;


        private void Awake()
        {
            m_moveAction.performed += OnMove;
            m_flickAction.started += OnFlickStart;
            m_flickAction.performed += OnFlickEnd;
        }

        private void OnEnable()
        {
            m_moveAction.Enable();
            m_flickAction.Enable();
        }

        private void OnDisable()
        {
            m_moveAction.Disable();
            m_flickAction.Disable();
        }

        
        private void OnMove(InputAction.CallbackContext ctx)
        {
            currentPosition = ctx.ReadValue<Vector2>();
        }

        private void OnFlickStart(InputAction.CallbackContext ctx)
        {
            startPosition = currentPosition;
        }

        private void OnFlickEnd(InputAction.CallbackContext ctx)
        {
            endPosition = currentPosition;
            flickDuration = (float)(ctx.time - ctx.startTime);
            Flick();
        }

        private void Flick()
        {
            Vector2 delta = endPosition - startPosition;
            if (!m_config.IsFlick(delta, flickDuration)) return;
            if (m_normalize) delta.Normalize();
            Flicked?.Invoke(delta);
        }
    }
}