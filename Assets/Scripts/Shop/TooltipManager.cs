using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TooltipManager : MonoBehaviour
{
    public static TooltipManager Instance;

    [SerializeField] private GameObject tooltipPanel;
    [SerializeField] private TMP_Text tooltipText;

    private void Awake()
    {
        Instance = this;
        HideTooltip();
    }

    public void ShowTooltip(string message)
    {
        if (ShopNavigationButton.canRotate)
        { 
            tooltipPanel.SetActive(true);
            tooltipText.text = message;
        }
    }

    public void HideTooltip()
    {
        tooltipPanel.SetActive(false);
    }
}
