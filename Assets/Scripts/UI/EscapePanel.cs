using UnityEngine;
using Zenject;

public class EscapePanel : MonoBehaviour
{
    [Inject] private UiPanels _uiPanels;
    [SerializeField] private PauseSystem _pauseSystem;

    private void OnEnable()
    {
        _pauseSystem.Pause();
    }

    public void Continue()
    {
        // AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.DefaultClick, _playerHealth.transform.position);
        _pauseSystem.Unpause();
        gameObject.SetActive(false);
    }

    public void UpgradeButton()
    {
        // AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.DefaultClick, _playerHealth.transform.position);
        _uiPanels.GetPanel(PanelEnum.Upgrade).SetActive(true);
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    private void OnDisable()
    {
        _pauseSystem.Unpause();
    }
}
