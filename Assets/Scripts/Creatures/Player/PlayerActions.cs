using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    private PlayerSpeed _playerSpeed;
    private PlayerAnimator _playerAnimator;
    private PlayerShoot _playerShoot;
    private bool _isShooting;

    private void Awake()
    {
        _playerSpeed = GetComponent<PlayerSpeed>();
        _playerAnimator = GetComponent<PlayerAnimator>();
        _playerShoot = GetComponent<PlayerShoot>();
    }

    private void Update()
    {
        if (!_isShooting || !_playerShoot.CanShoot())
        {
            return;
        }

        _playerShoot.Shoot();
        _playerAnimator.AnimatorSetTrigger(AnimatorStrings.Shoot);
    }

    public void Shoot(bool isPressed)
    {
        _isShooting = isPressed;
    }

    public void Run(bool isPressed)
    {
        _playerSpeed.ToggleRun(isPressed);
        _playerAnimator.PlayTargetBoolAnimation(isPressed, AnimatorStrings.Run);
    }
}
