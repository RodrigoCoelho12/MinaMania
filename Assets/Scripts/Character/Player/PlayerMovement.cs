using UnityEngine;

[RequireComponent (typeof(CharacterController))]
public partial class Player
{
    [Header("Player Movement Parameters")]
    public float mouseSensitivity = 100f;
    public float rotationSpeed = 10f;
    public float raycastDistance = 100f;
    private CharacterController cc;

    public override void Move()
    {
        bool isDashing = this.isDashing;
        float dashSpeed = this.currentDashData.dashSpeed;

        //float horizontalInput = UserInputManager.instance.MovementInput.x;
        //float verticalInput = UserInputManager.instance.MovementInput.y;

        //Vector3 movement = new Vector3(horizontalInput, 0, verticalInput).normalized;

        float currentSpeed = isDashing ? dashSpeed : speedValue;

        //cc.SimpleMove(currentSpeed  * movement);

    }

    void RotatePlayer()
    {
        this.transform.rotation = new Quaternion(0, this.transform.rotation.y, 0, this.transform.rotation.w);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
        if (Physics.Raycast(ray, out RaycastHit hit, raycastDistance)) 
        {
            Vector3 direction = hit.point - this.transform.position; 
            Debug.DrawLine(ray.origin, hit.point, Color.red);
            direction.y = 0f;
            if (direction.magnitude > 0.1f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }
}
