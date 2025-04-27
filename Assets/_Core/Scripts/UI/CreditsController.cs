using UnityEngine;
using UnityEngine.UIElements;

public class CreditsController : MonoBehaviour
{
    public VisualElement ui;

    public Button quitButton;

    public GameObject menuPanel;
    public GameObject creditsPanel;

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
        creditsPanel.SetActive(false);
        menuPanel.SetActive(true);
    }
}
