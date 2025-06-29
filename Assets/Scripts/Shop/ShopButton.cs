using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
public class ShopButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("Shop Button Properties")]
    [SerializeField] private List<ItemData> itemData;
    private int itemIndex;
    private bool isHoverEnabled = true;
    PlayerSO playerSO;
    private void Start()
    {
        playerSO = FindAnyObjectByType<PlayerSO>();
        itemIndex = 1; // Default to the first weapon
        ChangeButtonAppearance();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isHoverEnabled)
            return;

        this.transform.localScale *= 1.1f; // Scale up the button on hover
        if (itemIndex < itemData.Count && itemData[itemIndex] != null)
        {
            string tooltip;
            if (itemData[itemIndex] is DynamiteData dynamiteData)
            {
                tooltip = dynamiteData.GetTooltip(itemData[itemIndex - 1] as DynamiteData);
            }
            else if (itemData[itemIndex] is PickaxeData pickaxeData)
            {
                tooltip = pickaxeData.GetTooltip(itemData[itemIndex - 1] as PickaxeData);
            }
            else if (itemData[itemIndex] is WaterSprayData waterSprayData)
            {
                tooltip = waterSprayData.GetTooltip(itemData[itemIndex - 1] as WaterSprayData);
            }
            else if (itemData[itemIndex] is DashData dashData)
            {
                tooltip = dashData.GetTooltip(itemData[itemIndex - 1] as DashData);
            }
            else if (itemData[itemIndex] is MagnetData magnetData)
            {
                tooltip = magnetData.GetTooltip(itemData[itemIndex - 1] as MagnetData);
            }
            else if (itemData[itemIndex] is ExtraLifeData extraLifeData)
            {
                tooltip = extraLifeData.GetTooltip(itemData[itemIndex - 1] as ExtraLifeData);
            }
            else
            {
                Debug.LogWarning("Unknown item type.");
                return;
            }

            TooltipManager.Instance.ShowTooltip(tooltip);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.transform.localScale /= 1.1f; // Scale down the button on exit
        TooltipManager.Instance.HideTooltip();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (playerSO == null)
        {
            Debug.LogError("Player reference is missing.");
            return;
        }

        if (itemIndex >= itemData.Count)
        {
            Debug.Log("No more weapons to assign.");
            DisableButton();
            return;
        }

        ItemData currentItem = itemData[itemIndex];

        if (currentItem != null)
        {
            if (currentItem is DynamiteData dynamite)
            {
                playerSO.dynamiteData = dynamite;
                Debug.Log("Dynamite assigned.");
                playerSO.overrides = true;
            }
            else if (currentItem is PickaxeData pickaxe)
            {
                playerSO.pickaxeData = pickaxe;
                Debug.Log("Pickaxe assigned.");
                playerSO.overrides = true;
            }
            else if (currentItem is WaterSprayData waterspray)
            {
                playerSO.waterSprayData = waterspray;
                Debug.Log("Waterspray assigned.");
                playerSO.overrides = true;
            }
            else if (currentItem is DashData dash)
            {
                playerSO.dashData = dash;
                Debug.Log("Dash assigned.");
                playerSO.overrides = true;
            }
            else if (currentItem is MagnetData magnet)
            {
                playerSO.magnetData = magnet;
                Debug.Log("Magnet assigned.");
                playerSO.overrides = true;
            }
            else if (currentItem is ExtraLifeData extraLife)
            {
                playerSO.extraLifeData = extraLife;
                Debug.Log("Extra Life assigned.");
                playerSO.overrides = true;
            }
            else
            {
                Debug.LogWarning("Unknown weapon type.");
                return;
            }
        }
        else
        {
            Debug.LogWarning("Weapon data is null.");
            return;
        }

        itemIndex++;

        if (itemIndex >= itemData.Count)
        {
            DisableButton();
            return; // Avoid changing appearance for invalid index
        }

        ChangeButtonAppearance();
    }

    private void DisableButton()
    {
        isHoverEnabled = false;
    }

    private void ChangeButtonAppearance()
    {
        if (itemData == null || itemIndex >= itemData.Count)
        {
            Debug.LogWarning("Reached end of item list or itemData is not assigned. Appearance not changed.");
            return;
        }

        MeshRenderer renderer = GetComponent<MeshRenderer>();
        if (renderer != null)
        {
            renderer.material = itemData[itemIndex].itemMaterial;
            renderer.GetComponent<MeshFilter>().mesh = itemData[itemIndex].itemMesh;
            Debug.Log($"Button appearance changed to {itemData[itemIndex].itemName}.");
        }
        else
        {
            Debug.LogWarning("MeshRenderer component is missing.");
        }
    }


}
