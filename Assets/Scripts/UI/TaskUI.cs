using DG.Tweening;
using TMPro;
using UnityEngine;

namespace UI
{
    public class TaskUI : MonoBehaviour
    {
        [SerializeField] private EventManager m_events = null;
        [SerializeField] private LevelManager m_levelManager = null;
        [Space]
        [SerializeField] private RectTransform m_panel = null;
        [SerializeField] private CanvasGroup m_panelGroup = null;
        [SerializeField] private TMP_Text m_text = null;

        private void Start()
        {
            m_panel.pivot = m_panel.pivot * Vector2.right + Vector2.up;
            m_panelGroup.alpha = 0;
        }

        private void OnEnable()
        {
            m_events.LevelStarted += OnLevelStarted;
        }

        private void OnLevelStarted()
        {
            m_text.text = m_levelManager.CurrentDescription;
            Show();
        }

        private void Show()
        {
            DOTween.Sequence()
                .AppendCallback(() => SetGroup(true))
                .Append(m_panel.DOPivotY(0, 1))
                .Join(m_panelGroup.DOFade(1, 1))
                .AppendInterval(3)
                .Append(m_panel.DOPivotY(1, 1))
                .Join(m_panelGroup.DOFade(0, 1))
                .AppendCallback(() => SetGroup(false));
        }

        private void SetGroup(bool active)
        {
            m_panelGroup.interactable = active;
            m_panelGroup.blocksRaycasts = active;
        }
    }
}