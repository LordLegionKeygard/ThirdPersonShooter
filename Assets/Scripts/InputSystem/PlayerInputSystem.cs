using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInputSystem : MonoBehaviour
{
    [SerializeField] private PlayerActions _playerActions;
    public Vector2 MoveInput { get; private set; }
    private PlayerInput _playerInput;
    private delegate void Run(bool firstState);
    Run run;

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
        run = new Run(_playerActions.Run);
    }

    private void SetupInputActions()
    {
        _playerInput.actions["Run"].performed += _ => run(true);
        _playerInput.actions["Run"].canceled += _ => run(false);
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

        run = delegate { };
    }
}
