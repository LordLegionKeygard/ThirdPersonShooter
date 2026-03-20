using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    private PlayerSpeed _playerSpeed;
    private PlayerAnimator _playerAnimator;
    private PlayerShoot _playerShoot;

    private void Awake()
    {
        _playerSpeed = GetComponent<PlayerSpeed>();
        _playerAnimator = GetComponent<PlayerAnimator>();
    }

    public void Shoot()
    {
        if(_playerShoot.CanShoot())
        {
            _playerShoot.TryShoot();
            _playerAnimator.AnimatorSetTrigger(AnimatorStrings.Shoot);
        }
    }

    public void Run(bool isPressed)
    {
        _playerSpeed.ToggleRun(isPressed);
        _playerAnimator.PlayTargetBoolAnimation(isPressed, AnimatorStrings.Run);
    }
}
