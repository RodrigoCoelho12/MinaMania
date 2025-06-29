using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class ShopInputManager : MonoBehaviour
{
    public static ShopInputManager instance;

    public bool RotateRightInput { get; private set; }
    public bool RotateLeftInput { get; private set; }
    public bool SubmitInput { get; private set; }
    public bool BackInput { get; private set; }
    public bool isUsingGamepad { get; private set; }

    private PlayerInput _playerInput;

    private InputAction _rotateRightAction;
    private InputAction _rotateLeftAction;
    private InputAction _submitAction;
    private InputAction _backAction;



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
        _rotateRightAction = _playerInput.actions["RotateRight"];
        _rotateLeftAction = _playerInput.actions["RotateLeft"];
        _submitAction = _playerInput.actions["Submit"];
        _backAction = _playerInput.actions["Back"];
    }

    private void UpdateInput()
    {
        RotateRightInput = _rotateRightAction.WasPressedThisFrame();
        RotateLeftInput = _rotateLeftAction.WasPressedThisFrame();
        SubmitInput = _submitAction.WasPressedThisFrame();
        BackInput = _backAction.WasPressedThisFrame();

        if (_playerInput.currentControlScheme == "Gamepad")
        {
            isUsingGamepad = true;
        }
        else
        {
            isUsingGamepad = false;
        }
    }
}