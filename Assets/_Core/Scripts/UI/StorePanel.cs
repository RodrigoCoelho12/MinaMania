using UnityEngine;
using UnityEngine.UIElements;

public class StorePanel : MonoBehaviour
{
    public VisualElement ui;

    public Button quitButton;

    public GameObject menuPanel;
    public GameObject storePanel;

    private void Awake()
    {
        ui = GetComponent<UIDocument>().rootVisualElement;
    }

    private void OnEnable()
    {
        quitButton = ui.Q<Button>("QuitButton");
        quitButton.clicked += OnQuitButtonClicked;
    }

    private void OnQuitButtonClicked()
    {
        storePanel.SetActive(false);
        menuPanel.SetActive(true);
    }
}
