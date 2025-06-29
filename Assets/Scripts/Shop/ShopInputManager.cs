using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class ShopInputManager : MonoBehaviour
{
    public static ShopInputManager instance;

    public bool RotateRightInput { get; private set; }
    public bool RotateLeftInput { get; private set; }
    public bool isUsingGamepad { get; private set; }

    private PlayerInput _playerInput;

    private InputAction _rotateRightAction;
    private InputAction _rotateLeftAction;


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
    }

    private void UpdateInput()
    {
        RotateRightInput = _rotateRightAction.WasPressedThisFrame();
        RotateLeftInput = _rotateLeftAction.WasPressedThisFrame();

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