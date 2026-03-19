using UnityEngine;

public class PlayerSpeed : MonoBehaviour
{
    public float CurrentSpeed { get; private set; }
    private float _walkSpeed = 3f;
    private float _runSpeed = 6f;
    private bool _isRun;


    private void Awake()
    {
        ChangeCurrentSpeed();
    }

    public void ToggleRun(bool state)
    {
        _isRun = state;
        ChangeCurrentSpeed();
    }

    private void ChangeCurrentSpeed()
    {
        CurrentSpeed = _isRun ? _runSpeed : _walkSpeed;
    }
}
