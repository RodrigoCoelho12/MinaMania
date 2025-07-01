using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TooltipManager : MonoBehaviour
{
    public static TooltipManager Instance;

    [SerializeField] private GameObject tooltipPanel;
    [SerializeField] private TMP_Text tooltipText;
    [SerializeField] private TMP_Text tooltipTitle;
    [SerializeField] private TMP_Text tooltipPrice;

    private void Awake()
    {
        Instance = this;
        HideTooltip();
    }

    public void ShowTooltip(string message, string title, string price)
    {
        if (ShopNavigationButton.canRotate)
        { 
            tooltipPanel.SetActive(true);
            tooltipText.text = message;
            tooltipTitle.text = title;
            tooltipPrice.text = price+"$";
        }
    }

    public void HideTooltip()
    {
        tooltipPanel.SetActive(false);
    }
}
