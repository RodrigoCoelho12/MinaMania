using UnityEngine;

public class EnemyProto : MonoBehaviour
{
    private Transform target;
    private HealthController healthController;

    public EnemyTypes type;

    private float speed;

    void Start()
    {
        switch (type)
        {
            case EnemyTypes.Speed:
                speed = 6;
                break;
            case EnemyTypes.Tank:
                speed = 1;
                break;
            case EnemyTypes.Damage:
                speed = 3;
                break;
            default:
                Debug.LogWarning("Enemy type undefined");
                Destroy(this.gameObject);
                break;
        }

        healthController = gameObject.GetComponent<HealthController>();
       
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

            Vector3 knockbackDirection = transform.position - other.gameObject.transform.position;

            transform.Translate(knockbackDirection * Time.deltaTime * knockbackSpeed, Space.World);
        }
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
    } // -> freeze Y enemy position on Unity

    public void SetTarget(GameObject gameObject)
    {
        target = gameObject.transform;
    } 

    public void DeathRoutine()
    {
        Destroy(this.gameObject);
    }
}
