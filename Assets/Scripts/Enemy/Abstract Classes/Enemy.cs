using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    #region Propriedades
    public float damageValue { get; protected set; }
    public float speedValue { get; protected set; }

    [SerializeField] protected GameObject attackHitbox;
    
    [SerializeField] protected HealthController healthController;

    private Transform playerTarget;

    #endregion

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("WaterSpray"))
        {
            int waterSprayDamage = other.gameObject.GetComponent<WaterSpray>().atkValue;

            float knockbackSpeed = other.gameObject.GetComponent<WaterSpray>().knockbackSpeed;

            healthController.TakeDamage(waterSprayDamage * Time.deltaTime);

            Vector3 knockbackDirection = transform.position - other.gameObject.transform.position;// Determina a direção que o inimigo sera movido caso esteja dentro da area do ataque de spray

            transform.Translate(knockbackDirection * Time.deltaTime * knockbackSpeed, Space.World); // Movimenta o inimigo na direção determinada multiplicando pela velocidade desejada e Time.deltaTime para que a movimentação não seja instantanea

        }
    }

    void Update()
    {
        FollowTarget();
        LockYAxis();
    }

    #region FollowTarget() Comentarios
    /*
    A função FollowTarget() faz com que o inimigo siga o player
    
    A variavel playerDirection armazena a direção do player utilizando o método RotateTowards que rotaciona 
    o forward do inimigo (frente) na direção do player (determinada pela subtração da posição do player da posição atual do inimigo);

    o if utiliza um RayCast na direção do player para detectar o valor da distancia do inimigo em relação ao player, 
    garantindo que eles não ocupem o mesmo espaço e mantenham uma distancia de 1 unidade;

    O MoveTowards move o inimigo em direção ao player;

    o LookRotation rotaciona o inimigo baseado na direção atual do player (playerDirection);  
     */
    #endregion
    public void FollowTarget()
    {
        float step = speedValue * Time.deltaTime;

        Vector3 playerDirection = Vector3.RotateTowards(transform.forward, (playerTarget.position - transform.position), step, 0.0f);

        Vector3 dir = (playerTarget.position - transform.position);
        if (dir.magnitude > 1)
        {
            transform.position += dir.normalized * Time.deltaTime * speedValue;
        }

        /*
         RaycastHit hit;

         if (Physics.Raycast(transform.position, playerDirection, out hit))
         {
             Debug.DrawLine(transform.position, hit.point, Color.cyan);
             if(hit.distance > 1f)
             {
                 transform.position = Vector3.MoveTowards(transform.position, playerTarget.position, step);
             }
         }*/

        transform.rotation = Quaternion.LookRotation(playerDirection);
    }

    public void LockYAxis()
    {
        if (transform.position.y != 0)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
    }
    public void SetTarget(GameObject gameObject) 
    {
        playerTarget = gameObject.transform;
    }

    public void DeathRoutine()
    {
        Destroy(this.gameObject);
    }

    public abstract void Attack();
}
