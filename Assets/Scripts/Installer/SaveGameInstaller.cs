using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class SaveGameInstaller : MonoInstaller
{
    [SerializeField] private SaveGame _saveGame;

    public override void InstallBindings()
    {
        Container.Bind<SaveGame>().FromComponentInNewPrefab(_saveGame).AsSingle();
    }
}
