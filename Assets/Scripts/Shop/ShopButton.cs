using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
public class ShopButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("Shop Button Properties")]
    [SerializeField] private List<ItemData> itemData;
    private int itemIndex;
    private Player player;
    private bool isHoverEnabled = true;

    private void Start()
    {
        player = FindFirstObjectByType<Player>();
        if (player == null)
            Debug.LogError("Player not found in the scene.");
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
        if (player == null)
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
                player.currentDynamiteData = dynamite;
                Debug.Log("Dynamite assigned.");
            }
            else if (currentItem is PickaxeData pickaxe)
            {
                player.currentPickaxeData = pickaxe;
                Debug.Log("Pickaxe assigned.");
            }
            else if (currentItem is WaterSprayData waterspray)
            {
                player.currentWaterSprayData = waterspray;
                Debug.Log("Waterspray assigned.");
            }
            else if (currentItem is DashData dash)
            {
                player.currentDashData = dash;
                Debug.Log("Dash assigned.");
            }
            else if (currentItem is MagnetData magnet)
            {
                player.currentMagnetData = magnet;
                Debug.Log("Magnet assigned.");
            }
            else if (currentItem is ExtraLifeData extraLife)
            {
                player.currentExtraLifeData = extraLife;
                Debug.Log("Extra Life assigned.");
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
