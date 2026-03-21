using UnityEngine;
using Zenject;

public class WorldInstaller : MonoInstaller
{
    [SerializeField] private BulletsPool _bulletsPool;
    [SerializeField] private TakeDamageVFXPool _takeDamageVFXPool;

    public override void InstallBindings()
    {
        Container.Bind<BulletsPool>().FromInstance(_bulletsPool).AsSingle();
        Container.Bind<TakeDamageVFXPool>().FromInstance(_takeDamageVFXPool).AsSingle();
    }
}
