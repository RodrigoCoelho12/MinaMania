using UnityEngine;

public class EnemyProto : MonoBehaviour
{
    private Transform target;
    private HealthController healthController;

    public EnemyTypes type;

    private float speed;
    public float damage;

    void Start()
    {
        healthController = gameObject.GetComponent<HealthController>();

        switch (type)
        {
            case EnemyTypes.Speed:
                speed = 6;
                damage = 2;
                healthController.maxHealth = 20;
                break;
            case EnemyTypes.Tank:
                speed = 1;
                damage = 4;
                healthController.maxHealth = 50;
                break;
            case EnemyTypes.Damage:
                speed = 3;
                damage = 6;
                healthController.maxHealth = 35;
                break;
            default:
                Debug.LogWarning("Enemy type undefined");
                Destroy(this.gameObject);
                break;
        }
      
    }

    void Update()
    {
        FollowTarget();
        LockYAxis();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickaxe"))
        {
            int pickaxeDamage = other.gameObject.GetComponent<Pickaxe>().atkValue;
            Debug.Log("Pickaxe Damage");
            healthController.TakeDamage(pickaxeDamage);
        } 
    }

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

    public void Attack() { 
    }

    public void FollowTarget()
    {
        var step = speed * Time.deltaTime;
        
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }

    public void LockYAxis()
    {
        if (transform.position.y != 0)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
    }

    public void SetTarget(GameObject gameObject) => target = gameObject.transform;

    public void DeathRoutine()
    {
        Destroy(this.gameObject);
    }
}
