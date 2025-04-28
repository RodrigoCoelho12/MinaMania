using UnityEngine;
using UnityEngine.InputSystem;

public class UserInputManager : MonoBehaviour
{
   public static UserInputManager instance;


    public Vector2 MovementInput {  get; private set; }
    public bool PickaxeInput { get; private set; }
    public bool SprayInput { get; private set; }
    public bool BombInput { get; private set; }

    private PlayerInput _playerInput;

    private InputAction _movementAction;
    private InputAction _pickaxeAction;
    private InputAction _sprayAction;
    private InputAction _bombAction;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        _playerInput = GetComponent<PlayerInput>();

        SetupInputActions();
    }

    private void Update()
    {
        UpdateInput();
    }

    private void SetupInputActions()
    {
        _movementAction = _playerInput.actions["Movement"];
        _pickaxeAction = _playerInput.actions["PickaxeAttack"];
        _sprayAction = _playerInput.actions["SprayAttack"];
        _bombAction = _playerInput.actions["BombAttack"];
    }

    private void UpdateInput()
    {
        MovementInput = _movementAction.ReadValue<Vector2>();
        PickaxeInput = _pickaxeAction.IsPressed();
        SprayInput = _sprayAction.IsPressed();
        BombInput = _bombAction.WasPressedThisFrame();
    }
}
