using TMPro;
using UnityEngine;

public class WorldLanguage : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] _escapePanelTexts;

    private void Start()
    {
        SetupLanguage();
    }

    private void SetupLanguage()
    {
        _escapePanelTexts[0].text = Language.TextStatic[0];
        _escapePanelTexts[1].text = Language.TextStatic[1];
        _escapePanelTexts[2].text = Language.TextStatic[2];
    }
}
