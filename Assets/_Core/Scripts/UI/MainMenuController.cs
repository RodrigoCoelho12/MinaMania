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
        Debug.Log("PLAY");
        //gameManager.StartGame();
        menuPanel.SetActive(false);
        rankingPanel.SetActive(false);
    }

    private void OnRankingButtonClicked()
    {
        Debug.Log("RANKING");
        menuPanel.SetActive(false);
        rankingPanel.SetActive(true);
    }    
    
    private void OnSettingsButtonClicked()
    {
        Debug.Log("SETTINGS");
        menuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }
    
    private void OnCreditsButtonClicked()
    {
        Debug.Log("CREDITS");
        menuPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    private void OnQuitButtonClicked()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
