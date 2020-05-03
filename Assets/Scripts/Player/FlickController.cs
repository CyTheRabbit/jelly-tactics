using System;
using Jellyfish;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class FlickController : MonoBehaviour
    {
        public event Action<Vector2> Flicked = null;
        public event Action<Vector2, Pawn> FlickedManual = null;


        [SerializeField] private InputConfig m_config = null;
        [SerializeField] private bool m_normalize = true;
        [SerializeField] private InputAction m_moveAction = null;
        [SerializeField] private InputAction m_flickAction = null;

        
        private Vector2 startPosition = Vector2.zero;
        private Vector2 endPosition = Vector2.zero;
        private Vector2 currentPosition = Vector2.zero;
        private float flickDuration = 0;
        private Pawn flickedPawn = null;


        private void Awake()
        {
            m_moveAction.performed += OnMove;
            m_flickAction.started += OnFlickStart;
            m_flickAction.performed += OnFlickEnd;
            if (m_config.IsManualFlick)
            {
                m_flickAction.started += SelectPawn;
            }
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

        private void SelectPawn(InputAction.CallbackContext ctx)
        {
            Vector2 screenPoint = currentPosition;
            if (Camera.main == null) return;
            Ray ray = Camera.main.ScreenPointToRay(screenPoint);
            int mask = LayerMask.NameToLayer("Jellyfish");
            flickedPawn = Physics.Raycast(ray, out RaycastHit hit, mask) ? hit.collider.GetComponent<Pawn>() : null;
        }

        private void OnFlickEnd(InputAction.CallbackContext ctx)
        {
            endPosition = currentPosition;
            flickDuration = (float)(ctx.time - ctx.startTime);
            Flick(flickedPawn);
        }

        private void Flick(Pawn pawn = null)
        {
            Vector2 delta = endPosition - startPosition;
            if (!m_config.IsFlick(delta, flickDuration)) return;
            if (m_normalize) delta.Normalize();
            if (pawn != null)
            {
                FlickedManual?.Invoke(delta, pawn);
            }
            else
            {
                Flicked?.Invoke(delta);
            }
        }
    }
}