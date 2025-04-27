using UnityEngine;
using UnityEngine.UIElements;

public class PauseController : PanelControllerBase
{
    public GameObject pausePanel;

    private void OnEnable()
    {
        quitButton = ui.Q<Button>("QuitButton");
        quitButton.clicked += OnQuitButtonClicked;
    }

    private void OnQuitButtonClicked()
    {
        pausePanel.SetActive(false);
        menuPanel.SetActive(true);
    }
}
