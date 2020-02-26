using UnityEngine;

/// <summary>
/// Application's entry point that initializes all core managers and sets
/// some execution constants.
/// </summary>
public class AppManager : ScriptableObject
{
    private void Init()
    {
#if UNITY_EDITOR
        Application.targetFrameRate = 30;
#endif
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void EntryPoint()
    {
        const string appPath = "App";
        var app = Instantiate(Resources.Load<AppManager>(appPath));
        app.Init();
    }
}
