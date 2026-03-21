using UnityEngine;
using Zenject;

public class DontDestroyOnLoadInstaller : MonoInstaller
{
    [SerializeField] private GameObject[] _toInstall;
    public override void InstallBindings()
    {
        foreach (var installable in _toInstall)
        {
            DontDestroyOnLoad(Instantiate(installable));
        }
    }
}