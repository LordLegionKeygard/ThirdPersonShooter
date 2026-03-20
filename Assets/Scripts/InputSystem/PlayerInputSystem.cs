using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInputSystem : MonoBehaviour
{
    [SerializeField] private PlayerActions _playerActions;
    public Vector2 MoveInput { get; private set; }
    private PlayerInput _playerInput;

    private delegate void Run(bool state);
    private Run _run;

    private delegate void Shoot();
    private Shoot _shoot;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }
    private void Start()
    {
        InputToggle(true);
        SetupInputActions();
        SetupDelegates();
    }

    private void Update()
    {
        UpdateInputs();
    }

    private void SetupDelegates()
    {
        _run = new Run(_playerActions.Run);
        _shoot = new Shoot(_playerActions.Shoot);
    }

    private void SetupInputActions()
    {
        _playerInput.actions["Run"].performed += _ => _run(true);
        _playerInput.actions["Run"].canceled += _ => _run(false);
        _playerInput.actions["Shoot"].performed += _ => _shoot();
    }

    private void UpdateInputs()
    {
        MoveInput = _playerInput.actions["Move"].ReadValue<Vector2>();
    }

    public void InputToggle(bool state)
    {
        if (state) _playerInput.ActivateInput();
        else _playerInput.DeactivateInput();
    }

    private void OnDestroy()
    {
        InputToggle(false);

        _run = delegate { };
        _shoot = delegate { };
    }
}
