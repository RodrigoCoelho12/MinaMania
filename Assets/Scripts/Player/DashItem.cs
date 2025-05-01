using UnityEngine;

public class DashItem : Item
{
    public bool isDashing = false;           // Indica se o jogador está no meio de um dash
    public float dashSpeed = 25f;            // Velocidade do dash
    public float dashDuration = 0.2f;        // Duração do dash em segundos
    public float dashCooldown = 1f;          // Tempo de espera entre dashes
    private float dashTime = 0f;             // Tempo atual do dash em andamento

    private float lastDashTime = -Mathf.Infinity; // Armazena o momento do último dash (usado para controle de cooldown)

    private void Update()
    {
        Dash(); // Chama a lógica de dash a cada frame
    }
    public void Dash()
    {
        // Verifica se o jogador pressionou o botão de dash e se o cooldown já passou
        if (UserInputManager.instance.DashInput && Time.time >= lastDashTime + dashCooldown)
        {
            isDashing = true;           // Ativa o dash
            dashTime = 0f;              // Reinicia o tempo do dash
            lastDashTime = Time.time;   // Registra o tempo atual como o último dash
            Debug.Log("Dash");          // Mensagem no console para debug
        }

        if (isDashing)
        {
            dashTime += Time.deltaTime; // Atualiza o tempo do dash com o tempo passado desde o último frame

            // Se o tempo de dash ultrapassou a duração configurada, finaliza o dash
            if (dashTime >= dashDuration)
            {
                isDashing = false;
            }
        }
    }
}
