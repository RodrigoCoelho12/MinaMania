using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuController : MonoBehaviour
{
    public VisualElement ui;
    public GameManager gameManager;

    public Button playButton;
    public Button rankingButton;
    public Button settingsButton;
    public Button creditsButton;
    public Button quitButton;

    public GameObject settingsPanel;
    public GameObject creditsPanel;
    public GameObject menuPanel;
    public GameObject rankingPanel;

    private void Awake()
    {
        ui = GetComponent<UIDocument>().rootVisualElement;
    }

    private void OnEnable()
    {
        ui = GetComponent<UIDocument>().rootVisualElement;

        playButton = ui.Q<Button>("PlayButton");
        rankingButton = ui.Q<Button>("RankingButton");
        settingsButton = ui.Q<Button>("SettingsButton");
        creditsButton = ui.Q<Button>("CreditsButton");
        quitButton = ui.Q<Button>("QuitButton");

        if (playButton != null) playButton.clicked += OnPlayButtonClicked;
        if (rankingButton != null) rankingButton.clicked += OnRankingButtonClicked;
        if (settingsButton != null) settingsButton.clicked += OnSettingsButtonClicked;
        if (creditsButton != null) creditsButton.clicked += OnCreditsButtonClicked;
        if (quitButton != null) quitButton.clicked += OnQuitButtonClicked;
    }

    private void OnDisable()
    {
        if (playButton != null) playButton.clicked -= OnPlayButtonClicked;
        if (rankingButton != null) rankingButton.clicked -= OnRankingButtonClicked;
        if (settingsButton != null) settingsButton.clicked -= OnSettingsButtonClicked;
        if (creditsButton != null) creditsButton.clicked -= OnCreditsButtonClicked;
        if (quitButton != null) quitButton.clicked -= OnQuitButtonClicked;
    }

    private void OnPlayButtonClicked()
    {
        gameManager.StartGame();
        menuPanel.SetActive(false);
        rankingPanel.SetActive(false);
    }

    private void OnRankingButtonClicked()
    {
        rankingPanel.SetActive(true);
    }    
    
    private void OnSettingsButtonClicked()
    {
        settingsPanel.SetActive(true);
    }
    
    private void OnCreditsButtonClicked()
    {
        creditsPanel.SetActive(true);
    }

    private void OnQuitButtonClicked()
    {
        Application.Quit();
    }
}
