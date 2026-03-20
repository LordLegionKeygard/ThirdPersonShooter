using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    private PlayerSpeed _playerSpeed;
    private PlayerAnimator _playerAnimator;
    private PlayerMovement _playerMovement;
    private PlayerShoot _playerShoot;
    private bool _isShooting;

    private void Awake()
    {
        _playerSpeed = GetComponent<PlayerSpeed>();
        _playerAnimator = GetComponent<PlayerAnimator>();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerShoot = GetComponent<PlayerShoot>();
    }

    private void Update()
    {
        if (!_isShooting || _playerSpeed.GetIsRun() || !_playerShoot.CanStartShoot())
        {
            return;
        }

        _playerMovement.SetRotationToForwardCamera();
        _playerShoot.BeginShoot();
    }

    public void Shoot(bool isPressed)
    {
        _isShooting = isPressed;
    }

    public void Run(bool isPressed)
    {
        _playerSpeed.ToggleRun(isPressed);
        _playerAnimator.PlayTargetBoolAnimation(isPressed, AnimatorStrings.Run);

        if (isPressed)
        {
            _isShooting = false;
        }
    }
}
