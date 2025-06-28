using Unity.Mathematics.Geometry;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
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

        Vector3 inputDir = new Vector3(horizontalInput, 0, verticalInput).normalized;

        if (inputDir == Vector3.zero)
        {
            if (animator != null) animator.SetFloat("Blend", 0);
            return;
        }

        float d = Vector3.Dot(transform.forward, Vector3.forward);
        if (animator != null) animator.SetFloat("Blend", d * verticalInput);

        float currentSpeed = isDashing ? dashSpeed : speedValue;

        // Detecta colisão à frente com SphereCast
        Vector3 origin = transform.position + Vector3.up * 0.5f; // eleva um pouco para evitar o chão
        float radius = 0.3f;
        float checkDistance = 0.5f;
        RaycastHit hit;

        Vector3 finalMove = inputDir;

        if (Physics.SphereCast(origin, radius, inputDir, out hit, checkDistance))
        {
            // Projeta o movimento no plano da parede (evita empurrar contra ela)
            finalMove = Vector3.ProjectOnPlane(inputDir, hit.normal).normalized;
        }

        cc.SimpleMove(finalMove * currentSpeed);
    }


    void RotatePlayer()
    {
        if (UserInputManager.instance.isUsingGamepad)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            Vector2 dir = UserInputManager.instance.LookDirectionInput;

            float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
            if (dir != Vector2.zero)
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
