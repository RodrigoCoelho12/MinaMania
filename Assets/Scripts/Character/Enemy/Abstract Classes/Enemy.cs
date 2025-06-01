using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;
using UnityEngine.AI;

// Abstract class for all enemy types in the game. This class contains the basic attack functionality that all enemies will share.
public abstract class Enemy : Character
{
    [Header("Enemy Properties")]
    public Transform target;                // The target that the enemy will attack.
    public GameObject attackHitbox;         // The hitbox that will be activated when the enemy attacks.
    protected HitBox _hitBox;                // Reference to the HitBox component.
    public bool isAwakened = false;         // If true, the enemy will move towards the target and attack.

    private Vector3 _positionInProfiling;   // The initial position to reset the enemy when it dies.
    private float exposureTime = 0f;        // Time exposed to a continuous attack like WaterSpray.
    private Coroutine attackCoroutine;      // Reference to the attack coroutine.

    public float attackCooldown = 1f; // Time between attacks
    public float attackDuration = 0.5f; // Duration of the attack animation
    public float attackRange = 4f; // Range of the attack

    private NavMeshAgent navMeshAgent; // Reference to the NavMeshAgent component


    // Fix
    private void Awake()
    {
        // Store the starting position for reuse on death.
        healthBar.CurrentBarValue = healthBar.MaxBarValue;
        this.navMeshAgent = GetComponent<NavMeshAgent>();
        _positionInProfiling = transform.position;
    }

    #region Movement & Awakening

    public override void Move()
    {
        if(gameObject.GetComponent<Rigidbody>().linearVelocity.magnitude <= 0.1) 
        {
            navMeshAgent.enabled = true;
        }

        if (!isAwakened || navMeshAgent == null || target == null)
            return;

        // Set the destination for the NavMeshAgent
        navMeshAgent.SetDestination(target.position);

        // Optional: Limit rotation to Y-axis only
        if (navMeshAgent.velocity.sqrMagnitude > 0.1f)
        {
            Vector3 lookDirection = navMeshAgent.steeringTarget - transform.position;
            lookDirection.y = 0f;
            if (lookDirection != Vector3.zero)
            {
                Quaternion rotation = Quaternion.LookRotation(lookDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10f);
            }
        }
    }

    /// <summary>
    /// Toggles the awakened state and starts/stops the attack coroutine accordingly.
    /// </summary>
    public void SetAwakened(bool awakened)
    {
        if (isAwakened == awakened)
            return;

        isAwakened = awakened;

        if (isAwakened)
            StartAttackRoutine();
        else
            StopAttackRoutine();
    }

    public void StartAttackRoutine()
    {
        if (attackCoroutine == null)
            attackCoroutine = StartCoroutine(ShowHitBox());
    }

    public void StopAttackRoutine()
    {
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
            attackCoroutine = null;
            attackHitbox.SetActive(false);
        }
    }

    public void Attack()
    {
        // Check if the target is within attack range
        if (Vector3.Distance(transform.position, target.position) <= attackRange)
        {
            // Start the attack coroutine
            StartAttackRoutine();
        }
        else
        {
            // If the target is out of range, stop the attack
            StopAttackRoutine();
        }
    }
    #endregion

    #region Attack Hitbox Coroutine

    /// <summary>
    /// Activates the attack hitbox in intervals while the enemy is awakened.
    /// </summary>
protected IEnumerator ShowHitBox()
{
    WaitForSeconds hitboxActiveTime = new WaitForSeconds(0.5f);
    WaitForSeconds hitboxCooldownTime = new WaitForSeconds(4f);

    while (isAwakened)
    {
        attackHitbox.SetActive(true);

        float distance = Vector3.Distance(transform.position, target.position);
        if (distance > 4f)
        {
            //_hitBox.ChangeHitBoxColor(2);
        }
        else if (distance > 2f)
        {
            //_hitBox.ChangeHitBoxColor(1);
        }
        else
        {
            //_hitBox.ChangeHitBoxColor(0);
        }

        yield return hitboxActiveTime;

        attackHitbox.SetActive(false);
        yield return hitboxCooldownTime;
    }
}


    #endregion

    #region Damage & Death Handling

    /// <summary>
    /// Checks if health has dropped to zero or below, and triggers death routine.
    /// </summary>
    public void CheckDeath()
    {
        if (healthBar.CurrentBarValue <= 0)
            DeathRoutine();
    }

    public override void DeathRoutine()
    {
        // Play death animation here if needed.
        StopAttackRoutine();
        gameObject.SetActive(false);

        // Reset position and state for pooling or reuse.
        transform.position = _positionInProfiling;
        isAwakened = false;
    }

    public override void OnTriggerEnter(Collider other)
    {
        // Handle initial hit from any weapon.
        if (other.CompareTag("Pickaxe"))
        {
            Pickaxe pickaxe = other.GetComponent<Pickaxe>();
            
            healthBar.AdjustStatusBarBySubtraction(10);
            CheckDeath();
        }

    }

    public override void OnTriggerStay(Collider other)
    {
        // Handle continuous damage from WaterSpray.
        if (other.CompareTag("WaterSpray"))
        {
            WaterSpray waterSpray= other.GetComponent<WaterSpray>();
            
            if (waterSpray == null)
                return;

            float knockbackSpeed = waterSpray.knockbackSpeed;
            Vector3 knockbackDirection = transform.position - other.gameObject.transform.position;
            transform.Translate(knockbackDirection * Time.deltaTime * knockbackSpeed, Space.World);

            exposureTime += Time.deltaTime;
            
            if (exposureTime >= 2f)
            {
                healthBar.AdjustStatusBarBySubtraction(1 * 2);
                exposureTime = 0f;
                CheckDeath();
            }
        }
    }

    #endregion
}