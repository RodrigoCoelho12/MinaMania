using TMPro;
using UnityEngine;

public class CurrencyController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;

    private void Update()
    {
        UpdateCurrencyUI();
    }

    public void UpdateCurrencyUI()
    {
        text.text = ": " + PlayerSO.Instance.playerCurrency.ToString();
    }
}
