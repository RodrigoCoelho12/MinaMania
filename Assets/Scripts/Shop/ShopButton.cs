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

    private PlayerSO playerSO;

    private void Start()
    {
        originalScale = transform.localScale;

        playerSO = FindAnyObjectByType<PlayerSO>();
        playerSO.overrides = false;

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
            if (!string.IsNullOrEmpty(tooltip))
            {
                TooltipManager.Instance.ShowTooltip(tooltip);
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
        if (currentCamera.transform.position == camPos[1])
        {
            if (itemIndex >= itemData.Count)
            {
                Debug.Log("No more items or player not assigned.");
                DisableButton();
                return;
            }
            if (playerSO.playerCurrency >= itemData[itemIndex].itemPrice)
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
                ConfirmAssignment(() => playerSO.dynamiteData = dynamite);
                break;
            case PickaxeData pickaxe:
                ConfirmAssignment(() => playerSO.pickaxeData = pickaxe);
                break;
            case WaterSprayData waterSpray:
                ConfirmAssignment(() => playerSO.waterSprayData = waterSpray);
                break;
            case DashData dash:
                ConfirmAssignment(() => playerSO.dashData = dash);
                break;
            case MagnetData magnet:
                ConfirmAssignment(() => playerSO.magnetData = magnet);
                break;
            case ExtraLifeData extraLife:
                ConfirmAssignment(() => playerSO.extraLifeData = extraLife);
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
        itemIndex++;
        playerSO.overrides = true;
        Debug.Log("Advanced to next item.");
        playerSO.playerCurrency -= itemData[itemIndex].itemPrice;
        if (itemIndex >= itemData.Count)
        {
            DisableButton();
            return;
        }

        ChangeButtonAppearance();
    }

    private void DisableButton()
    {
        isHoverEnabled = false;
        Debug.Log("Shop button disabled.");
    }

    private void ChangeButtonAppearance()
    {
        if (itemData == null || itemIndex >= itemData.Count)
        {
            Debug.LogWarning("No item to show.");
            return;
        }

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
        return current.description;

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
}
