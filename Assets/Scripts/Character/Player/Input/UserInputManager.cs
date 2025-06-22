using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class UserInputManager : MonoBehaviour
{
   public static UserInputManager instance;


    public Vector2 MovementInput {  get; private set; }
    public Vector2 LookDirectionInput {  get; private set; }
    public bool PickaxeInput { get; private set; }
    public bool SprayInput { get; private set; }
    public bool DynamiteInput { get; private set; }
    public bool DashInput { get; private set; }
    public bool MenuOpenCloseInput { get; private set; }
    public bool isUsingGamepad {  get; private set; }



    private PlayerInput _playerInput;
    private InputAction _movementAction;
    private InputAction _lookDirectionAction;
    private InputAction _pickaxeAction;
    private InputAction _sprayAction;
    private InputAction _dynamiteAction;
    private InputAction _dashAction;
    private InputAction _menuOpenCloseAction;

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
        _lookDirectionAction = _playerInput.actions["LookDirection"];
        _pickaxeAction = _playerInput.actions["PickaxeAttack"];
        _sprayAction = _playerInput.actions["SprayAttack"];
        _dynamiteAction = _playerInput.actions["DynamiteAttack"];
        _dashAction = _playerInput.actions["Dash"];
        _menuOpenCloseAction = _playerInput.actions["MenuOpenClose"];
    }

    private void UpdateInput()
    {
        MovementInput = _movementAction.ReadValue<Vector2>();
        LookDirectionInput = _lookDirectionAction.ReadValue<Vector2>();
        PickaxeInput = _pickaxeAction.IsPressed();
        SprayInput = _sprayAction.IsPressed();
        DynamiteInput = _dynamiteAction.WasPressedThisFrame();
        DashInput = _dashAction.WasPressedThisFrame();
        MenuOpenCloseInput = _menuOpenCloseAction.WasPressedThisFrame();

        if(_playerInput.currentControlScheme == "Gamepad")
        {
            isUsingGamepad = true;
        }
        else
        {
            isUsingGamepad = false;
        }
    }
}
