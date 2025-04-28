using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class RankingController : MonoBehaviour
{
    public VisualElement ui;

    public Button quitButton;

    public GameObject menuPanel;
    public GameObject rankingPanel;

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
        rankingPanel.SetActive(false);
        menuPanel.SetActive(true);
    }
}
