using UnityEngine;
using UnityEngine.UIElements;

public class KeybindsController : PanelControllerBase
{
    protected Button forwardButton;
    protected Button backwardButton;
    protected Button rightButton;
    protected Button leftButton;

    protected override void OnEnable()
    {
        rootUi = GetComponent<UIDocument>().rootVisualElement;

        quitButton = rootUi.Q<Button>("QuitButton");
        forwardButton = rootUi.Q<Button>("ForwardKeyButton");
        backwardButton = rootUi.Q<Button>("BackwardKeyButton");
        rightButton = rootUi.Q<Button>("RightKeyButton");
        leftButton = rootUi.Q<Button>("LeftKeyButton");



        quitButton.clicked += OnQuitButtonClicked;

    }
}
