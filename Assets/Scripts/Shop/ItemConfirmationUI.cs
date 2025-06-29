using UnityEngine;
using UnityEngine.UI;

public class ItemConfirmationUI : MonoBehaviour
{
    public GameObject confirmPanel;
    public Button yesButton;
    public Button noButton;
    public GameObject NavigationPanel;
    private System.Action onConfirmCallback;

    void Start()
    {
        confirmPanel.SetActive(false);
    }

    private void OnConfirm()
    {
        onConfirmCallback?.Invoke();
        onConfirmCallback = null;
        confirmPanel.SetActive(false);
        NavigationPanel.SetActive(true);
    }

    private void OnCancel()
    {

        onConfirmCallback = null;
        NavigationPanel.SetActive(true);
        confirmPanel.SetActive(false);
    }
    public void ShowConfirmation(System.Action onConfirm)
    {
        NavigationPanel.SetActive(false);
        confirmPanel.SetActive(true);
        onConfirmCallback = onConfirm;

        yesButton.onClick.RemoveAllListeners();
        noButton.onClick.RemoveAllListeners();

        yesButton.onClick.AddListener(OnConfirm);
        noButton.onClick.AddListener(OnCancel);
    }
}

