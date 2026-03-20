using UnityEngine;

public class PlayerSpeed : MonoBehaviour
{
    private float _currentSpeed;
    private float _walkSpeed = 3f;
    private float _runSpeed = 6f;
    private bool _isRun;
    public float GetCurrentSpeed() => _currentSpeed;
    public bool GetIsRun() => _isRun;


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
        _currentSpeed = _isRun ? _runSpeed : _walkSpeed;
    }
}
