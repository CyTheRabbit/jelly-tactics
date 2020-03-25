using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Event Manager", menuName = "Managers/Event Manager", order = 0)]
public class EventManager : BaseManager
{
    public event Action ManagersLoaded = null;

    public void OnManagersLoaded()
    {
        ManagersLoaded?.Invoke();
    }


    public override void Init()
    {
        ManagersLoaded = null;
    }
}