using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputSystem : MonoBehaviour
{
    [SerializeField] private PlayerActions _playerActions;

    public Vector2 MoveInput { get; private set; }

    private PlayerInput _playerInput;
    private InputAction _moveAction;
    private InputAction _runAction;
    private InputAction _shootAction;
    private InputAction _menuAction;
    private bool _isCanInput;


    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _moveAction = _playerInput.actions["Move"];
        _runAction = _playerInput.actions["Run"];
        _shootAction = _playerInput.actions["Shoot"];
        _menuAction = _playerInput.actions["Menu"];
    }

    private void OnEnable()
    {
        _isCanInput = true;
        InputToggle(true);
        SubscribeInputActions();
    }

    private void Update()
    {
        UpdateInputs();
    }

    private void UpdateInputs()
    {
        if (!_isCanInput) return;
        MoveInput = _moveAction.ReadValue<Vector2>();
    }

    private void SubscribeInputActions()
    {
        _runAction.performed += OnRunPerformed;
        _runAction.canceled += OnRunCanceled;
        _shootAction.performed += OnShootPerformed;
        _shootAction.canceled += OnShootCanceled;
        _menuAction.performed += OnMenuPerformed;
    }

    private void UnsubscribeInputActions()
    {
        _runAction.performed -= OnRunPerformed;
        _runAction.canceled -= OnRunCanceled;
        _shootAction.performed -= OnShootPerformed;
        _shootAction.canceled -= OnShootCanceled;
        _menuAction.performed -= OnMenuPerformed;
    }

    private void OnRunPerformed(InputAction.CallbackContext context)
    {
        if (!_isCanInput) return;
        _playerActions.Run(true);
    }

    private void OnRunCanceled(InputAction.CallbackContext context)
    {
        if (!_isCanInput) return;
        _playerActions.Run(false);
    }

    private void OnShootPerformed(InputAction.CallbackContext context)
    {
        if (!_isCanInput) return;
        _playerActions.Shoot(true);
    }

    private void OnShootCanceled(InputAction.CallbackContext context)
    {
        if (!_isCanInput) return;
        _playerActions.Shoot(false);
    }

    private void OnMenuPerformed(InputAction.CallbackContext context)
    {
        if (!_isCanInput) return;
        CustomEvents.FireEscape();
    }

    public void InputToggle(bool state)
    {
        if (state)
        {
            _playerInput.ActivateInput();
        }
        else
        {
            _playerInput.DeactivateInput();
        }
    }

    private void OnDisable()
    {
        _isCanInput = false;
        UnsubscribeInputActions();
        InputToggle(false);
    }

    private void OnDestroy()
    {
        _isCanInput = false;
    }
}
