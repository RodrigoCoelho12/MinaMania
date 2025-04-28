using UnityEngine;
using UnityEngine.UIElements;

public class StorePanel : PanelControllerBase
{
    public GameManager gameManager;
    /*
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
        gameManager.UnPause(gameObject);
    }
    */
}
