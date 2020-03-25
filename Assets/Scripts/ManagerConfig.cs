using UnityEngine;

[CreateAssetMenu(fileName = "Manager Config", menuName = "Configs/Manager Config", order = 0)]
public class ManagerConfig : ScriptableObject
{
    [SerializeField] private BaseManager[] m_managers = null;

    public void Init()
    {
        foreach (BaseManager manager in m_managers) manager.Init();
    }
}