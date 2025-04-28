using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float mouseSensitivity = 100f;
    public float rotationSpeed = 10f;
    public float raycastDistance = 100f;

    private bool isDashing = false;
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;
    private float dashTime = 0f;

    private Rigidbody rb;
    private float lastDashTime = -Mathf.Infinity;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Dash();
        Move();
        RotatePlayer();
    }

    void Move()
    {
        float horizontalInput = UserInputManager.instance.MovementInput.x;
        float verticalInput = UserInputManager.instance.MovementInput.y;

        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput).normalized;

        float currentSpeed = isDashing ? dashSpeed : moveSpeed;

        transform.Translate(currentSpeed * Time.deltaTime * movement, Space.World);
    }

    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time >= lastDashTime + dashCooldown)
        {
            isDashing = true;
            dashTime = 0f;
            lastDashTime = Time.time;
        }

        if (isDashing)
        {
            dashTime += Time.deltaTime;
            if (dashTime >= dashDuration)
            {
                isDashing = false;
            }
        }
    }

    void RotatePlayer()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, raycastDistance))
        {
            Vector3 direction = hit.point - transform.position;
            Debug.DrawLine(ray.origin, hit.point, Color.red);
            direction.y = 0f;
            if (direction.magnitude > 0.1f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }
}
