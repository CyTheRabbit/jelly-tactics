using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Event Manager", menuName = "Managers/Event Manager", order = 0)]
public class EventManager : BaseManager
{
    public event Action ManagersLoaded = null;

    public event Action<Ray> JellyfishFlicked = null;

    public void OnManagersLoaded()
    {
        ManagersLoaded?.Invoke();
    }

    public void OnJellyfishFlicked(Ray flick)
    {
        JellyfishFlicked?.Invoke(flick);
    }

    public override void Init()
    {
        ManagersLoaded = null;
    }
}