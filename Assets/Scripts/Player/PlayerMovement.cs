using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float mouseSensitivity = 100f;
    public float rotationSpeed = 10f;
    public float raycastDistance = 100f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    void Update()
    {
        Move();
        RotatePlayer();
    }

    void Move()
    {
        bool isDashing = gameObject.GetComponent<DashItem>().isDashing;
        float dashSpeed = gameObject.GetComponent<DashItem>().dashSpeed;

        float horizontalInput = UserInputManager.instance.MovementInput.x;
        float verticalInput = UserInputManager.instance.MovementInput.y;

        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput).normalized;

        float currentSpeed = isDashing ? dashSpeed : moveSpeed;

        transform.Translate(currentSpeed * Time.deltaTime * movement, Space.World);
    }


    void RotatePlayer()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Cria um Ray com origem da camera e direção determinada pela posição do mouse na tela
        if (Physics.Raycast(ray, out RaycastHit hit, raycastDistance)) // Lança o Ray e armazena o hit (acerto) do  Raycast na variavel hit
        {
            Vector3 direction = hit.point - transform.position; // Determina a direção desejada subtraíndo a posição do ponto de acert (hit) e a propria posição atual
            Debug.DrawLine(ray.origin, hit.point, Color.red);
            direction.y = 0f; // Trava o eixo y para que o player não olhe para cima
            if (direction.magnitude > 0.1f) // Impede a rotação do player caso o mouse esteja muito proximo da posição do player na tela
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction); // Armazena o alvo de rotação do player (direção desejada)
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime); // Interpola o valor de rotação do player atual com o alvo, numa velocidade determinada (rotationSpeed * Time.deltaTime)
            }
        }
    }
}
