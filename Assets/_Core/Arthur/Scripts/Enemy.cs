using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform target;
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

    }

    void Update()
    {
        FollowTarget();
    }

    public void FollowTarget()
    {
        var step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        if (Vector3.Distance(transform.position, target.position) < 0.001f)
        {
            target.position *= -1.0f;
        }
    }
    public void SetTarget(GameObject gameObject) => target = gameObject.transform;

    public void DeathRoutine()
    {
        Destroy(this.gameObject);
    }
}
