using UnityEngine;
using UnityEngine.UIElements;

public class PauseController : PanelControllerBase
{
    public GameObject configPanel; 
    public GameManager gameManager;

    protected Button configButton;
  
    protected override void OnEnable()
    {
        rootUi = GetComponent<UIDocument>().rootVisualElement;

        quitButton = rootUi.Q<Button>("QuitButton");
        configButton = rootUi.Q<Button>("ConfigButton");


        quitButton.clicked += OnQuitButtonClicked;
        configButton.clicked += OnConfigButtonClicked;
    }


    protected override void OnDisable()
    {
        configButton.clicked -= OnConfigButtonClicked;
        quitButton.clicked -= OnQuitButtonClicked;

    }

    protected override void OnQuitButtonClicked()
    {
        gameManager.UnPause(gameObject);
    }

    private void OnConfigButtonClicked()
    {
        gameObject.SetActive(false);
        configPanel.SetActive(true);
    }
}
