using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    private PlayerSpeed _playerSpeed;
    private PlayerAnimator _playerAnimator;

    private void Awake()
    {
        _playerSpeed = GetComponent<PlayerSpeed>();
        _playerAnimator = GetComponent<PlayerAnimator>();
    }

    public void Attack()
    {

    }

    public void Run(bool isPressed)
    {
        _playerSpeed.ToggleRun(isPressed);
        _playerAnimator.PlayTargetBoolAnimation(isPressed, AnimatorStrings.Run);
    }
}
