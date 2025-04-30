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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickaxe"))
        {
            int pickaxeDamage = other.gameObject.GetComponent<Pickaxe>().atkValue;
            Debug.Log("Pickaxe Damage");
            healthController.TakeDamage(pickaxeDamage);
        } 
        else if (other.CompareTag("WaterSpray"))
        {
            int waterSprayDamage = other.gameObject.GetComponent<WaterSpray>().atkValue;
            healthController.TakeDamage(waterSprayDamage);
        }
    }

    public void FollowTarget()
    {
        var step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }

    public void SetTarget(GameObject gameObject) => target = gameObject.transform;

    public void DeathRoutine()
    {
        Destroy(this.gameObject);
    }
}
