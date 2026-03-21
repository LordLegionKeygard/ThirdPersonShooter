using UnityEngine;

// 0 - English						en
// 1 - Russian						ru

public class Language : MonoBehaviour
{
    public static int LanguageNumber = 1;
    private string[,] _text = new string[WorldGameInfo.LanguageLength, 2];
    public static string[] TextStatic = new string[WorldGameInfo.LanguageLength];

    private void Awake()
    {
        SetLanguage();
    }

    public void SetLanguage()
    {
        _text[0, 0] = "Continue";
        _text[0, 1] = "Продолжить";

        _text[1, 0] = "Upgrade";
        _text[1, 1] = "Улучшить";

        _text[2, 0] = "Exit";
        _text[2, 1] = "Выход";

        for (int x = 0; x < WorldGameInfo.LanguageLength; x++) TextStatic[x] = _text[x, LanguageNumber];
    }
}
