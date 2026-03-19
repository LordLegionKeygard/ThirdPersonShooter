using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    public void PlayTargetBoolAnimation(bool state, int animation)
    {
        _animator.SetBool(animation, state);
    }
}
