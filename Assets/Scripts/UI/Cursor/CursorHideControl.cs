using UnityEngine;
using Cinemachine;

public class CursorHideControl : MonoBehaviour
{
    [SerializeField] private CinemachineInputProvider _cinemachineInputProvider;
    private int _activeUiWindow;
    public bool IsUiActive() => _activeUiWindow > 0;

    private void Start()
    {
        CustomEvents.OnHideCursor += ActiveWindow;
        SetupCursor();
    }

    private void ActiveWindow(bool state)
    {
        if (state) _activeUiWindow++;
        else _activeUiWindow--;

        SetupCursor();
    }

    private void SetupCursor()
    {
        Cursor.visible = IsUiActive();
        Cursor.lockState = IsUiActive() ? CursorLockMode.None : CursorLockMode.Locked;
        _cinemachineInputProvider.enabled = !IsUiActive();
    }

    private void OnDestroy()
    {
        CustomEvents.OnHideCursor -= ActiveWindow;
    }
}
