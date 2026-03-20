using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    private PlayerShoot _playerShoot;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerShoot = GetComponent<PlayerShoot>();
    }
    
    public void PlayTargetBoolAnimation(bool state, int animation)
    {
        _animator.SetBool(animation, state);
    }

    public void AnimatorSetTrigger(int name)
    {
        _animator.SetTrigger(name);
    }

    public void OnShootAnimationEvent()
    {
        _playerShoot.Fire();
    }

    public void OnShootAnimationEndEvent()
    {
        _playerShoot.OnShootAnimationEnd();
    }
}
