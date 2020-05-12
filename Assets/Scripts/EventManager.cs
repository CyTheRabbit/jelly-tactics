using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Event Manager", menuName = "Managers/Event Manager", order = 0)]
public class EventManager : BaseManager
{
    
    
    public event Action ManagersLoaded = null;

    public event Action<Ray> JellyfishFlicked = null;
    public event Action<Vector3> JellyfishBumped = null;
    public event Action JellyfishPerished = null;

    public event Action CrabDefeated = null;

    public event Action Victory = null;
    public event Action ClearUI = null;
    public event Action LevelStarted = null;

    public void OnManagersLoaded()
    {
        ManagersLoaded?.Invoke();
    }

    public void OnJellyfishFlicked(Ray flick)
    {
        JellyfishFlicked?.Invoke(flick);
    }

    public void OnJellyfishBumped(Vector3 bump)
    {
        JellyfishBumped?.Invoke(bump);
    }

    public void OnJellyfishPerished()
    {
        JellyfishPerished?.Invoke();
    }

    public void OnCrabDefeated()
    {
        CrabDefeated?.Invoke();
    }

    public void OnClearUI()
    {
        ClearUI?.Invoke();
    }

    public void OnVictory()
    {
        Victory?.Invoke();
    }

    public override void Init()
    {
        ManagersLoaded = null;
    }

    public void OnLevelLoaded(AsyncOperation operation)
    {
        LevelStarted?.Invoke();
    }
}