using DG.Tweening;
using UnityEngine;

namespace Visual
{
    public class WinUIAnimator : MonoBehaviour
    {
        [SerializeField] private EventManager m_events = null;
        [SerializeField] private RectTransform m_header = null;
        [SerializeField] private RectTransform m_footer = null;


        private void OnEnable()
        {
            m_header.pivot *= Vector2.right;
            m_events.Victory += Show;
            m_events.ClearUI += Hide;
        }

        private void OnDisable()
        {
            m_events.Victory -= Show;
            m_events.ClearUI -= Hide;
        }
        
        
        public void Show()
        {
            DOTween.Sequence()
                .Append(m_header.DOPivotY(1, 1.0f))
                .Append(m_footer.DOPivotY(0, 1.6f));
        }

        public void Hide()
        {
            DOTween.Sequence()
                .Append(m_header.DOPivotY(0, 0.8f))
                .Join(m_footer.DOPivotY(1, 0.8f));
        }
    }
}