using UnityEngine;
using UnityEngine.UIElements;

public class SettingController : MonoBehaviour
{
    public VisualElement ui;

    public Button quitButton;

    public GameObject menuPanel;
    public GameObject settingsPanel;

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
        settingsPanel.SetActive(false);
        menuPanel.SetActive(true);
    }
}
