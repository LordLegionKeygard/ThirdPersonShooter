using UnityEngine;

public class UiPanels : MonoBehaviour
{
    [SerializeField] private GameObject[] _panels;
    public GameObject GetPanel(PanelEnum panel) => _panels[(int)panel];
    private void Start()
    {
        CustomEvents.OnEscape += EscapePanelToggle;
    }

    private void EscapePanelToggle()
    {
        // if (_panels[(int)PanelEnum.Upgrade].activeInHierarchy)
        // {
        //     _panels[(int)PanelEnum.Upgrade].SetActive(false);
        //     // AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.AllPanelsOpen[3], _playerHealth.transform.position);
        //     return;
        // }

        PanelsToggleActiveSelf((int)PanelEnum.Escape);
    }

    public void PanelsToggleActiveSelf(int number)
    {
        _panels[number].SetActive(!_panels[number].activeSelf);
    }

    private void OnDestroy()
    {
        CustomEvents.OnEscape -= EscapePanelToggle;
    }
}

[System.Serializable]
public enum PanelEnum
{
    Escape = 0,
    Upgrade = 1,
}
