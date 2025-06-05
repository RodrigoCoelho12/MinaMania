using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WaterSpray : Weapon
{
    [SerializeField] float totalWaterAmount;
    [SerializeField] float currentWaterAmount;
    [SerializeField] float waterSpent;
    [SerializeField] float waterFillSpeed;

    [SerializeField] bool waterIsRecharging =  false;

    public float knockbackSpeed;

    [SerializeField] GameObject waterSprayBarBG;
    [SerializeField] GameObject rechargeIndicator;
    [SerializeField] Image waterSprayBar;
    public override void Attack()
    {
        MeshRenderer wsMeshRenderer= gameObject.GetComponent<MeshRenderer>();
        Collider wsCollider = gameObject.GetComponent<Collider>();

        if (UserInputManager.instance.SprayInput && waterIsRecharging == false)
        {
            wsMeshRenderer.enabled = true;
            wsCollider.enabled = true;
            
            currentWaterAmount -= waterSpent * Time.deltaTime;
            waterSprayBar.fillAmount -= (waterSpent * Time.deltaTime) / totalWaterAmount;
        }
        else
        {
            wsMeshRenderer.enabled = false;
            wsCollider.enabled = false;

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
