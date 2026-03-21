using UnityEngine;

public class PlayerSpeed : MonoBehaviour
{
    private float _currentSpeed;
    private float _walkSpeed;
    private float _runSpeed = 3f;
    private bool _isRun;
    public float GetCurrentSpeed() => _currentSpeed;
    public bool GetIsRun() => _isRun;

    public void SetupSpeed(float speed)
    {
        _walkSpeed = speed;
        ChangeCurrentSpeed();
    }

    public void ToggleRun(bool state)
    {
        _isRun = state;
        ChangeCurrentSpeed();
    }

    private void ChangeCurrentSpeed()
    {
        _currentSpeed = _isRun ? _walkSpeed + _runSpeed : _walkSpeed;
    }
}
