using System;
using UnityEngine;

public class CustomEvents : MonoBehaviour
{
    public static Action<bool> OnHideCursor;
    public static void FireHideCursor(bool state)
    {
        OnHideCursor?.Invoke(state);
    }
}
