using UnityEngine;
using Zenject;

public class WorldInstaller : MonoInstaller
{
    [SerializeField] private BulletsPool _bulletsPool;
    [SerializeField] private TakeDamageVFXPool _takeDamageVFXPool;
    [SerializeField] private UiPanels _uiPanels;
    [SerializeField] private CursorHideControl _cursorHideControl;

    public override void InstallBindings()
    {
        Container.Bind<BulletsPool>().FromInstance(_bulletsPool).AsSingle();
        Container.Bind<TakeDamageVFXPool>().FromInstance(_takeDamageVFXPool).AsSingle();
        Container.Bind<UiPanels>().FromInstance(_uiPanels).AsSingle();
        Container.Bind<CursorHideControl>().FromInstance(_cursorHideControl).AsSingle();
    }
}
