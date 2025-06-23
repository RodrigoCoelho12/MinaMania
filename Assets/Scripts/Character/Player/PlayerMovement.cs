using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent (typeof(CharacterController))]
public partial class Player
{
    [Header("Player Movement Parameters")]
    public float mouseSensitivity = 100f;
    public float rotationSpeed = 10f;
    public float raycastDistance = 100f;
    
    private Quaternion targetRotation = Quaternion.identity;
    private CharacterController cc;
    private Animator animator;

    public override void Move()
    {
        bool isDashing = this.isDashing;
        float dashSpeed = this.currentDashData.dashSpeed;

        float horizontalInput = UserInputManager.instance.MovementInput.x;
        float verticalInput = UserInputManager.instance.MovementInput.y;

        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput).normalized;

        animator.SetFloat("Blend", verticalInput);

        float currentSpeed = isDashing ? dashSpeed : speedValue;

        cc.SimpleMove(currentSpeed  * movement);

    }

    void RotatePlayer()
    {
        if (UserInputManager.instance.isUsingGamepad)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            Vector2 dir = UserInputManager.instance.LookDirectionInput;

            float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
            if(dir != Vector2.zero)
            {
                targetRotation = Quaternion.Euler(0, angle, 0);
            }
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, raycastDistance))
            {
                Vector3 direction = hit.point - this.transform.position;
                Debug.DrawLine(ray.origin, hit.point, Color.red);
                direction.y = 0f;
                if (direction.magnitude > 0.1f)
                {
                    targetRotation = Quaternion.LookRotation(direction);
                }
            }
        }

        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

    }
}
