using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("Shop Button Properties")]
    [SerializeField] private List<ItemData> itemData;
    private int itemIndex = 1;

    private bool isHoverEnabled = true;

    public Camera currentCamera;
    public ItemConfirmationUI confirmationUI;
    private Vector3 originalScale;

    public Vector3[] camPos = new Vector3[2]; // index 0 = standard, index 1 = close
    private bool isMoving;

    bool isAvailable;

    private void Start()
    {
        originalScale = transform.localScale;

        CheckButtonType();

        ChangeButtonAppearance();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isHoverEnabled)
            return;

        transform.localScale *= 1.1f;

        if (currentCamera.transform.position != camPos[1] || confirmationUI.confirmPanel.activeInHierarchy)
            return;

        if (itemIndex < itemData.Count && itemData[itemIndex] != null)
        {
            string tooltip = GetTooltipForItem(itemIndex);
            string title = GetTitleForItem(itemIndex);
            string price = GetPriceForItem(itemIndex);

            if (!string.IsNullOrEmpty(tooltip))
            {
                TooltipManager.Instance.ShowTooltip(tooltip, title, price);
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = originalScale;
        TooltipManager.Instance.HideTooltip();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (currentCamera.transform.position == camPos[1] && isAvailable)
        {
            if (itemIndex >= itemData.Count)
            {
                Debug.Log("No more items or player not assigned.");
                DisableButton();
                return;
            }
            if (PlayerSO.Instance.playerCurrency >= itemData[itemIndex].itemPrice)
            {
                HandleItemAssignment(itemData[itemIndex]);
            }

        }

        if (!isMoving)
        {
            StartMoving(camPos[1], 1f, currentCamera.gameObject);
        }
    }

    private void HandleItemAssignment(ItemData item)
    {
        if (item == null)
        {
            Debug.LogWarning("Item data is null.");
            return;
        }

        switch (item)
        {
            case DynamiteData dynamite:
                ConfirmAssignment(() => PlayerSO.Instance.dynamiteData = dynamite);
                break;
            case PickaxeData pickaxe:
                ConfirmAssignment(() => PlayerSO.Instance.pickaxeData = pickaxe);
                break;
            case WaterSprayData waterSpray:
                ConfirmAssignment(() => PlayerSO.Instance.waterSprayData = waterSpray);
                break;
            case DashData dash:
                ConfirmAssignment(() => PlayerSO.Instance.dashData = dash);
                break;
            case MagnetData magnet:
                ConfirmAssignment(() => PlayerSO.Instance.magnetData = magnet);
                break;
            case ExtraLifeData extraLife:
                ConfirmAssignment(() => PlayerSO.Instance.extraLifeData = extraLife);
                break;
            default:
                Debug.LogWarning("Unknown item type.");
                break;
        }
    }

    private void ConfirmAssignment(System.Action assignAction)
    {
        confirmationUI.ShowConfirmation(() =>
        {
            assignAction?.Invoke();
            Debug.Log("Item assigned.");
            AdvanceItem();
        });
    }

    private void AdvanceItem()
    {
        // Cobra pelo novo item
        PlayerSO.Instance.playerCurrency -= itemData[itemIndex].itemPrice;
        // Verifica se h� mais itens
        if (itemIndex + 1 >= itemData.Count)
        {
            Debug.Log("No more items to advance to.");
            isAvailable = false;
            DisableButton();
            return;
        }
        // Avan�a para o pr�ximo item
        itemIndex++;

        //// Se acabou os itens após esse avanço, desativa botão
        //if (itemIndex + 1 >= itemData.Count)
        //{

        //    DisableButton();
        //}

        ChangeButtonAppearance();
    }


    private void DisableButton()
    {
        isHoverEnabled = false;
        this.gameObject.SetActive(false);
        Debug.Log("Shop button disabled.");
    }

    private void ChangeButtonAppearance()
    {
        if (itemData == null || itemIndex >= itemData.Count)
        {
            Debug.LogWarning("No item to show.");
            return;
        }
        isAvailable = true;
        if (TryGetComponent(out MeshRenderer renderer))
        {
            renderer.material = itemData[itemIndex].itemMaterial;

            if (TryGetComponent(out MeshFilter filter))
                filter.mesh = itemData[itemIndex].itemMesh;

            Debug.Log($"Button appearance changed to {itemData[itemIndex].itemName}.");
        }
        else
        {
            Debug.LogWarning("MeshRenderer component is missing.");
        }
    }

    private string GetTooltipForItem(int index)
    {
        if (index <= 0 || index >= itemData.Count)
            return null;

        ItemData current = itemData[index];
        ItemData previous = itemData[index - 1];

        return current switch
        {
            DynamiteData dynamite => dynamite.GetTooltip(previous as DynamiteData),
            PickaxeData pickaxe => pickaxe.GetTooltip(previous as PickaxeData),
            WaterSprayData spray => spray.GetTooltip(previous as WaterSprayData),
            DashData dash => dash.GetTooltip(previous as DashData),
            MagnetData magnet => magnet.GetTooltip(previous as MagnetData),
            ExtraLifeData life => life.GetTooltip(previous as ExtraLifeData),
            _ => null
        };
    }
    private string GetTitleForItem(int index)
    {
        if (index <= 0 || index >= itemData.Count)
            return null;

        ItemData current = itemData[index];
        //ItemData previous = itemData[index - 1];

        return current switch
        {
            DynamiteData dynamite => dynamite.itemName,
            PickaxeData pickaxe => pickaxe.itemName,
            WaterSprayData spray => spray.itemName,
            DashData dash => dash.itemName,
            MagnetData magnet => magnet.itemName,
            ExtraLifeData life => life.itemName,
            _ => null
        };
    }
    private string GetPriceForItem(int index)
    {
        if (index <= 0 || index >= itemData.Count)
            return null;

        ItemData current = itemData[index];
        //ItemData previous = itemData[index - 1];

        return current switch
        {
            DynamiteData dynamite => dynamite.itemPrice.ToString(),
            PickaxeData pickaxe => pickaxe.itemPrice.ToString(),
            WaterSprayData spray => spray.itemPrice.ToString(),
            DashData dash => dash.itemPrice.ToString(),
            MagnetData magnet => magnet.itemPrice.ToString(),
            ExtraLifeData life => life.itemPrice.ToString(),
            _ => null
        };
    }

    public void StartMoving(Vector3 newPosition, float timeToMove, GameObject gameObject)
    {
        StartCoroutine(MoveCameraToPosition(newPosition, timeToMove, gameObject));
    }

    private IEnumerator MoveCameraToPosition(Vector3 targetPosition, float duration, GameObject gameObject)
    {
        isMoving = true;
        Vector3 startPosition = gameObject.transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            gameObject.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        gameObject.transform.position = targetPosition;
        isMoving = false;
    }

    private void CheckButtonType()
    {
        switch (gameObject.tag)
        {
            case "Pickaxe":
                itemIndex = PlayerSO.Instance.pickaxeData.pickaxeIndex+1;
                if(itemIndex == 3)
                    DisableButton();
                break;

            case "WaterSpray":
                itemIndex = PlayerSO.Instance.waterSprayData.waterSprayIndex+1;
                if (itemIndex == 3)
                    DisableButton();
                break;

            case "Dynamite":
                itemIndex = PlayerSO.Instance.dynamiteData.dynamiteIndex+1;
                if (itemIndex == 3)
                    DisableButton();
                break;

            case "Boots":
                if (PlayerSO.Instance.dashData != null)
                    DisableButton();
                break;

            case "Magnet":
                if (PlayerSO.Instance.magnetData != null)
                    DisableButton();
                break;

            case "ExtraLife":
                if (PlayerSO.Instance.extraLifeData != null)
                    DisableButton();
                break;
        }
    }
}
