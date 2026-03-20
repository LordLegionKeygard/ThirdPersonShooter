using UnityEngine;
using Zenject;

public class WorldInstaller : MonoInstaller
{
    [SerializeField] private BulletsPool _bulletsPool;

    public override void InstallBindings()
    {
        Container.Bind<BulletsPool>().FromInstance(_bulletsPool).AsSingle();
    }
}
