using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Level Manager", menuName = "Managers/Level Manager", order = 0)]
public class LevelManager : BaseManager
{
    [Serializable]
    private class Level
    {
        [SerializeField, Scene] private string m_scene = null;
        [SerializeField] private string m_name = null;

        public string Scene => m_scene;
        public string Name => m_name;
        public bool IsLoaded { get; set; }
    }
    
    [SerializeField] private EventManager m_events = null;
    [SerializeField, ReorderableList] private Level[] m_levelSequence = null;


    private Level currentLevel = null;
    private IEnumerator<Level> scenery = null;
        

    private IEnumerator<Level> LevelSequence()
    {
        return ((IEnumerable<Level>) m_levelSequence).GetEnumerator();
    }

    private void Load()
    {
        if (currentLevel == null) return;
        if (currentLevel.IsLoaded) Unload();
        AsyncOperation loading = SceneManager.LoadSceneAsync(currentLevel.Scene, LoadSceneMode.Additive);
        loading.completed += m_events.OnLevelLoaded;
        currentLevel.IsLoaded = true;
    }

    private void Unload(Action callback = null)
    {
        if (currentLevel == null || !currentLevel.IsLoaded) return;
        AsyncOperation unloading = SceneManager.UnloadSceneAsync(currentLevel.Scene);
        if (callback != null)
        {
            unloading.completed += _ => callback.Invoke();
        }
        currentLevel.IsLoaded = false;
    }


    public void LoadNextLevel()
    {
        Unload();
        if (scenery == null)
        {
            scenery = LevelSequence();
        }
        bool hasNextLevel = scenery.MoveNext();
        if (hasNextLevel)
        {
            currentLevel = scenery.Current;
            Load();
        }
        else
        {
            // Win the game
        }
    }

    public void Restart()
    {
        Unload(Load);
    }

    public override void Init()
    {
        currentLevel = null;
        foreach (Level level in m_levelSequence)
        {
            level.IsLoaded = false;
        }
    }
}