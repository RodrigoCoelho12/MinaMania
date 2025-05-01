using UnityEngine;

public class DashItem : Item
{
    public bool isDashing = false;
    public float dashSpeed = 25f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;
    private float dashTime = 0f;

    private float lastDashTime = -Mathf.Infinity;

    private void Update()
    {
        Dash();
    }

    public  void Dash()
    {
        if (UserInputManager.instance.DashInput && Time.time >= lastDashTime + dashCooldown)
        {
            isDashing = true;
            dashTime = 0f;
            lastDashTime = Time.time;
            Debug.Log("Dash");
        }

        if (isDashing)
        {
            dashTime += Time.deltaTime;
            if (dashTime >= dashDuration)
            {
                isDashing = false;
            }
        }

        //    public float speed = 5.0f; // Velocidade do personagem

        //void Update()
        //{
        //    // Obter a direção para frente
        //    Vector3 forwardDirection = Vector3.forward;

        //    // Multiplicar a direção para frente pela velocidade e pelo tempo
        //    Vector3 movement = forwardDirection * speed * Time.deltaTime;

        //    // Aplicar o movimento ao personagem
        //    transform.Translate(movement, Space.World);
        //}
    }
}
