using UnityEngine;
using UnityEngine.UIElements;

public class ConfigController : PanelControllerBase
{
    public GameManager gameManager;
    [SerializeField] GameObject mainMenu;

    protected override void OnEnable()
    {
        rootUi = GetComponent<UIDocument>().rootVisualElement;

        quitButton = rootUi.Q<Button>("QuitButton");
        quitButton.clicked += OnQuitButtonClicked;

    }

    protected override void OnDisable()
    {
        quitButton.clicked -= OnQuitButtonClicked;
    }

    protected override void OnQuitButtonClicked()
    {
        if (mainMenu.activeInHierarchy)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameManager.UnPause(gameObject);
        }
    }

}
