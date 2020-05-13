using DG.Tweening;
using UnityEngine;

namespace Visual
{
    public class LooseUIAnimator : MonoBehaviour
    {
        [SerializeField] private EventManager m_events = null;
        [SerializeField] private RectTransform m_header = null;


        private void OnEnable()
        {
            m_header.pivot *= Vector2.right;
            m_events.Defeat += Show;
            m_events.ClearUI += Hide;
        }

        private void OnDisable()
        {
            m_events.Defeat -= Show;
            m_events.ClearUI -= Hide;
        }


        public void Show()
        {
            m_header.DOPivotY(1, 1.0f);
        }

        public void Hide()
        {
            m_header.DOPivotY(0, 0.8f);
        }
    }
}