using UnityEngine;
using UnityEngine.Events;

public class EffectsBinder : MonoBehaviour
{
    [SerializeField] private EventManager m_events = null;

    [SerializeField] private UnityEvent m_onFlick = null;
    [SerializeField] private UnityEvent m_onBump = null;
    [SerializeField] private UnityEvent m_onJellyfishPerish = null;
    [SerializeField] private UnityEvent m_onCrabDefeated = null;
    [SerializeField] private UnityEvent m_onVictory = null;
    

    private void OnEnable()
    {
        m_events.JellyfishFlicked += Flick;
        m_events.JellyfishBumped += Bump;
        m_events.JellyfishPerished += m_onJellyfishPerish.Invoke;
        m_events.CrabDefeated += m_onCrabDefeated.Invoke;
        m_events.Victory += m_onVictory.Invoke;
    }

    private void OnDisable()
    {
        m_events.JellyfishFlicked -= Flick;
        m_events.JellyfishBumped -= Bump;
        m_events.JellyfishPerished -= m_onJellyfishPerish.Invoke;
        m_events.CrabDefeated -= m_onCrabDefeated.Invoke;
        m_events.Victory -= m_onVictory.Invoke;
    }


    private void Flick(Ray _) => m_onFlick?.Invoke();

    private void Bump(Vector3 _) => m_onBump?.Invoke();
}