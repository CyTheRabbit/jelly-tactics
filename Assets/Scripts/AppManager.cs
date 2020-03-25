using UnityEngine;

/// <summary>
/// Application's entry point that initializes all core managers and sets
/// some execution constants.
/// </summary>
public class AppManager : ScriptableObject
{
    [SerializeField] private ManagerConfig m_managers = null;
    [SerializeField] private EventManager m_events = null;


    private void Init()
    {
#if UNITY_EDITOR
        Application.targetFrameRate = 30;
#endif
        m_managers.Init();
        m_events.OnManagersLoaded();
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void EntryPoint()
    {
        const string appPath = "App";
        var app = Instantiate(Resources.Load<AppManager>(appPath));
        app.Init();
    }
}
