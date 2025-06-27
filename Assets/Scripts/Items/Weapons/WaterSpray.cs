using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WaterSpray : Weapon
{
    [SerializeField] float totalWaterAmount;
    [SerializeField] float currentWaterAmount;
    [SerializeField] float waterSpent;
    [SerializeField] float waterFillSpeed;

    public bool waterIsRecharging =  false;

    public float knockbackSpeed;

    [SerializeField] GameObject waterSprayBarBG;
    [SerializeField] GameObject rechargeIndicator;
    [SerializeField] Image waterSprayBar;

    private Animator animator;


    private int waterSpayAttackCount;

    private void Start()
    {
        waterSpayAttackCount = 0;
        animator = GetComponentInParent<Animator>();
    }

    public override void Attack()
    {
        MeshRenderer wsMeshRenderer= gameObject.GetComponent<MeshRenderer>();
        Collider wsCollider = gameObject.GetComponent<Collider>();

        if (UserInputManager.instance.SprayInput && !waterIsRecharging)
        {
            animator.SetTrigger("StartedWaterSpray");

            if (animator.GetBool("IsUsingWaterSpray"))
            {
                wsMeshRenderer.enabled = true;
                wsCollider.enabled = true;

                currentWaterAmount -= waterSpent * Time.deltaTime;
                waterSprayBar.fillAmount -= (waterSpent * Time.deltaTime) / totalWaterAmount;
            }

           
            waterSpayAttackCount++;
        }
        else
        {
            wsMeshRenderer.enabled = false;
            wsCollider.enabled = false;
            animator.SetBool("IsUsingWaterSpray", false);

            if (currentWaterAmount < totalWaterAmount)
            {
                waterSprayBar.fillAmount = Mathf.MoveTowards(waterSprayBar.fillAmount, 1f, Time.deltaTime * waterFillSpeed);
                currentWaterAmount = Mathf.MoveTowards(currentWaterAmount / totalWaterAmount, 1f, Time.deltaTime * waterFillSpeed) * totalWaterAmount;
            }

            if(currentWaterAmount >= totalWaterAmount)
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
