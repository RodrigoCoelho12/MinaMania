using UnityEngine;
using System.Collections;

public abstract class Enemy : MonoBehaviour
{
    #region Propriedades
    public float damageValue { get; protected set; }
    public float speedValue { get; protected set; }

    public float attackShowTime { get; protected set; }
    public float attackHideTime { get; protected set; }

    [SerializeField] protected GameObject attackHitbox;

    //[SerializeField] protected HealthController healthController;

    private Transform playerTarget;

    #endregion

    // private void OnTriggerStay(Collider other)
    // {
    //     if (other.CompareTag("WaterSpray"))
    //     {
    //         int waterSprayDamage = other.gameObject.GetComponent<WaterSpray>().atkValue;

    //         float knockbackSpeed = other.gameObject.GetComponent<WaterSpray>().knockbackSpeed;

    //         healthController.TakeDamage(waterSprayDamage * Time.deltaTime);

    //         Vector3 knockbackDirection = transform.position - other.gameObject.transform.position;// Determina a dire��o que o inimigo sera movido caso esteja dentro da area do ataque de spray

    //         transform.Translate(knockbackDirection * Time.deltaTime * knockbackSpeed, Space.World); // Movimenta o inimigo na dire��o determinada multiplicando pela velocidade desejada e Time.deltaTime para que a movimenta��o n�o seja instantanea

    //     }
    // }

    void Update()
    {
        FollowTarget();
        LockYAxis();
    }

    #region FollowTarget() Comentarios
    /*
    A fun��o FollowTarget() faz com que o inimigo siga o player
    
    A variavel playerDirection armazena a dire��o do player utilizando o m�todo RotateTowards que rotaciona 
    o forward do inimigo (frente) na dire��o do player (determinada pela subtra��o da posi��o do player da posi��o atual do inimigo);

    o if utiliza um RayCast na dire��o do player para detectar o valor da distancia do inimigo em rela��o ao player, 
    garantindo que eles n�o ocupem o mesmo espa�o e mantenham uma distancia de 1 unidade;

    O MoveTowards move o inimigo em dire��o ao player;

    o LookRotation rotaciona o inimigo baseado na dire��o atual do player (playerDirection);  
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
    
    public IEnumerator Attack(GameObject attackHitbox)
    {
        while (true)
        {
            attackHitbox.SetActive(true);

            yield return new WaitForSeconds(0.5f);

            attackHitbox.SetActive(false);

            yield return new WaitForSeconds(4f);
        }
    }
}
