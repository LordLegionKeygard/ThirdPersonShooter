using System;
using UnityEngine;

public class CustomEvents : MonoBehaviour
{
    public static Action<bool> OnHideCursor;
    public static void FireHideCursor(bool state)
    {
        OnHideCursor?.Invoke(state);
    }

    public static Action<int> OnChangeScore;
    public static void FireChangeScore(int score)
    {
        OnChangeScore?.Invoke(score);
    }

    public static Action OnEscape;
    public static void FireEscape()
    {
        OnEscape?.Invoke();
    }
}
