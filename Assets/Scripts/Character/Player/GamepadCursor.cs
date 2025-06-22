using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Users;

public class GamepadCursor : MonoBehaviour
{
    private Mouse _virtualMouse;
    private RectTransform cursorTransform;

    [SerializeField] PlayerInput _playerInput;
    [SerializeField] RectTransform canvasTransform;

    [SerializeField] float cursorSpeed = 1000f;
    [SerializeField] int padding = 35;

    private void Awake()
    {
        cursorTransform = GetComponent<RectTransform>();
    }
    private void OnEnable()
    {
        if(_virtualMouse == null)
        {
            _virtualMouse = (Mouse)InputSystem.AddDevice("VirtualMouse");
        }
        else if (!_virtualMouse.added)
        {
            InputSystem.AddDevice(_virtualMouse);
        }

        InputUser.PerformPairingWithDevice(_virtualMouse, _playerInput.user);

        if(cursorTransform != null)
        {
            Vector2 position = cursorTransform.position;
            InputState.Change(_virtualMouse.position, position);
        }
    }

    private void Update()
    {
        UpdateMotion();
    }

    private void OnDisable()
    {
        if (_virtualMouse != null && _virtualMouse.added)
        {
            InputSystem.RemoveDevice(_virtualMouse);
        }
    }

    private void UpdateMotion()
    {
        if(_virtualMouse == null || !UserInputManager.instance.isUsingGamepad)
        {
            return;
        }

        Vector2 deltaValue = UserInputManager.instance.LookDirectionInput * cursorSpeed * Time.deltaTime;

        Vector2 currentPosition = _virtualMouse.position.ReadValue();
        Vector2 newPosition = currentPosition + deltaValue;

        newPosition.x = Mathf.Clamp(newPosition.x, padding, Screen.width - padding);
        newPosition.y = Mathf.Clamp(newPosition.y, padding, Screen.height - padding);

        InputState.Change(_virtualMouse.position, newPosition);
        InputState.Change(_virtualMouse.delta, deltaValue);

        AnchorCursor(newPosition);

    }

    private void AnchorCursor(Vector2 position)
    {
        Vector2 anchoredPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasTransform, position, null ,out anchoredPosition);
        cursorTransform.anchoredPosition = anchoredPosition;
    }
}
