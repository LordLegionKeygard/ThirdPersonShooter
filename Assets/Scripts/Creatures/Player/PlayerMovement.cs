using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private PlayerInputSystem _playerInputSystem;
    private PlayerActions _playerInputController;
    private Animator _animator;
    private PlayerSpeed _playerSpeed;
    private CharacterController _characterController;
    public float RotationSpeed;
    private float _velocityMove;
    [SerializeField] private float acceleration = 0.1f;
    [SerializeField] private float deceleration = 0.5f;
    [SerializeField] private bool _canWalk;
    [SerializeField] private bool _canRotate = true;
    public bool CanWalk => _canWalk;
    private bool _isDeath;
    private bool _needUpdate = true;


    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _playerInputController = GetComponent<PlayerActions>();
        _animator = GetComponent<Animator>();
        _playerSpeed = GetComponent<PlayerSpeed>();
    }

    private void Update()
    {
        _animator.SetFloat("inputX", _playerInputSystem.MoveInput.x, 0.3f, Time.deltaTime * 10f);
        _animator.SetFloat("inputY", _playerInputSystem.MoveInput.y, 0.3f, Time.deltaTime * 10f);

        Walk();
    }

    private void Walk()
    {
        Vector3 direction = new Vector3(_playerInputSystem.MoveInput.x, 0f, _playerInputSystem.MoveInput.y).normalized;

        if (direction.magnitude >= 0.5f)
        {
            _needUpdate = true;
            Vector3 projectedCameraForward = Vector3.ProjectOnPlane(_camera.transform.forward, Vector3.up);
            Quaternion rotationToCamera = Quaternion.LookRotation(projectedCameraForward, Vector3.up);

            Vector3 moveDirection = rotationToCamera * direction;
            Quaternion rotationToMoveDirection = Quaternion.LookRotation(moveDirection, Vector3.up);

            if (!_canRotate) return;

            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationToMoveDirection, RotationSpeed * Time.deltaTime);

            if (!_canWalk) return;

            _characterController.Move(moveDirection.normalized * _playerSpeed.CurrentSpeed * Time.deltaTime);

            if (_velocityMove < 1.0f)
            {
                _velocityMove += Time.deltaTime * acceleration;
            }
        }
        else
        {
            if (_velocityMove > 0.0f)
            {
                _velocityMove -= Time.deltaTime * deceleration;

                if (_needUpdate)
                {
                    _playerInputController.Run(false);
                    _needUpdate = false;
                }
            }
        }
        _animator.SetFloat(AnimatorStrings.Speed, _velocityMove);
    }

    private void CanWalkToggle(bool state)
    {
        _canWalk = state;
    }

    private void CanRotateToggle(bool state)
    {
        _canRotate = state;
    }
}
