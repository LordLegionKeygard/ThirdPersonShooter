using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private PlayerInputSystem _playerInputSystem;
    private PlayerActions _playerInputController;
    private Animator _animator;
    private PlayerSpeed _playerSpeed;
    private CharacterController _characterController;
    private float _rotationSpeed = 300;
    private float _velocityMove;
    private float _acceleration = 5;
    private float _deceleration = 5;
    private float _gravity = -20;
    private float _groundedGravity = -2f;
    private bool _canWalk = true;
    private bool _canRotate = true;
    private bool _needUpdate = true;
    private float _verticalVelocity;


    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _playerInputController = GetComponent<PlayerActions>();
        _animator = GetComponent<Animator>();
        _playerSpeed = GetComponent<PlayerSpeed>();
    }

    private void Update()
    {
        Walk();
        ApplyGravity();
    }

    private void Walk()
    {
        Vector3 direction = new Vector3(_playerInputSystem.MoveInput.x, 0f, _playerInputSystem.MoveInput.y).normalized;
        Vector3 horizontalMove = Vector3.zero;

        if (direction.magnitude >= 0.5f)
        {
            _needUpdate = true;
            Vector3 projectedCameraForward = Vector3.ProjectOnPlane(_camera.transform.forward, Vector3.up);
            Quaternion rotationToCamera = Quaternion.LookRotation(projectedCameraForward, Vector3.up);

            Vector3 moveDirection = rotationToCamera * direction;
            Quaternion rotationToMoveDirection = Quaternion.LookRotation(moveDirection, Vector3.up);

            if (_canRotate)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationToMoveDirection, _rotationSpeed * Time.deltaTime);
            }

            if (_canWalk)
            {
                horizontalMove = moveDirection.normalized * _playerSpeed.GetCurrentSpeed();
            }

            if (_velocityMove < 1.0f)
            {
                _velocityMove += Time.deltaTime * _acceleration;
            }
        }
        else
        {
            if (_velocityMove > 0.0f)
            {
                _velocityMove -= Time.deltaTime * _deceleration;

                if (_needUpdate)
                {
                    _playerInputController.Run(false);
                    _needUpdate = false;
                }
            }
        }

        Vector3 move = horizontalMove;
        move.y = _verticalVelocity;
        _characterController.Move(move * Time.deltaTime);

        _animator.SetFloat(AnimatorStrings.Speed, _velocityMove);
    }

    private void ApplyGravity()
    {
        if (_characterController.isGrounded && _verticalVelocity < 0f)
        {
            _verticalVelocity = _groundedGravity;
            return;
        }

        _verticalVelocity += _gravity * Time.deltaTime;
    }

    public void SetRotationToForwardCamera()
    {
        Vector3 cameraForward = Vector3.ProjectOnPlane(_camera.transform.forward, Vector3.up);

        if (cameraForward.sqrMagnitude <= Mathf.Epsilon)
        {
            return;
        }

        transform.rotation = Quaternion.LookRotation(cameraForward.normalized, Vector3.up);
    }
}
