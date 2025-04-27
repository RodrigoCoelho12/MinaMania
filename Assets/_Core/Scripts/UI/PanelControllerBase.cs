using UnityEngine;
using UnityEngine.UIElements;

public class PanelControllerBase : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject thisPanel;

    protected VisualElement ui;
    protected Button quitButton;

    protected virtual void Awake()
    {
        ui = GetComponent<UIDocument>().rootVisualElement;
    }

    protected virtual void OnEnable()
    {
        quitButton = ui.Q<Button>("QuitButton");
        if (quitButton != null)
            quitButton.clicked += OnQuitButtonClicked;
    }

    protected virtual void OnDisable()
    {
        if (quitButton != null)
            quitButton.clicked -= OnQuitButtonClicked;
    }

    protected virtual void OnQuitButtonClicked()
    {
        thisPanel.SetActive(false);
        menuPanel.SetActive(true);
    }
}
