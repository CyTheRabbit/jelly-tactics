using UnityEngine;

public class LevelHolder : MonoBehaviour
{
    [SerializeField] private LevelManager m_levelManager = null;

    private void Start()
    {
        if (transform.childCount == 0)
        {
            m_levelManager.LoadNextLevel();
        }
    }
}