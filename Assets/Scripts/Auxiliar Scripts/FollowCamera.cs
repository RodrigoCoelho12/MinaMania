using UnityEngine;

public class CameraFollow3D : MonoBehaviour
{
    public Transform player;

    // Offset no eixo Z em relação ao jogador
    public float zOffset = -30f;

    // Altura fixa da câmera
    public float fixedHeightY = 35f;

    // Zona morta no eixo X (a câmera só move se o jogador sair dela)
    public float deadZoneWidth = 4f;

    public float smoothTime = 0.2f;
    private Vector3 velocity = Vector3.zero;

    // Limites do mapa (opcional)
    public Vector2 minXzLimit;
    public Vector2 maxXzLimit;

    void LateUpdate()
    {
        Vector3 cameraPos = transform.position;

        // Cálculo da posição-alvo da câmera com altura fixa e offset Z
        Vector3 targetPos = new Vector3(player.position.x, fixedHeightY, player.position.z + zOffset);

        // Verifica se o jogador saiu da zona morta no eixo X
        float deltaX = player.position.x - cameraPos.x;
        if (Mathf.Abs(deltaX) > deadZoneWidth / 2f)
        {
            cameraPos.x = Mathf.Lerp(cameraPos.x, targetPos.x, Time.deltaTime / smoothTime);
        }

        // A câmera sempre segue no eixo Z suavemente (mantendo offset)
        cameraPos.z = Mathf.Lerp(cameraPos.z, targetPos.z, Time.deltaTime / smoothTime);

        // Altura fixa
        cameraPos.y = fixedHeightY;

        // Limites (opcional)
        cameraPos.x = Mathf.Clamp(cameraPos.x, minXzLimit.x, maxXzLimit.x);
        cameraPos.z = Mathf.Clamp(cameraPos.z, minXzLimit.y, maxXzLimit.y);

        transform.position = cameraPos;
    }

    void OnDrawGizmosSelected()
    {
        if (player == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(
            new Vector3(player.position.x - deadZoneWidth / 2f, fixedHeightY, player.position.z + zOffset),
            new Vector3(player.position.x + deadZoneWidth / 2f, fixedHeightY, player.position.z + zOffset)
        );
    }
}
