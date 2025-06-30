using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WaterSpray : Weapon
{
    [SerializeField] float currentWaterAmount;
    [SerializeField] float waterSpent;
    [SerializeField] float waterFillSpeed;

    public bool waterIsRecharging =  false;

    public float knockbackSpeed;

    [SerializeField] GameObject waterSprayBarBG;
    [SerializeField] GameObject rechargeIndicator;
    [SerializeField] Image waterSprayBar;

    [SerializeField] Mesh[] waterSprayRanges;
    [SerializeField] ParticleSystem[] waterSprayParticleSystems;

    private Animator animator;
    public float waterSprayDamage {  get; private set; }
    private float maxWaterAmount;
    private int waterSprayRangeIndex;
    private MeshCollider wsCollider;


    private int waterSpayAttackCount;

    private void Start()
    {
        waterSpayAttackCount = 0;
        animator = GetComponentInParent<Animator>();
        waterSprayDamage = GetComponentInParent<Player>().currentWaterSprayData.damage;
        maxWaterAmount = GetComponentInParent<Player>().currentWaterSprayData.waterMaxAmount;
        waterSprayRangeIndex = GetComponentInParent<Player>().currentWaterSprayData.sprayRangeIndex;
        
        wsCollider = gameObject.GetComponent<MeshCollider>();
        wsCollider.sharedMesh = waterSprayRanges[waterSprayRangeIndex];

        currentWaterAmount = maxWaterAmount;


        foreach (ParticleSystem particleSystem in waterSprayParticleSystems)
        {
            particleSystem.Stop();
        }
    }

    public override void Attack()
    {
        if (UserInputManager.instance.SprayInput && !waterIsRecharging)
        {
            animator.SetTrigger("StartedWaterSpray");

            if (animator.GetBool("IsUsingWaterSpray"))
            {
                waterSprayParticleSystems[waterSprayRangeIndex].Play();
                wsCollider.enabled = true;

                currentWaterAmount -= waterSpent * Time.deltaTime;
                waterSprayBar.fillAmount -= (waterSpent * Time.deltaTime) / maxWaterAmount;
            }

           
            waterSpayAttackCount++;
        }
        else
        {
            waterSprayParticleSystems[waterSprayRangeIndex].Stop();
            wsCollider.enabled = false;
            animator.SetBool("IsUsingWaterSpray", false);

            if (currentWaterAmount < maxWaterAmount)
            {

                waterSprayBar.fillAmount = Mathf.MoveTowards(waterSprayBar.fillAmount, 1f, Time.deltaTime * waterFillSpeed);
                currentWaterAmount = Mathf.MoveTowards(currentWaterAmount / maxWaterAmount, 1f, Time.deltaTime * waterFillSpeed) * maxWaterAmount;
            }

            if(currentWaterAmount >= maxWaterAmount)
            {
                waterIsRecharging = false;
                rechargeIndicator.SetActive(false);
                waterSprayBarBG.GetComponent<Animator>().enabled = false;
                waterSprayBarBG.GetComponent<Image>().color = Color.white;
            }

        }

        if (currentWaterAmount < 0)
        {
            currentWaterAmount = 0;
            waterIsRecharging = true;
            rechargeIndicator.SetActive(true);
            waterSprayBarBG.GetComponent<Animator>().enabled = true;
        }

    }
}
